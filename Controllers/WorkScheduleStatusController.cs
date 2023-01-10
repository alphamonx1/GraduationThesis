using CAPSTONEPROJECT.DataModels.WorkScheduleStatusDataModel;
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
    public class WorkScheduleStatusController : ControllerBase
    {
        private readonly WorkScheduleStatusService _service;

        public WorkScheduleStatusController(WorkScheduleStatusService service)
        {
            _service = service;
        }

    [HttpGet]
    public ActionResult<WorkScheduleStatusGetModel> GetAll()
        {
            var list = _service.GetAll();
            if(list != null)
            {
                return Ok(list);
            }
            else
            {
                return NoContent();
            }
        }

    }
}
