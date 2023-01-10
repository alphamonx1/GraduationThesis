using System;

namespace CAPSTONEPROJECT.DataModels.ContractSalaryModel
{
    public class ContractSalaryResponseModel
    {
        public string ContractID { get; set; }
        public string EmployeeID { get; set; }
        public string Fullname { get; set; }
        public double? ContractSalary { get; set; }
        public int? ContractTypeID { get; set; }
        public int? BasiscWorkingTime { get; set; }
        public DateTime? SignDate { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }

    }
}
