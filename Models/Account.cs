using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("Account")]
    public partial class Account
    {
        [Key]
        [Column("AccountID")]
        [StringLength(20)]
        public string AccountId { get; set; }
        [Column("RoleID")]
        public int? RoleId { get; set; }
        [Column("password")]
        [StringLength(16)]
        public string Password { get; set; }
        [Column("OTP")]
        [StringLength(6)]
        public string Otp { get; set; }
        public string AccessToken { get; set; }
        public string DeviceToken { get; set; }
        public bool? DelFlag { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty(nameof(Employee.Account))]
        public virtual Employee AccountNavigation { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty("Accounts")]
        public virtual Role Role { get; set; }
    }
}
