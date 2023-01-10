using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("ContractType")]
    public partial class ContractType
    {
        public ContractType()
        {
            ContractSalaries = new HashSet<ContractSalary>();
        }

        [Key]
        [Column("ContractTypeID")]
        public int ContractTypeId { get; set; }
        [StringLength(50)]
        public string ContractTypeName { get; set; }

        [InverseProperty(nameof(ContractSalary.ContractType))]
        public virtual ICollection<ContractSalary> ContractSalaries { get; set; }
    }
}
