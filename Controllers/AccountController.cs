using CAPSTONEPROJECT.DataModels.AccountDataModel;
using CAPSTONEPROJECT.DataModels.MailDataModel;
using CAPSTONEPROJECT.Models;
using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _service;
        private readonly MailService _mailService;
        private readonly EmployeeService _employeeService;
        private readonly LugContext _context;
        public AccountController(AccountService service, MailService mailService, EmployeeService employeeService, LugContext context)
        {
            _service = service;
            _mailService = mailService;
            _employeeService = employeeService;
            _context = context;
        }


        [HttpGet]
        public ActionResult<AccountResponseModel> GetAll()
        {
            var list = _service.GetAll();
            if (list == null)
            {
                return NoContent();
            }
            return Ok(list);
        }

        [HttpGet("{id}")]
        public ActionResult<AccountResponseModel> GetByID(string id)
        {
            var account = _service.GetByID(id);
            if (account == null)
            {
                return NoContent();
            }
            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateAsync([FromBody] AccountCreateModel dataModel)
        {
            bool status = _service.CreateAccount(dataModel);

            if (status)
            {
                MailRequestModel mailModel = new()
                {
                    ToEmail = _employeeService.GetByID(dataModel.AccountID).Email,
                    Subject = "(No-reply) - Thông tin đăng nhập ",
                    Body = "<h3>Tài Khoản: " + dataModel.AccountID + " </h3></br>" + "<h3>mật khẩu: " + _service.DeCryptPassword(_service.GetByID(dataModel.AccountID).Password) + " </h3></br>" + "<h2> Hãy đổi mật khẩu ngay sau khi đăng nhập vào hệ thống</h2>"
                };
                var emailSent = await _mailService.SendEmailAsync(mailModel);
                if (emailSent)
                {
                    return Ok("Tạo tài khoản thành công");

                }
                else
                {
                    
                    return BadRequest("Email không xác định");
                }

            }
            else
            {
                return BadRequest(new
                {
                    Message = "Tạo tài khoản thất bại , hãy thử lại",
                });
            }
        }

        [HttpPost("SendOTP")]
        public async Task<ActionResult<bool>> SendOTPAsync([FromBody] SendOTPModel dataModel)
        {
            bool status = _service.CreateOTP(dataModel.EmployeeID);
            if (status)
            {
                var otp = _service.GetOTP(dataModel.EmployeeID);
                MailRequestModel mailModel = new()
                {
                    ToEmail = _employeeService.GetByID(dataModel.EmployeeID).Email,
                    Subject = "(No-reply) - Mã xác nhận của bạn ",
                    Body = otp,
                };
                bool success = await _mailService.SendEmailAsync(mailModel);
                if (success)
                {
                    return Ok("Gửi OTP thành công");
                }
                else
                {
                    var account = _context.Accounts.Where(x => x.AccountId == dataModel.EmployeeID).FirstOrDefault();
                    account.Otp = null;
                    _context.SaveChanges();
                    return BadRequest("Gửi OTP thất bại,email không tồn tại, hãy thử lại");
                }


            }
            else
            {
                return BadRequest(new
                {
                    Message = "Gửi OTP thất bại , tài khoản không tồn tại"
                });
            }


        }

       [HttpPost("OTPVerify")]
        public ActionResult<bool> OTPVerify(VerifyOTPModel dataModel)
        {
            bool status = _service.VerifyOTP(dataModel.OTP, dataModel.EmployeeID);
            if (status)
            {
                return Ok("xác nhận OTP thành công");
            }
            else
            {
                return BadRequest("OTP không đúng , hãy thử lại");
            }


        }


        [HttpPut("superadmin/SetRole")]
        public ActionResult<bool> SetRole([FromBody] AccountSetRoleModel dataModel)
        {


            bool status = _service.SetRole(dataModel.EmployeeID,dataModel.RoleID);
            if (status)
            {
                return Ok("Cập nhật role thành công");
            }
            else
            {
                return BadRequest("Cập nhật role thất bại, hãy thử lại");
            }
        }

        [HttpPut("resetPassword")]
        public  ActionResult<bool> Update([FromBody] AccountResetModel dataModel)
        {
                bool status = _service.ResetPassword(dataModel.EmployeeID, dataModel);
                if (status)
                {
                    return Ok("Đổi mật khẩu thành công");
                }
                else
                {
                    return BadRequest("Đổi mật khẩu thất bại , hãy thử lại");

                }

        }

        [HttpPut("changePassword")]
        public ActionResult<bool> ChangePassword([FromBody] AccountUpdateModel dataModel)
        {
            bool PassVerify = _service.VerifyPassword(dataModel.VerifyPassword, dataModel.EmployeeID);
            if (PassVerify)
            {
                bool status = _service.ChangePassword(dataModel.EmployeeID, dataModel);
                if (status)
                {
                    return Ok("Thay đổi mật khẩu thành công");
                }
                else
                {
                    return BadRequest("Thay đổi mật khẩu thất bại , hãy thử lại");
                }
            }
            else
            {
                return BadRequest("Mật khẩu xác nhận không đúng , bạn đã quên mật khẩu ? hãy thử quên Mật khẩu");
            }




        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(string id)
        {
            bool status = _service.DeleteAccount(id);
            if (status)
            {

                return Ok("Xóa tài khoản thành công");
            }
            else
            {
                return NotFound("Xóa tài khoản thật bại , hãy thử lại");
            }
        }
    }
}
