namespace CAPSTONEPROJECT.DataModels.ReportEmployeeWorkStatusModel
{
    public class EmployeeWorkInformationModel
    {
        public string EmployeeID { get; set; }
        public string Fullname { get; set; }
        public int TotalDayToWork { get; set; }
        public int TotalDayWorked { get; set; }
        public int TotalAbsentDay { get; set; }
        public int TotalLateDay { get; set; }
    }
}
