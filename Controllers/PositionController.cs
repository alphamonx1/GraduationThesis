using CAPSTONEPROJECT.DataModels.PositionDataModel;
using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Mvc;

namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly PositionService _service;

        public PositionController(PositionService service)
        {
            _service = service;
        }
        // GET: api/<PositionController>
        [HttpGet]
        public ActionResult<PositionResponse> GetAll()
        {
            var list= _service.getAll();
            if (list == null)
            {
                return NoContent();
            }
            return Ok(list);
        }

        [HttpPost]
        public ActionResult<bool> Create([FromBody] PosCreateModel dataModel)
        {
            bool status = _service.CreatePosition(dataModel);
            if (status)
            {
                return Ok("Tạo thành công");
            }
            else
            {
                return BadRequest("Đã có vị trí này hoặc có sai sót trong khi nhập , hãy thử lại");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<bool> Update(string id, [FromBody] PosUpdateModel dataModel)
        {
            bool status = _service.UpdatePos(id, dataModel);
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
        public ActionResult<bool> Delete(string id)
        {
            bool status = _service.DeletePos(id);
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
