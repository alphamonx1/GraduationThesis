using CAPSTONEPROJECT.DataModels.InfoDataModel;

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
    public class GetInfoController : ControllerBase
    {
        [HttpGet]
        public ActionResult<InfoDataModel> GetInfo(DateTime inputDate)
        {

            DateTime currentServerDate = DateTime.Now;
            DateTime currentLocalDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentServerDate, "SE Asia Standard Time");
            DateTime inputDateConvert = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(inputDate, "SE Asia Standard Time");
            var currentHours = currentServerDate.Hour;
            if (currentServerDate == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(new
                {
                    ServerDate = currentServerDate,
                    ServerHour = currentHours,
                    LocalDate = currentLocalDate,
                    ConvertDate =  inputDateConvert,
                    
                });
            }


        }


    }
}
