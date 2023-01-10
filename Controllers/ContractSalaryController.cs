using CAPSTONEPROJECT.DataModels.ContractSalaryModel;
using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Mvc;


namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractSalaryController : ControllerBase
    {
        private readonly ContractSalaryService _service;
        public ContractSalaryController(ContractSalaryService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<ContractSalaryResponseModel> GetAll()
        {
            var list = _service.getAll();
            if (list == null)
            {
                return NoContent();
            }
            return Ok(list);
        }

        [HttpPost]
        public ActionResult<bool> Create([FromBody] ContractSalaryCreateModel dataModel)
        {
            bool status = _service.CreateContractSalaryForEmployee(dataModel);
            if (status)
            {
                return Ok("Tạo hợp đồng thành công");
            }
            else
            {
                return BadRequest("Tạo thất bại , có thể hợp đồng của nhân viên đã có hoặc sai sót khi nhập dữ liệu , hãy thử lại");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<bool> Update(string id, [FromBody] ContractSalaryUpdateModel dataModel)
        {
            bool status = _service.UpdateCTS(id, dataModel);
            if (status)
            {
                return Ok("Cập nhật hợp đồng thành công");
            }
            else
            {
                return NotFound("Cập Nhật hợp đồng thất bại");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(string id)
        {
            bool status = _service.DeleteCTS(id);
            if (status)
            {
                return Ok("Xóa thành công");
            }
            else
            {
                return NotFound("xóa thất bại");
            }
        }



    }
}
