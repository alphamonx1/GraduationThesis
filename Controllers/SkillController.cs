using CAPSTONEPROJECT.DataModels.SkillDataModel;
using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Mvc;

namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly SkillService _service;
        public SkillController(SkillService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult<SkillResponseModel> GetAll()
        {
            var list = _service.GetAll();
            if (list == null)
            {
                return NoContent();
            }
            return Ok(list);
        }

        [HttpPost]
        public ActionResult<bool> Create([FromBody] SkillCreateModel dataModel)
        {
            bool status = _service.CreateSkill(dataModel);
            if (status)
            {
                return Ok("Tạo kĩ năng thành công");
            }
            else
            {
                return BadRequest("Kỹ năng đã có hoặc có lỗi xảy ra khi nhập dữ liệu , hãy thử lại");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<bool> Update(string id, [FromBody] SkillUpdateModel dataModel)
        {
            bool? status = _service.UpdateSkill(id, dataModel);
            if (status == true)
            {
                return Ok("Cập nhật thành công");
            }
            else
            {
                return NotFound("Cập nhật thất bại, hãy thử lại");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(string id)
        {
            bool? status = _service.DeleteSkill(id);
            if (status == true)
            {
                return Ok("Xóa kỹ năng thành công");
            }
            else
            {
                return NotFound("Xóa kĩ năng thất bại,  hãy thử lại");
            }
        }

    }
}

