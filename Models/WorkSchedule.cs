using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("WorkSchedule")]
    public partial class WorkSchedule
    {
        public WorkSchedule()
        {
            Applications = new HashSet<Application>();
        }

        [Key]
        [Column("WorkScheduleID")]
        public int WorkScheduleId { get; set; }
        [Column("EmployeeID")]
        [StringLength(20)]
        public string EmployeeId { get; set; }
        [Column("ShiftID")]
        public int? ShiftId { get; set; }
        [Column("WorkScheduleStatusID")]
        public int? WorkScheduleStatusId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? WorkingDate { get; set; }
        [Column("WorkplaceID")]
        [StringLength(10)]
        public string WorkplaceId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OutTime { get; set; }
        public double? WorkHours { get; set; }
        [Column("IsOT")]
        public bool? IsOt { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("WorkSchedules")]
        public virtual Employee Employee { get; set; }
        [ForeignKey(nameof(ShiftId))]
        [InverseProperty("WorkSchedules")]
        public virtual Shift Shift { get; set; }
        [ForeignKey(nameof(WorkScheduleStatusId))]
        [InverseProperty("WorkSchedules")]
        public virtual WorkScheduleStatus WorkScheduleStatus { get; set; }
        [ForeignKey(nameof(WorkplaceId))]
        [InverseProperty("WorkSchedules")]
        public virtual Workplace Workplace { get; set; }
        [InverseProperty(nameof(Application.Shift))]
        public virtual ICollection<Application> Applications { get; set; }
    }
}
