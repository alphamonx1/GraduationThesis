using CAPSTONEPROJECT.DataModels.EmployeeTypeDataModel;
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
    public class EmployeeTypeController : ControllerBase
    {
        private readonly EmployeeTypeService _service;

        public EmployeeTypeController(EmployeeTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<EmployeeTypeResponseModel> GetAll()
        {
            var List = _service.GetAll();
            if (List != null)
            {
                return Ok(List);
            }
            else
            {
                return NoContent();
            }




        }

    }
}
