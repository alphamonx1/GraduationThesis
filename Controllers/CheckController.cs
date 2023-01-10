using CAPSTONEPROJECT.DataModels.CheckDataModel;
using CAPSTONEPROJECT.Services;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CAPSTONEPROJECT.DataModels.FeedbackDataModel;
using System;

namespace CAPSTONEPROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckController : ControllerBase
    {
        private readonly CheckService _service;
        private readonly FeedbackService _fbservice;
        public CheckController(CheckService service,FeedbackService fbservice)
        {
            _service = service;
            _fbservice = fbservice;

        }

        [HttpPost("faceRe/")]
        public async Task<ActionResult<CheckResponseModel>> GetByIDAsync([Required][FromBody] CheckFaceModel dataModel)
        {
            var url = "https://lugvn.ap.ngrok.io/face/";
            var client = new HttpClient();
            string MyJson = JsonConvert.SerializeObject(new
            {
                image = dataModel.ImageUrl,
            });

            DateTime currentServerDate = DateTime.Now;
            DateTime currentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentServerDate, "SE Asia Standard Time");

            var content = new StringContent(MyJson, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await client.PostAsync(url, content);

            if (responseMessage.IsSuccessStatusCode)
            {
                var id = responseMessage.Content.ReadAsStringAsync();
                string json = id.Result.ToString();
                JObject jobject = JObject.Parse(json);
                string employeeID = (string)jobject.SelectToken("id");

                TimeSpan timeOccur = new(currentDate.Hour, currentDate.Minute,currentDate.Second);

                

                if (employeeID == "Unknown")
                {
                    return BadRequest("Không tìm thấy nhân viên trong hệ thống");
                }
                else
                {
                    var EmpResponse = _service.GetByID (employeeID, dataModel.BSSID);
                    var feedback = new FeedbackCreateModel
                    {
                        EmployeeID = employeeID,
                        Image = dataModel.ImageUrl,
                        Reason = "Điểm danh",
                        TimeOccur = timeOccur.ToString(),
                    };
                    _fbservice.SendFeedback(feedback);
                    return Ok(new
                    {
                        EmpResponse
                    });
                }
                
            }
            else
            {
                return BadRequest("không nhân diện được khuôn mặt");
            }        
        }
            
        [HttpPost]
        public ActionResult<bool> CheckingAttendance([FromBody] CheckConfirmModel dataModel)
        {

            var status = _service.CheckConfirm(dataModel);
            if (status == true)
            {
                return Ok("Điểm danh thành công");
            }
            else
            {
                return BadRequest("Bạn không có lịch làm việc");
            }
        }



    }
}
