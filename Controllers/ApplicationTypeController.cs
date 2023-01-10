using CAPSTONEPROJECT.DataModels.ApplicationTypeDataModel;
using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Mvc;

namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationTypeController : ControllerBase
    {
        private readonly ApplicationTypeService _service;

        public ApplicationTypeController(ApplicationTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<ApplicationTypeResponseModel> GetAll()
        {
            var list = _service.GetAll();
            if (list != null)
            {
                return Ok(list);

            }
            else
            {
                return NoContent();
            }


        }

        [HttpPost]
        public ActionResult<bool> Create([FromBody] ApplicationTypeCreateModel dataModel)
        {
            bool status = _service.CreateApplicationType(dataModel);
            if (status == true)
            {
                return Ok("Tạo thành công");
            }
            else
            {
                return BadRequest("Tạo thất bại loại đơn vừa tạo có thể đã tồn tại hoặc đã có sai sót khi nhập thông tin , hãy thử lại");
            }

        }


    }
}
