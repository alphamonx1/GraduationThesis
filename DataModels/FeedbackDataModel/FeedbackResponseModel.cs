using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.DataModels.FeedbackDataModel
{
    public class FeedbackResponseModel
    {
        public int FeedbackID { get; set; }
        public string EmployeeID { get; set; }
        public string Fullname { get; set; }
        public string Image { get; set; }
        public string Reason { get; set; }
        public int CheckHours { get; set; }
        public int CheckMinutes { get; set; }
        public DateTime WorkingDate { get; set; }


    }
}
