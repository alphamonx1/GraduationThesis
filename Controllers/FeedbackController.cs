using CAPSTONEPROJECT.DataModels.FeedbackDataModel;
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
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _service;
        public FeedbackController(FeedbackService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<FeedbackResponseModel> GetAll()
        {
            var list = _service.GetAll();
            if(list != null)
            {
                return Ok(list);
            }
            else
            {
                return BadRequest("Không có dữ liệu");
            }
        }
        [HttpGet("manager/{month}/{year}")]
        public ActionResult<FeedbackResponseModel> GetByMonth(int month,int year)
        {
            var list = _service.GetByMonth(month,year);
            if (list != null)
            {
                return Ok(list);
            }
            else
            {
                return BadRequest("Không có dữ liệu");
            }
        }

        [HttpGet("FeedbackID")]
        public ActionResult<FeedbackResponseModel> GetByID(int FeedbackID)
        {
            var fb = _service.GetByID(FeedbackID);
            if (fb != null)
            {
                return Ok(fb);
            }
            else
            {
                return BadRequest("Không có dữ liệu");
            }
        }


        [HttpPost]
        public ActionResult<bool> SendFeedback(FeedbackCreateModel dataModel)
        {
            var status = _service.SendFeedback(dataModel);
            if (status)
            {
                return Ok("Gửi khiếu nại thành công");
            }
            else
            {
                return BadRequest("Gủi khiếu nại thất bại, hãy thử lại");
            }
        }

    }
}
