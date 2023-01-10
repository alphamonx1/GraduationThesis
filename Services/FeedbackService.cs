using CAPSTONEPROJECT.DataModels.FeedbackDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.Services
{
    public class FeedbackService
    {
        private readonly LugContext _context;
        public FeedbackService(LugContext context)
        {
            _context = context;
        }
        
        public List<FeedbackResponseModel> GetAll()
        {
            var query = _context.CheckAttendanceFeedbacks
                .Select(feedback => new FeedbackResponseModel
                {
                    FeedbackID = feedback.FeedbackId,
                    EmployeeID = feedback.EmployeeId,
                    Fullname = _context.Employees.Where(x => x.EmployeeId == feedback.EmployeeId).Select(x => x.FullName).FirstOrDefault(),
                    CheckHours =  feedback.CheckTime.Value.Hours,
                    CheckMinutes = feedback.CheckTime.Value.Minutes,
                    Reason = feedback.Reason,
                    WorkingDate = (DateTime) feedback.WorkingDate,

                });
            var result = new List<FeedbackResponseModel>();
            foreach (var item in query)
            {
                result.Add(item);
            }
            return result;
        }

        public List<FeedbackResponseModel> GetByMonth(int month,int year)
        {
            var query = _context.CheckAttendanceFeedbacks.Where(x=>x.WorkingDate.Value.Month==month && x.WorkingDate.Value.Year == year)
                .Select(feedback => new FeedbackResponseModel
                {
                    FeedbackID = feedback.FeedbackId,
                    EmployeeID = feedback.EmployeeId,
                    Fullname = _context.Employees.Where(x=>x.EmployeeId == feedback.EmployeeId).Select(x=>x.FullName).FirstOrDefault(),
                    CheckHours = feedback.CheckTime.Value.Hours,
                    CheckMinutes = feedback.CheckTime.Value.Minutes,
                    Reason = feedback.Reason,
                    WorkingDate = (DateTime)feedback.WorkingDate,

                }).OrderByDescending(x=>x.WorkingDate);
            var result = new List<FeedbackResponseModel>();
            foreach (var item in query)
            {
                result.Add(item);
            }
            return result;
        }

        public FeedbackResponseModel GetByID(int FeedbackID)
        {
            var query = _context.CheckAttendanceFeedbacks.Where(x=>x.FeedbackId == FeedbackID)
                .Select(feedback => new FeedbackResponseModel
                {
                    FeedbackID = feedback.FeedbackId,
                    EmployeeID = feedback.EmployeeId,
                    Fullname = _context.Employees.Where(x => x.EmployeeId == feedback.EmployeeId).Select(x => x.FullName).FirstOrDefault(),
                    Image = Convert.ToBase64String(feedback.Image),
                    CheckHours = feedback.CheckTime.Value.Hours,
                    CheckMinutes = feedback.CheckTime.Value.Minutes,
                    Reason = feedback.Reason,
                    WorkingDate = (DateTime)feedback.WorkingDate,
                }).FirstOrDefault();

            return query;
        }

        public bool SendFeedback(FeedbackCreateModel dataModel)
        {
            bool status = false;
            DateTime currentServerDate = DateTime.Now;
            DateTime currentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentServerDate, "SE Asia Standard Time");
            var TimeOccur = TimeSpan.Parse(dataModel.TimeOccur);

            var Image = Convert.FromBase64String(dataModel.Image);

            try
            {
                var feedback = new CheckAttendanceFeedback
                {
                    EmployeeId = dataModel.EmployeeID,
                    Image =  Image,
                    CheckTime = TimeOccur,
                    WorkingDate = currentDate.Date,
                    Reason = dataModel.Reason,

                };

                _context.CheckAttendanceFeedbacks.Add(feedback);
                status = _context.SaveChanges() > 0;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }

            return status;


        }




    }
}
