using System.ComponentModel.DataAnnotations;

namespace CAPSTONEPROJECT.DataModels.AccountDataModel
{
    public class AccountUpdateModel
    {
        public string EmployeeID { get; set; }
        [Required]
        public string VerifyPassword { get; set; }
        public string Password { get; set; }
    }
}
