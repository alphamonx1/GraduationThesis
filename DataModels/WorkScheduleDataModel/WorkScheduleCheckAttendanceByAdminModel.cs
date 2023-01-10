using System;

namespace CAPSTONEPROJECT.DataModels.WorkScheduleDataModel
{
    public class WorkScheduleCheckAttendanceByAdminModel
    {
        public int WorkScheduleStatusID { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        
    }
}
