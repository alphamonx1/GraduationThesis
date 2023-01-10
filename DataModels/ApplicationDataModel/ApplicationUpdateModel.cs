using System;

namespace CAPSTONEPROJECT.DataModels.ApplicationDataModel
{
    public class ApplicationUpdateModel
    {
        public string ApplycationContent { get; set; }
        public int? ApplicationTypeID { get; set; }
        public DateTime? ApplyDate { get; set; }
        public int? ShiftID { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
