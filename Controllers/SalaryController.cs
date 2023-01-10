using CAPSTONEPROJECT.DataModels.SalaryDataModel;
using CAPSTONEPROJECT.Models;
using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;

namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly SalaryService _service;
        private readonly LugContext _context;
        public SalaryController(SalaryService service, LugContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public ActionResult<SalaryResponseModel> GetAll()
        {
            var list = _service.GetAll();
            if (list == null)
            {
                return NoContent();
            }
            return Ok(list);
        }


        [HttpGet("{month}/{year}")]
        public ActionResult<SalaryResponseModel> GetByMonthYear(int month, int year)
        {
            var list = _service.GetByMonthYear(month,year);
            if (list == null)
            {
                return NoContent();
            }
            return Ok(list);
        }

        [HttpGet("{employeeID}/{month}/{year}")]
        public ActionResult<SalaryResponseModel> GetByID(string employeeID, int month, int year)
        {
            var slr = _service.GetSalaryDetaiLByMonthAndYear(employeeID, month, year);
            if (slr == null)
            {
                return NoContent();
            }
            return Ok(slr);
        }

        [HttpGet("employee/{employeeID}")]
        public ActionResult<SalaryResponseModel> GetSalaryMonthBefore(string employeeID)
        {
            var slr = _service.GetMonthBeforeSalary(employeeID);
            if (slr == null)
            {
                return NoContent();
            }
            return Ok(slr);
        }

        [HttpPost("calculate")]
        public ActionResult<bool> Calculate()
        {
            DateTime CurrentServerTime = DateTime.Now;
            DateTime CurrentLocalTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(CurrentServerTime, "SE Asia Standard Time");

     
            bool InSalaryCalculatePeriod = _service.InSalaryCalculatePeriod(CurrentLocalTime);
            

            if (InSalaryCalculatePeriod)
            {
                    bool status = _service.CalculateIncomeForEmployee();
                    if (status)
                    {
                        return Ok("Tính lương tháng" + " " + (CurrentLocalTime.Month-1) + " thành công");
                    }
                    else
                    {
                        return BadRequest("Tính lương tháng " + " " + (CurrentLocalTime.Month-1) + " thất bại ");
                    }
            }
            else
            {
                return BadRequest("Chưa tới kì tính lương");
            }
            

            
        }

        [HttpPut("{id}")]
        public ActionResult<bool> Update(int id, [FromBody] SalaryUpdateModel dataModel)
        {
            DateTime CurrentServerTime = DateTime.Now;
            DateTime CurrentLocalTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(CurrentServerTime, "SE Asia Standard Time");

            var salary = _context.Salaries.Where(x => x.SalaryId == id).FirstOrDefault();
            var FullName = _context.Employees.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.FullName).FirstOrDefault();

            var InPeriod = _service.InSalaryUpdatePeriod(CurrentLocalTime);
            if (InPeriod)
            {
                bool status = _service.UpdateSalaryForEmployee(id, dataModel);
                if (status)
                {
                    return Ok("Đã cập nhật lương tháng" + " " + salary.Month + " " + " của nhân viên " + " " + FullName + " " + " thành công");
                }
                else
                {
                    return NotFound("Đã cập nhật lương tháng" + " " + salary.Month + " " + " của nhân viên " + " " + FullName + " " + " thất bại");
                }
            }
            else
            {
                return BadRequest("Không trong thời gian được cập nhật lương");
            }

        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            var salary = _context.Salaries.Where(x => x.SalaryId == id).FirstOrDefault();
            var FullName = _context.Employees.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.FullName).FirstOrDefault();

            bool status = _service.DeleteSalaryForEmployee(id);
            if (status)
            {
                return Ok("Đã xóa lương tháng" + " " + salary.Month + " " + " của nhân viên " + " " + FullName + " " + " thành công");
            }
            else
            {
                return NotFound("Đã xóa lương tháng" + " " + salary.Month + " " + " của nhân viên " + " " + FullName + " " + " thất bại");
            }
        }



    }
}
