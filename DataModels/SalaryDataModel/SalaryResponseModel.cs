namespace CAPSTONEPROJECT.DataModels.SalaryDataModel
{
    public class SalaryResponseModel
    {
        public int SalaryID { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int? BasicWorkingTime { get; set; }
        public double? RealWorkingTime { get; set; }
        public double? ContractSalary { get; set; }
        public double? Income { get; set; }
        public string Notes { get; set; }
        public int? EmployeeTypeID { get; set; }

    }
}
