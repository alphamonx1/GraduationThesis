using System;

namespace CAPSTONEPROJECT.DataModels.ContractSalaryModel
{
    public class ContractSalaryUpdateModel
    { 
        public double? ContractSalary { get; set; }
        public DateTime? SignDate { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
    }
}
