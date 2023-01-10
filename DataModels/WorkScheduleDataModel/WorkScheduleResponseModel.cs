using System;

namespace CAPSTONEPROJECT.DataModels.WorkScheduleDataModel
{
    public class WorkScheduleResponseModel
    {
        public int WorkScheduleID { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string ShiftName { get; set; }
        public int? ShiftStartTimeHours { get; set; }
        public int? ShiftStartTimeMinute { get; set; }
        public int? ShiftEndTimeHours { get; set; }
        public int? ShiftEndTimeMinute { get; set; }
        public string WorkScheduleStatus { get; set; }
        public DateTime? WorkingDate { get; set; }
        public string Address { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public double? WorkHours { get; set; }
        
        
    }
}
