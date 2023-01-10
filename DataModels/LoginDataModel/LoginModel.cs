using System.ComponentModel.DataAnnotations;

namespace CAPSTONEPROJECT.DataModels.LoginDataModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string EmployeeID { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        //    public string DeviceToken { get; set; }
        //}
    }
}
