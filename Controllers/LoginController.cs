
using CAPSTONEPROJECT.DataModels.LoginDataModel;
using CAPSTONEPROJECT.Models;
using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Linq;

namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _service;
        private readonly LugContext _context;

        public LoginController(LoginService service, LugContext context)
        {
            _service = service;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = _service.AuthenticatedUser(login);

            if (user != null)
            {
                var tokenString = _service.GenerateJsonWebToken(user);
                var expired = _service.GetExpiredDateFromToken(tokenString);
                var account = _context.Accounts.Where(x => x.AccountId == login.EmployeeID).FirstOrDefault();

                account.AccessToken = tokenString;
                _context.SaveChanges();

                response = Ok(new { 
                    EmployeeID = user.EmployeeID,
                    RoleName = user.RoleName,
                    FullName = user.Fullname,
                    WorkplaceID = user.WorkplaceID,
                    Token = tokenString,
                    Expired = expired,
                    EmployeeTypeID = user.EmployeeTypeID,
                });
            }
            else
            {
                response = BadRequest("Đăng nhập thất bại , hãy thử lại");
            }

            return response;
        }

        [HttpPost("{token}")]
        public IActionResult RefreshToken(string token)
        {
            var principal = _service.GetPrincipalFromExpiredToken(token);
            var role = principal.Claims.FirstOrDefault(c => c.Type == "roleName").Value;
            var employeeID = principal.Claims.FirstOrDefault(c => c.Type == "employeeID").Value;
            var fullName = principal.Claims.FirstOrDefault(c => c.Type == "fullName").Value;
            var WorkplaceID = principal.Claims.FirstOrDefault(c => c.Type == "workplaceid").Value;
            var user = new UserResponseModel
            {
                EmployeeID = employeeID,
                RoleName = role,
                Fullname = fullName,
                WorkplaceID = WorkplaceID,
                
                
            };

            var newToken = _service.GenerateJsonWebToken(user);
            var expired = _service.GetExpiredDateFromToken(newToken);

            if(newToken != null)
            {
                return Ok(new
                {
                    Messsage = "Token refresh complete",
                    token = newToken,
                    EmployeeID = employeeID,
                    Fullname = fullName,
                    RoleName = role,
                    Workplace = WorkplaceID,
                    
                    
                });
            }
            else
            {
                return Unauthorized("Not an valid token");
            }


            


        }


    }
}
