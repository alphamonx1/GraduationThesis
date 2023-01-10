using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("Salary")]
    public partial class Salary
    {
        public Salary()
        {
            BackupSalaries = new HashSet<BackupSalary>();
        }

        [Key]
        [Column("SalaryID")]
        public int SalaryId { get; set; }
        [Column("EmployeeID")]
        [StringLength(20)]
        public string EmployeeId { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public double? CurrentContractSalary { get; set; }
        public double? Income { get; set; }
        public string Notes { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("Salaries")]
        public virtual Employee Employee { get; set; }
        [InverseProperty(nameof(BackupSalary.Salary))]
        public virtual ICollection<BackupSalary> BackupSalaries { get; set; }
    }
}
