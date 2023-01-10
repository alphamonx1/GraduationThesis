using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("WorkScheduleStatus")]
    public partial class WorkScheduleStatus
    {
        public WorkScheduleStatus()
        {
            WorkSchedules = new HashSet<WorkSchedule>();
        }

        [Key]
        [Column("WorkScheduleStatusID")]
        public int WorkScheduleStatusId { get; set; }
        [StringLength(50)]
        public string WorkScheduleStatusName { get; set; }

        [InverseProperty(nameof(WorkSchedule.WorkScheduleStatus))]
        public virtual ICollection<WorkSchedule> WorkSchedules { get; set; }
    }
}
