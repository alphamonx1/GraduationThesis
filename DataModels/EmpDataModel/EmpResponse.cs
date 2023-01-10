using System;

namespace CAPSTONEPROJECT.DataModels.EmpDataModel
{
    public class EmpResponse
    {
        public string EmployeeID { get; set; }
        public string ProfileImage{ get; set; }
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
        public string Workplace { get; set; }
        public string EmployeeTypeName { get; set; }
        public string Position { get; set; }
        public DateTime? IDCardDateOfIssue { get; set; }
        public string IDCardPlaceOfIssue { get; set; }
        public bool? DelFlag { get; set; }

    }
}
