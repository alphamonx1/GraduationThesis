using CAPSTONEPROJECT.DataModels.WorkScheduleDataModel;
using CAPSTONEPROJECT.Models;
using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkScheduleController : ControllerBase
    {
        private readonly WorkScheduleService _service;
        private readonly LugContext _context;
        public WorkScheduleController(WorkScheduleService service, LugContext context)
        {
            _service = service;
            _context = context;
        }
        [HttpGet]
        public ActionResult<WorkScheduleResponseModel> GetAll()
        {
            var list = _service.GetAll();
            if (list == null)
            {
                return NoContent();
            }
            return Ok(list);
        }

        [HttpGet("manager/{month}/{year}")]
        public ActionResult<WorkScheduleResponseModel> GetByMonth(int month,int year)
        {
            var list = _service.GetByMonthYear(month,year);
            if(list != null)
            {
                return Ok(list);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{employeeID}/{WorkingDate}")]
        public ActionResult<WorkScheduleResponseModel> GetByIDAndDate(string employeeID, DateTime WorkingDate)
        {
            var list = _service.GetByIDAndDate(employeeID, WorkingDate);
            if (list == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(list);
            }


        }

        [HttpGet("remain/{employeeID}/{WorkingDate}")]
        public ActionResult<WorkScheduleResponseModel> GetRemainWS(string employeeID, DateTime WorkingDate)
        {
            var remain = _service.GetRemainWS(employeeID, WorkingDate);
            if (remain == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(remain);
            }
        }

        [HttpGet("manager/{WorkplaceID}")]
        public ActionResult<WorkScheduleResponseModel> GetByWorkSchedule(string WorkplaceID)
        {
            var list = _service.GetByWorkplace(WorkplaceID);
            if (list == null)
            {
                return BadRequest("No record found");
            }
            else
            {
                return Ok(list);
            }


        }

        [HttpPost("manager")]
        public ActionResult<bool> Create([FromBody] WorkScheduleCreateModel dataModel)
        {
            bool status = _service.CreateWS(dataModel);
            if (status)
            {
                return Ok("Tạo lịch làm việc thành công");
            }
            else
            {
                return BadRequest("Nhân viên này đã có lịch làm việc hoặc có lỗi trong dữ liệu nhập , hãy thử lại");
            }
        }

        [HttpPost("manager/automatic")]
        public ActionResult<bool> AutomaticCreate([FromBody] AutomaticWorkScheduleCreateModel dataModel)
        {
            bool status = _service.AutomaticCreateWS(dataModel);
            if (status)
            {
                return Ok("Tạo lịch làm việc tháng này thành công");
            }
            else
            {
                return BadRequest("Nhân viên này đã có lịch làm việc tháng này hoặc có lỗi trong dữ liệu nhập , hãy thử lại");
            }
        }


        [HttpPut("manager/update/{id}")]
        public ActionResult<bool> Update(int id, [FromBody] WorkScheduleUpdateModel dataModel)
        {
            DateTime CurrentServerDate = DateTime.Now;
            DateTime CurrentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(CurrentServerDate, "SE Asia Standard Time");




            if (CurrentDate.Date == dataModel.WorkingDate)
            {
                return BadRequest("Không thể thay đổi lịch làm việc lúc này");
            }
            else
            {
                bool status = _service.UpdateWS(id, dataModel);
                if (status)
                {
                    return Ok("Cập nhật lịch làm việc thành công");
                }
                else
                {
                    return BadRequest("Cập nhật lịch làm việc thất bại, hãy thử lại");
                }
            }


        }

        [HttpPut("manager/checkattendance/{id}")]
        public ActionResult<bool> CheckAttendanceByAdmin(int id, [FromBody] WorkScheduleCheckAttendanceByAdminModel dataModel)
        {
            DateTime CurrentServerDate = DateTime.Now;
            DateTime CurrentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(CurrentServerDate, "SE Asia Standard Time");

            var Shift = _context.WorkSchedules.Where(x => x.WorkScheduleId == id).Select(x => x.ShiftId).FirstOrDefault();
            var ShiftStartTime = _context.Shifts.Where(x => x.ShiftId == Shift).Select(x => x.StartTime).FirstOrDefault();


            bool status = _service.CheckAttendanceByAdmin(id, dataModel);
            if (status)
            {
                return Ok("Điểm danh thành công");
            }
            else
            {
                return NotFound("Không tìm thấy lịch làm việc");
            }




        }


        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            bool status = _service.DeleteWS(id);
            if (status)
            {
                return Ok("Xóa lịch làm việc thành công");
            }
            else
            {
                return NotFound("Xóa lịch làm việc thất bại");
            }
        }
    }
}
