using System;

namespace CAPSTONEPROJECT.DataModels.ApplicationDataModel
{
    public class ApplicationResponseModel
    {
        public int ApplicationID { get; set; }
        public string Fullname { get; set; }
        public string EmployeeID { get; set; }
        public string ApplicationContent { get; set; }
        public string ApplicationType { get; set; }
        public DateTime? ApplicationMakingDate { get; set; }
        public DateTime? ApplyDate { get; set; }
        public int? ShiftID { get; set; }
        public string ShiftName { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? LastReviewByAboveDate { get; set; }
        public int? ApplicationStatusID { get; set; }
        public string Reason { get; set; }
        public int? TotalOffDayRemain { get; set; }
    }
}
