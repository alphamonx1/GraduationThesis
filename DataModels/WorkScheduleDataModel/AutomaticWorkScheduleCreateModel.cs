using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.DataModels.WorkScheduleDataModel
{
    public class AutomaticWorkScheduleCreateModel
    {
        public List<string> ListEmployeeID { get; set; }
        public string WorkplaceID { get; set; }
        public int ShiftID { get; set; }
        public bool NoSaturday { get; set; }
        public bool NoSunday { get; set; }

    }
}
