using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.DataModels.ReportEmployeeWorkStatusModel
{
    public class StoreWorkingStatusModel
    {
        public int TotalWorkingEmployee { get; set; }
        public int TotalLateEmployee { get; set; }
        public int TotalOffEmployee { get; set; }
        public int TotalAbsentEmployee { get; set; }
    }
}
