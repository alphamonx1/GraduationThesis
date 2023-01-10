using System;

namespace CAPSTONEPROJECT.DataModels.WorkScheduleDataModel
{
    public class WorkScheduleUpdateModel
    {
        public string EmployeeID { get; set; }
        public int ShiftID { get; set; }
        public DateTime? WorkingDate { get; set; }
        public string WorkplaceID { get; set; }
        public bool? DelFlag { get; set; }
    }
}
