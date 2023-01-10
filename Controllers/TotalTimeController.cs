using CAPSTONEPROJECT.DataModels.TotalDayDataModel;
using CAPSTONEPROJECT.Models;
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
    public class TotalTimeController : ControllerBase
    {
        private readonly TotalDayService _service;
        private readonly LugContext _context;
        public TotalTimeController(TotalDayService service, LugContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public ActionResult<TotalDayResponseModel> GetTotalTime(string EmployeeID, int month , int year)
        {
            var contractexist = _context.ContractSalaries.Any(x => x.EmployeeId == EmployeeID);

            if (contractexist)
            {
                var total = _service.CountEmployeeTotalDay(EmployeeID, month, year);
                if (total != null)
                {
                    return Ok(total);
                }
                else
                {
                    return BadRequest("Nhân viên này chưa có dữ liệu làm việc");
                }
            }
            else
            {
                return BadRequest("Chưa tồn tại hợp đồng nhân viên");
            }
         
        }

        [HttpGet("off")]
        public ActionResult<TotalOffResponseModel> GetOffDayRemain(string EmployeeID)
        {
            var offDayRemain = _service.CountEmployeeTotalOffDayRemain(EmployeeID);
            if(offDayRemain != null)
            {
                return Ok(offDayRemain);
            }
            else
            {
                return BadRequest("Không tìm thấy ngày nghỉ còn lại của nhân viên này");
            }
        }

    }
}
