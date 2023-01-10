using CAPSTONEPROJECT.DataModels.AccountDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CAPSTONEPROJECT.Services
{
    public class AccountService
    {
        private readonly LugContext _context;
        private readonly Random _random = new Random();

        public AccountService(LugContext context)
        {
            _context = context;
        }

        public List<AccountResponseModel> GetAll()
        {
            var query = _context.Accounts
                .Select(account => new AccountResponseModel
                {
                    AccountID = account.AccountId,
                    Password = account.Password, 
                    RoleName = _context.Roles.Where(x => x.RoleId == account.RoleId).Select(x => x.RoleName).Single(),
                    DelFlag = account.DelFlag,
                });

            var result = new List<AccountResponseModel>();
            foreach (var item in query)
            {
                item.Password = DeCryptPassword(item.Password);
                if (item.DelFlag != true)
                {
                    
                    result.Add(item);
                }
            }
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public AccountResponseModel GetByID(string id)
        {
            var query = _context.Accounts.Where(x => x.AccountId == id)
                .Select(account => new AccountResponseModel
                {
                    AccountID = account.AccountId,
                    Password = account.Password,
                    RoleName = _context.Roles.Where(x => x.RoleId == account.RoleId).Select(x => x.RoleName).Single(),
                    DelFlag = account.DelFlag,
                }).FirstOrDefault();

            return query;
        }

        public bool CreateAccount(AccountCreateModel dataModel)
        {
            bool status = false;
            try
            {
                var password = RandomPassword();
                var encryptPassword = EncryptPassword(password);
                var account = new Account
                {
                    AccountId = dataModel.AccountID,
                    Password = encryptPassword,
                    RoleId = 4
                };
                if (!AccountExist(account.AccountId))
                {
                    _context.Accounts.Add(account);
                    status = _context.SaveChanges() > 0;
                }
                else
                {

                    status = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
            return status;
        }

        public bool CreateOTP(string EmployeeID)
        {
            bool status = false;
            
            var otp = RandomNumber(0, 1000000).ToString("D6");
            var emp = _context.Accounts.Where(x => x.AccountId == EmployeeID).FirstOrDefault();
            if(emp != null)
            {
                emp.Otp = otp;
            } 
            return status = _context.SaveChanges() > 0;
        }

        public string GetOTP(string EmployeeID)
        {
            var OTP = _context.Accounts.Where(x => x.AccountId == EmployeeID).Select(x=>x.Otp).FirstOrDefault();
            return OTP;
        }

        public bool VerifyOTP(string otp,string EmployeeID)
        {
            bool status = false;
            var emp = _context.Accounts.Where(x => x.AccountId == EmployeeID).FirstOrDefault();
            if(otp == null)
            {
                status = false;
            }
            else if(emp.Otp == otp)
            {
                status = true;
            }

            return status;
        }

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

             
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; 

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public string RandomPassword()
        {
            var passwordBuilder = new StringBuilder();

            
            passwordBuilder.Append(RandomString(4, true));

            
            passwordBuilder.Append(RandomNumber(1000, 9999));

            passwordBuilder.Append(RandomString(2));
            return passwordBuilder.ToString();
        }

        public bool ChangePassword(string id, AccountUpdateModel dataModel)
        {
            bool status = false;
            try
            {
                var account = _context.Accounts.Where(x => x.AccountId == id).FirstOrDefault();
                var newPassword = EncryptPassword(dataModel.Password);
                account.Password = newPassword;
                status = _context.SaveChanges() > 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
            return status;
        }

        public bool ResetPassword(string id, AccountResetModel dataModel)
        {
            bool status = false;
            try
            {
                var account = _context.Accounts.Where(x => x.AccountId == id).FirstOrDefault();
                var newPassword = EncryptPassword(dataModel.Password);
                account.Password = newPassword;
                status = _context.SaveChanges() > 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
            return status;
        }

        public bool VerifyPassword(string VerifyPassword,string EmployeeID)
        {
            var status = false;
            var account = _context.Accounts.Where(x => x.AccountId == EmployeeID).FirstOrDefault();
            var CurrentPassword = DeCryptPassword(account.Password);
            if(VerifyPassword == CurrentPassword)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;

        }

        public bool SetRole(string id,int RoleID)
        {
            bool status = false;
            var account = _context.Accounts.Where(x => x.AccountId == id).FirstOrDefault();
            if(RoleID != 1)
            {
                account.RoleId = RoleID;
                status = _context.SaveChanges() > 0;
            }
            else
            {
                status = false;
            }
            return status;
        }

        public bool DeleteAccount(string id)
        {
            bool status = false;
            try
            {
                var account = _context.Accounts.Where(x => x.AccountId == id).FirstOrDefault();
                _context.Remove(account);
                status = _context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            return status;
        }

        public bool AccountExist(string id)
        {
            return _context.Accounts.Any(x => x.AccountId == id);
        }

        public string EncryptPassword(string strEncrypted)
        {
            byte[] b = Encoding.UTF8.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }

        public string DeCryptPassword(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = Encoding.ASCII.GetString(b);
            }
            catch (Exception)
            {
                decrypted = "";
            }
            return decrypted;
        }
    }
}
