using CAPSTONEPROJECT.DataModels.ApplicationDataModel;
using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Linq;

namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationService _service;
        public ApplicationController(ApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<ApplicationResponseModel> GetAll()
        {

            var list = _service.GetAll();
            if (list == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(list);
            }


        }

        [HttpGet("manager/{ApplicationID}/{EmployeeID}")]
        public ActionResult<ApplicationResponseModel> GetByID(int ApplicationID, string EmployeeID)
        {
            var application = _service.GetByID(ApplicationID, EmployeeID);
            if (application == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(application);
            }

        }

        [HttpGet("{month}/{year}")]
        public ActionResult<ApplicationResponseModel> GetApplicationsByMonth(int month, int year)
        {
            var list = _service.GetApplicationByMonthAndYear(month, year);
            if (list == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(list);
            }
        }

        ///api/application/1/10/2021
        [HttpGet("{employeeID}/{month}/{year}")]
        public ActionResult<ApplicationResponseModel> GetByIDAndMonthAndYear(string employeeID, int month, int year)
        {
            var list = _service.GetApplicationByIDAndMonthAndYear(employeeID, month, year);
            if (list == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(list);
            }
        }

        [HttpPost]
        public ActionResult<bool> Create([FromBody] ApplicationCreateModel dataModel)
        {
            if (dataModel.ApplicationTypeID == 1)
            {
                var exist = _service.IsWSExist(dataModel.EmployeeID, dataModel.ShiftID, dataModel.ApplyDate);
                if (exist)
                {
                    var status = _service.CreateApplication(dataModel);
                    if (status)
                    {
                        return Ok("Gửi đơn thành công");
                    }
                    else
                    {
                        return BadRequest("Đơn không hợp lệ, bạn đã gửi đơn rồi ? ");
                    }
                }
                else
                {
                    return BadRequest("Không có lịch làm việc");
                }
            }
            else if(dataModel.ApplicationTypeID == 3)
            {
                var exist = _service.IsWSExist(dataModel.EmployeeID, dataModel.ShiftID, dataModel.ApplyDate);
                if (exist)
                {
                    var status = _service.CreateApplication(dataModel);
                    if (status)
                    {
                        return Ok("Gửi đơn thành công");
                    }
                    else
                    {
                        return BadRequest("Đơn không hợp lệ, bạn đã gửi đơn rồi ?");
                    }
                }
                else
                {
                    return BadRequest("Không có lịch làm việc");
                }
            }
            else
            {
                var exist = _service.IsWSExist(dataModel.EmployeeID, dataModel.ShiftID, dataModel.ApplyDate);
                if (!exist)
                {
                    var status = _service.CreateApplication(dataModel);
                    if (status)
                    {
                        return Ok("Gửi đơn thành công");
                    }
                    else
                    {
                        return BadRequest("Đơn không hợp lệ");
                    }
                }
                else
                {
                    return BadRequest("Bạn đã có lịch làm việc trong thời gian này, hãy thử lại với ngày khác");
                }
                

            }



        }

        [HttpPut("{id}/{employeeID}")]
        public ActionResult<bool> Update(int id, string employeeID, [FromBody] ApplicationUpdateModel dataModel)
        {
            bool condition = _service.CheckApplicationCondition(id);
            if (condition)
            {
                bool status = _service.UpdateApplication(id, employeeID, dataModel);
                if (status)
                {
                    return Ok("Đơn đã được cập nhật");
                }
                else
                {
                    return BadRequest("Đơn cập nhật thất bại , hãy kiểm tra lại thông tin");
                }
            }
            else
            {
                return BadRequest("Đơn đã được duyệt ,nếu muốn thay đổi hãy báo cho các cấp quản lý");
            }
            
        }

        [HttpPut("manager/{id}/{employeeID}")]
        public ActionResult<bool> Approved(int id, string employeeID, int? shiftID, [FromBody] ApplicationApproveModel dataModel)
        {
            bool condition = _service.CheckApplicationCondition(id);
            if (condition)
            {
                bool status = _service.ApprovedApplication(id, employeeID, shiftID, dataModel);
                if (status)
                {
                    return Ok("Đơn đã được duyệt");
                }
                else
                {
                    return BadRequest("Duyệt đơn thất bại , hãy thử lại");
                }
            }
            else
            {
                return BadRequest("Đơn đã được duyệt bởi người quản lý khác hoặc đã bị hủy bỏ, hãy thử lại");
            }
           
        }

        [HttpDelete("manager/{id}")]
        public ActionResult<bool> Reject(int id, string employeeID, [FromBody] ApplicationApproveModel dataModel)
        {
            bool condition = _service.CheckApplicationCondition(id);
            if (condition)
            {
                bool status = _service.RejectApplication(id, employeeID, dataModel);
                if (status)
                {
                    return Ok("Từ chối phê duyệt đơn thành công");
                }
                else
                {
                    return BadRequest("Thất bại , hãy thử lại");
                }
            }
            else
            {
                return BadRequest("Đơn đã được duyệt hoặc đã bị hủy bỏ , hãy thử lại");
            }
            
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Cancel(int id, string employeeID)
        {
            bool condition = _service.CheckApplicationCondition(id);
            if (condition)
            {
                bool status = _service.CancelApplication(id, employeeID);
                if (status)
                {
                    return Ok("Đơn đã được hủy bỏ");
                }
                else
                {
                    return BadRequest("Hủy bỏ đơn thất bại , thử lại");
                }
            }
            else
            {
                return BadRequest("Đơn đã được duyệt hoặc bị từ chối, hãy liên hệ với các cấp quản lý nếu muốn thay đổi");


            }
           
        }






    }
}
