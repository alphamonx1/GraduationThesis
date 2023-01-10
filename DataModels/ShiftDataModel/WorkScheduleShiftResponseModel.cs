using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.DataModels.ShiftDataModel
{
    public class WorkScheduleShiftResponseModel
    {
        public int ShiftID { get; set; }
        public string ShiftName { get; set; }
        public int ShiftStartHours { get; set; }
        public int ShiftStartMinutes { get; set; }
        public int ShiftEndHours { get; set; }
        public int ShiftEndMinutes { get; set; }

    }
}
