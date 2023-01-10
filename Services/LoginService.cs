using CAPSTONEPROJECT.DataModels.LoginDataModel;
using CAPSTONEPROJECT.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CAPSTONEPROJECT.Services
{
    public class LoginService
    {
        private readonly LugContext _context;
        private readonly AccountService _service;
        private readonly IConfiguration _config;

        public LoginService(LugContext context, IConfiguration config, AccountService service)
        {
            _context = context;
            _config = config;
            _service = service;

        }

        public string GenerateJsonWebToken(UserResponseModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("employeeID",userInfo.EmployeeID),
                new Claim("roleName",userInfo.RoleName),
                new Claim("fullName",userInfo.Fullname),
                new Claim("workplaceid",userInfo.WorkplaceID),

                new Claim(JwtRegisteredClaimNames.Exp ,$"{new DateTimeOffset(DateTime.Now.AddHours(24)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
            };


            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                notBefore: DateTime.Now,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        public UserResponseModel AuthenticatedUser(LoginModel login)
        {
            var account = _context.Accounts.Where(x => x.AccountId == login.EmployeeID && x.Password ==  _service.EncryptPassword(login.Password))
                .Select(user => new UserResponseModel
                {
                    EmployeeID = user.AccountId,
                    Fullname = _context.Employees.Where(x=>x.EmployeeId == user.AccountId).Select(x=>x.FullName).FirstOrDefault(),
                    RoleName = _context.Roles.Where(x => x.RoleId == user.RoleId).Select(x => x.RoleName).FirstOrDefault(),
                    WorkplaceID = _context.Employees.Where(x=>x.EmployeeId == user.AccountId).Select(x=>x.WorkplaceId).FirstOrDefault(),
                    EmployeeTypeID = _context.Employees.Where(x => x.EmployeeId == user.AccountId).Select(x => x.EmployeeTypeId).FirstOrDefault(),

                }).FirstOrDefault();


            return account;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("12518290asfdajh124512-58asdfasdjasd12419asdamsdn12414981a")),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public DateTime GetExpiredDateFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var JWTToken = tokenHandler.ReadToken(token);

            return JWTToken.ValidTo;
        }

    }
}

