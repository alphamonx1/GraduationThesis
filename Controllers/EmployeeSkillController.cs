using CAPSTONEPROJECT.DataModels.EmployeeSkillDataModel;
using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Mvc;

namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeSkillController : ControllerBase
    {
        private readonly EmployeeSkillService _service;
        public EmployeeSkillController(EmployeeSkillService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<EmployeeSkillResponseModel> GetAll()
        {
            var list = _service.GetAll();
            if (list == null)
            {
                return NoContent();
            }
            return Ok(list);
        }

        [HttpGet("{id}")]
        public ActionResult<EmployeeSkillResponseModel> GetByID(string id)
        {
            var EmployeeSkill = _service.GetByID(id);
            if(EmployeeSkill == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(EmployeeSkill);
            }
        }

        [HttpPost]
        public ActionResult<bool> Create([FromBody] EmployeeSkillCreateModel dataModel)
        {
            bool status = _service.CreateEmployeeSkill(dataModel);
            if (status)
            {
                return Ok("Tạo thành công");
            }
            else
            {
                
                return BadRequest("Tạo thất bại , hãy thử lại");
            }
        }


        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(string id,string skillID)
        {
            bool status = _service.DeleteEmployeeSkill(id,skillID);
            if (status)
            {
                return Ok("xóa thành công");
            }
            else
            {
                return NotFound("xóa thất bại , hãy thử lại");
            }
        }

    }
}
