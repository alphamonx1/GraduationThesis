using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("BackupSalary")]
    public partial class BackupSalary
    {
        [Key]
        [Column("BackupSalaryID")]
        public int BackupSalaryId { get; set; }
        [Column("SalaryID")]
        public int? SalaryId { get; set; }
        [Column("EmployeeID")]
        [StringLength(20)]
        public string EmployeeId { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public double? CurrentContractSalary { get; set; }
        public double? Income { get; set; }
        public string Notes { get; set; }

        [ForeignKey(nameof(SalaryId))]
        [InverseProperty("BackupSalaries")]
        public virtual Salary Salary { get; set; }
    }
}
