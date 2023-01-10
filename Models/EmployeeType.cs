using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("EmployeeType")]
    public partial class EmployeeType
    {
        public EmployeeType()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        [Column("EmployeeTypeID")]
        public int EmployeeTypeId { get; set; }
        [StringLength(50)]
        public string EmployeeTypeName { get; set; }
        public bool? DelFlag { get; set; }

        [InverseProperty(nameof(Employee.EmployeeType))]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
