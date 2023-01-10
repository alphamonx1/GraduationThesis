using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.DataModels.FeedbackDataModel
{
    public class FeedbackCreateModel
    {
        public string Image { get; set; }
        public string EmployeeID { get; set; }
        public string TimeOccur { get; set; }
        public string Reason { get; set; }
        
    }
}
