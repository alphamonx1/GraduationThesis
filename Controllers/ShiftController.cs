using CAPSTONEPROJECT.DataModels.ShiftDataModel;
using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Mvc;

using System;

namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly ShiftService _service;
        
        public ShiftController(ShiftService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<ShiftResponseModel> GetAll()
        {
            var list = _service.GetAll();
            if (list == null)
            {
                return NoContent();
            }
            return Ok(list);
        }

        [HttpGet("application")]
        public ActionResult<WorkScheduleShiftResponseModel> GetByWorkingDate(string EmployeeID,DateTime WorkingDate)
        {
            
            var list = _service.GetByWorkingDate(EmployeeID,WorkingDate);
            if(list != null)
            {
                return Ok(list);
            }
            else
            {
                return NotFound("Không tìm thấy ca nào trong ngày đã chọn");
            }

        }


        [HttpPost]
        public ActionResult<bool> Create([FromBody] ShiftCreateModel dataModel)
        {
            bool status = _service.CreateShift(dataModel);
            if (status)
            {
                return Ok("Tạo ca thành công");
            }
            else
            {
                
                return BadRequest("Ca đã tồn tại , hoặc có lỗi trong nhập dữ liệu , hãy thử lại");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<bool> Update(int id, [FromBody] ShiftUpdateModel dataModel)
        {
            bool status = _service.UpdateShift(id, dataModel);
            if (status)
            {
                return Ok("Cập nhật ca thành công");
            }
            else
            {
                return NotFound("Cập nhật ca thất bại, hãy thử lại");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            bool status = _service.DeleteShift(id);
            if (status)
            {
                return Ok("Xóa ca thành công");
            }
            else
            {
                return NotFound("Xóa ca thất bại , hãy thử lại");
            }
        }

    }
}
