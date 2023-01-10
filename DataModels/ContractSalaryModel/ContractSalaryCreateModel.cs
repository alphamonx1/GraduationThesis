using System;

namespace CAPSTONEPROJECT.DataModels.ContractSalaryModel
{
    public class ContractSalaryCreateModel
    {
        public string ContractID { get; set; }
        public string EmployeeID { get; set; }
        public float ContractSalary { get; set; }
        public int? ContractTypeID { get; set; }
        public DateTime? SignDate { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }


    }
}
