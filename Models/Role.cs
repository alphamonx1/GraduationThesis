using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        [Key]
        [Column("RoleID")]
        public int RoleId { get; set; }
        [StringLength(20)]
        public string RoleName { get; set; }
        public bool? DelFlag { get; set; }

        [InverseProperty(nameof(Account.Role))]
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
