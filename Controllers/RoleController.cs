using CAPSTONEPROJECT.DataModels.RoleDataModel;
using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Mvc;

namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _service;
        public RoleController(RoleService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult<RoleResponseModel> GetAll()
        {
            var list = _service.GetAll();
            if (list == null)
            {
                return NoContent();
            }
            return Ok(list);
        }

        [HttpPost]
        public ActionResult<bool> Create([FromBody] RoleCreateModel dataModel)
        {
            bool status = _service.CreateRole(dataModel);
            if (status)
            {
                return Ok("Tạo thành công");
            }
            else
            {   
                return BadRequest("Đã có role này hoặc có lỗi xảy ra khi nhập , hãy thử lại");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<bool> Update(int id, [FromBody] RoleUpdateModel dataModel)
        {
            bool status = _service.UpdateRole(id, dataModel);
            if (status)
            {
                return Ok("Cập nhật thành công");
            }
            else
            {
                return NotFound("Cập nhật thất bại , hãy thử lại");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            bool status = _service.DeleteRole(id);
            if (status)
            {
                return Ok("Xóa thành công");
            }
            else
            {
                return NotFound("Xóa thất bại , hãy thử lại");
            }
        }
    }
}
