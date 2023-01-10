using CAPSTONEPROJECT.DataModels.EmpDataModel;
using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EMPController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EMPController(EmployeeService service)
        {
            _service = service;
        }

        // GET: api/<EMPController>
        [HttpGet]
        public ActionResult<EmpResponse> GetAll()
        {
            var list = _service.GetAll();
            if (list == null)
            {
                return NoContent();
            }
            return Ok(list);
        }

        // GET api/<EMPController>/5
        [HttpGet("{id}")]
        public ActionResult<EmpResponse> GetByID(string id)
        {
            var empByID = _service.GetByID(id);
            if(empByID == null)
            {
                return NoContent();
            }
            return Ok(empByID);

        }

        [HttpGet("manager/{WorkplaceID}")]
        public ActionResult<EmpResponse> GetByWorkplace(string WorkplaceID)
        {
            var empByWorkplace = _service.GetByWorkplace(WorkplaceID);
            if (empByWorkplace == null)
            {
                return NoContent();
            }
            return Ok(empByWorkplace);

        }

        [HttpGet("manager/count/{WorkplaceID}")]
        public ActionResult<int> CountByWorkplace(string WorkplaceID)
        {
            var result = _service.CountByWorkplace(WorkplaceID);
            if (result < 0)
            {
                return BadRequest("Có sai sót khi nhập dữ liệu , thử lại");
            }
            else
            {
                return Ok(result);
            }
        }

        // POST api/<EMPController>
        [HttpPost]
        public ActionResult<bool> Create([FromBody] EmpCreateModel dataModel)
        {
            bool status = _service.CreateEmp(dataModel);
            if(status)
            {
                return Ok("Nhập hồ sơ nhân viên thành công");
            }
            else
            {
                return BadRequest("Hồ sơ nhân viên đã tồn tại hoặc có sai sót khi nhập dữ liệu, hay thử lại");
            }
        }

        //[HttpPost("ReadFile")]
        //public ActionResult<bool> Import(IFormFile file)
        //{
        //    var status = _service.ImportEmpByExcel(file);
        //    if (status)
        //    {
        //        return Ok("Nhập dữ liệu nhân viên thành công");
        //    }
        //    else
        //    {
        //        return BadRequest("File excel không hợp lệ");
        //    }

        //}


        // PUT api/<EMPController>/5
        [HttpPut("{id}")]
        public ActionResult<bool> Update(string id, [FromBody] EmpUpdateModel dataModel)
        {
            bool status = _service.UpdateEmp(id, dataModel);
            if(status)
            {
                return Ok("Cập nhật hồ sơ nhân viên thành công");
            }
            else
            {
                return NotFound("Cập nhật hồ sơ thất bại, hãy thử lại");
            }
        }

        // DELETE api/<EMPController>/5
        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteByID(string id)
        {
            bool status = _service.ChangeEmpStatus(id);
            if(status)
            {
                return Ok("Đổi trạng thái nhân viên thành công");
            }
            else
            {
                return NotFound("đổi trạng thái nhân viên  thất bại , hãy thử lại");
            }
        }
    }
}
