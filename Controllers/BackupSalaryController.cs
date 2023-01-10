using CAPSTONEPROJECT.DataModels.BackupSalaryDataModel;
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
    public class BackupSalaryController : ControllerBase
    {
        private readonly BackupSalaryService _service;

        public BackupSalaryController(BackupSalaryService service)
        {
            _service = service;
        }

        [HttpGet("SalaryID")]
        public ActionResult<BackupSalaryResponseModel> GetListByID(int SalaryID)
        {
            var List = _service.GetListByID(SalaryID);
            if(List != null)
            {
                return Ok(List);
            }
            else
            {
                return NoContent();
            }

        }

        [HttpDelete]
        public ActionResult<bool> DeleteByID(int id , int SalaryID)
        {
            var status = _service.DeleteBackupSalary(id, SalaryID);
            if (status)
            {
                return Ok("Xóa lịch sử thành công");
            }
            else
            {
                return BadRequest("Xóa lịch sử thất bại");
            }
        }


    }
}
