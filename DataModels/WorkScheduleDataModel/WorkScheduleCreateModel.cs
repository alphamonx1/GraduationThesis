using System;
using System.Collections.Generic;

namespace CAPSTONEPROJECT.DataModels.WorkScheduleDataModel
{
    public class WorkScheduleCreateModel
    {
        public string EmployeeID { get; set; }
        public int? ShiftID { get; set; }
        public List<DateTime> ListWorkingDate { get; set; }
        public string WorkplaceID { get; set; }
    }
}
