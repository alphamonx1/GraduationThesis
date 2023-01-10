using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Mvc;

namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _service;

        public DashboardController(DashboardService service)
        {
            _service = service;

        }

        [HttpGet("hr/employee")]
        public ActionResult<int> CountTotalEmployeeActive()
        {
            var result = _service.CountTotalEmployeeActive();
            if(result < 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(new
                {
                    TotalEmployee = result
                });
            }


        }
        [HttpGet("hr/application")]
        public ActionResult<int> CountTotalApplications()
        {
            var result = _service.CountTotalApplication();
            if(result < 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(new
                {
                    TotalApplication = result
                });
            }

        }

        [HttpGet("hr/workplace")]
        public ActionResult<int> CountTotalWorkplace()
        {
            var result = _service.CountTotalWorkplace();
            if (result < 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(new
                {
                    TotalWorkplace = result
                });
            }
        }



    }
}
