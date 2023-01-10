using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.DataModels.TotalDayDataModel
{
    public class TotalDayResponseModel
    {
        public double TotalWorkTimeInMonth { get; set; }
        public double TotalWorkedTime { get; set; }
        public int TotalAbsentDay { get; set; }
        public int TotalLateDay { get; set; }
        public int EmployeeTypeID { get; set; }
    }
}
