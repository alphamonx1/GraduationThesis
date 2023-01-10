using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("ContractSalary")]
    public partial class ContractSalary
    {
        [Key]
        [Column("ContractSalaryID")]
        public int ContractSalaryId { get; set; }
        [Column("ContractID")]
        [StringLength(20)]
        public string ContractId { get; set; }
        [Column("EmployeeID")]
        [StringLength(20)]
        public string EmployeeId { get; set; }
        [Column("ContractSalary")]
        public double? ContractSalary1 { get; set; }
        [Column("BasicWorkingDay-Time")]
        public int? BasicWorkingDayTime { get; set; }
        [Column("ContractTypeID")]
        public int? ContractTypeId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? SignDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ContractStartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ContractEndDate { get; set; }

        [ForeignKey(nameof(ContractTypeId))]
        [InverseProperty("ContractSalaries")]
        public virtual ContractType ContractType { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("ContractSalaries")]
        public virtual Employee Employee { get; set; }
    }
}
