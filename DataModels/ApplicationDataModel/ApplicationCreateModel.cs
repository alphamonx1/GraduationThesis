using System;

namespace CAPSTONEPROJECT.DataModels.ApplicationDataModel
{
    public class ApplicationCreateModel
    { 
        public string ApplicationContent { get; set; }
        public int? ApplicationTypeID { get; set; }
        public int? ShiftID { get; set; }
        public string EmployeeID { get; set; }
        public DateTime? ApplyDate {get;set;}
       

    }
}
