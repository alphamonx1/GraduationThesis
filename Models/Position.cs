using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("Position")]
    public partial class Position
    {
        public Position()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        [Column("PositionID")]
        [StringLength(10)]
        public string PositionId { get; set; }
        [StringLength(50)]
        public string PositionName { get; set; }
        public bool? DelFlag { get; set; }

        [InverseProperty(nameof(Employee.Position))]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
