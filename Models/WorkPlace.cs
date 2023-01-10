using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("Workplace")]
    public partial class Workplace
    {
        public Workplace()
        {
            Employees = new HashSet<Employee>();
            WorkSchedules = new HashSet<WorkSchedule>();
        }

        [Key]
        [Column("WorkplaceID")]
        [StringLength(10)]
        public string WorkplaceId { get; set; }
        [StringLength(50)]
        public string WorkplaceName { get; set; }
        [StringLength(50)]
        public string Address { get; set; }
        [Column("BSSID")]
        [StringLength(50)]
        public string Bssid { get; set; }
        public bool? DelFlag { get; set; }

        [InverseProperty(nameof(Employee.Workplace))]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty(nameof(WorkSchedule.Workplace))]
        public virtual ICollection<WorkSchedule> WorkSchedules { get; set; }
    }
}
