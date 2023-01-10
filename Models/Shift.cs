using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("Shift")]
    public partial class Shift
    {
        public Shift()
        {
            WorkSchedules = new HashSet<WorkSchedule>();
        }

        [Key]
        [Column("ShiftID")]
        public int ShiftId { get; set; }
        [StringLength(50)]
        public string ShiftName { get; set; }
        [Column(TypeName = "time(0)")]
        public TimeSpan? StartTime { get; set; }
        [Column(TypeName = "time(0)")]
        public TimeSpan? EndTime { get; set; }
        public bool? DelFlag { get; set; }

        [InverseProperty(nameof(WorkSchedule.Shift))]
        public virtual ICollection<WorkSchedule> WorkSchedules { get; set; }
    }
}
