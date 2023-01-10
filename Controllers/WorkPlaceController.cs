using CAPSTONEPROJECT.DataModels.WorkDataModel;
using CAPSTONEPROJECT.Models;
using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Mvc;

using System.Linq;

namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkplaceController : ControllerBase
    {
        private readonly WorkplaceService _service;
        private readonly LugContext _context;

        public WorkplaceController(WorkplaceService service,LugContext context)
        {
            _service = service;
            _context = context;

        }



        [HttpGet]
        public ActionResult<WorkplaceResponse> GetAll()
        {
            var list = _service.GetAll();
            if (list == null)
            {
                return NoContent();
            }
            return Ok(list);
        }

        [HttpGet("id")]
        public ActionResult<WorkplaceResponse> GetByID(string id)
        {
            var wpDetail = _service.GetByID(id);
            if (wpDetail == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(wpDetail);
            }

        }
        [HttpGet("hr/{employeeID}")]
        public ActionResult<WorkplaceEmployeeModel> GetByEmployeeID(string employeeID)
        {
            if (_context.Employees.Any(x=>x.EmployeeId == employeeID))
            {
                var EmpWP = _service.GetByEmployeeID(employeeID);
                if (EmpWP == null)
                {
                    return BadRequest("Nhân viên này chưa thuộc chi nhánh nào");
                }
                else
                {
                    return Ok(EmpWP);
                }
            }
            else
            {
                return BadRequest("Chưa có ID nhân viên này trong hệ thống");
            }
           
        }


        [HttpPost]
        public ActionResult<bool> Create([FromBody] WPCreateModel dataModel)
        {
            bool status = _service.CreateWorkplace(dataModel);
            if (status)
            {
                return Ok("Tạo cửa hàng thành công");
            }
            else
            {   
                return BadRequest("Đã tồn tại cửa hàng trong hệ thống , hãy thử lại");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<bool> Update(string id, [FromBody] WPUpdateModel dataModel)
        {
            bool status = _service.UpdateWorkplace(id, dataModel);
            if (status)
            {
                return Ok("Cập nhật thông tin cửa hàng thành công");
            }
            else
            {
                return NotFound("Cập nhật thất bại , hãy thử lại");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(string id)
        {
            bool status = _service.DeleteWorkPlace(id);
            if (status)
            {
                return Ok("Xóa cửa hàng khỏi hệ thống thành công");
            }
            else
            {
                return NotFound("Xõa thất bại , hãy thử lại");
            }
        }
    }
}
