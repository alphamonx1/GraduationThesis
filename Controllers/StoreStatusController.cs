using CAPSTONEPROJECT.DataModels.ReportEmployeeWorkStatusModel;
using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreStatusController : ControllerBase
    {
        private readonly CountService _service;

        public StoreStatusController(CountService service)
        {
            _service = service;

        }

        [HttpGet("manager")]
        public ActionResult<StoreWorkingStatusModel> GetStoreStatus(String WorkplaceID)
        {
            var StoreStatus = _service.GetStoreWorkingStatus(WorkplaceID);
            if(StoreStatus != null)
            {
                return Ok(StoreStatus);
            }
            else
            {
                return BadRequest(new
                {
                    Message = "Hiện tại không có ca làm việc nào để hiển thị"
                });
            }


        }



    }
}
