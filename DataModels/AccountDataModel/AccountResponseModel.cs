namespace CAPSTONEPROJECT.DataModels.AccountDataModel
{
    public class AccountResponseModel
    {
        public string AccountID { get; set; }
        public string RoleName { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public bool? DelFlag { get; set; }
    }
}
