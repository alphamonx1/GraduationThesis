using CAPSTONEPROJECT.DataModels.SystemSettingDataModel;
using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemSettingController : ControllerBase
    {
        private readonly SystemSettingService _service;

        public SystemSettingController(SystemSettingService service)
        {
            _service = service;
        }

        [HttpGet("SystemSettingID")]
        public ActionResult<SystemSettingResponseModel> GetByID()
        {
            var setting = _service.GetByID();
            if(setting != null)
            {
                return Ok(setting);
            }
            else
            {
                return BadRequest("Không tìm thấy cài đặt nào trong hệ thống");
            }


        }

        [HttpPut("SystemSettingID")]
        public ActionResult<bool> UpdateSetting(int SystemSettingID,[FromBody] SystemSettingUpdateModel dataModel)
        {
            var status = _service.UpdateSetting(SystemSettingID, dataModel);
            if (status)
            {
                return Ok("Cập nhật cài đặt hệ thống thành công");
            }
            else
            {
                return BadRequest("Cập nhật cài đặt thất bại");
            }



        }

    }
}
