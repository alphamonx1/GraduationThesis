using System;

namespace CAPSTONEPROJECT.DataModels.EmpDataModel
{
    public class EmpCreateModel
    {
        public string EmployeeID { get; set; }
        public string ProfileImage { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Ethnic { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public string PermanentAddress { get; set; }
        public string TemporaryAddress { get; set; }
        public string WorkplaceID { get; set; }
        public string PositionID { get; set; }
        public DateTime? IDCardDateOfIssue { get; set; }
        public string IDCardPlaceOfIssue { get; set; }
    }
}
