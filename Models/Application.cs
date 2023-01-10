using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("Application")]
    public partial class Application
    {
        [Key]
        [Column("ApplicationID")]
        public int ApplicationId { get; set; }
        [Column("EmployeeID")]
        [StringLength(20)]
        public string EmployeeId { get; set; }
        [Column("ShiftID")]
        public int? ShiftId { get; set; }
        public string ApplicationContent { get; set; }
        [Column("ApplicationTypeID")]
        public int? ApplicationTypeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ApplicationMakingDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastReviewByAboveDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ApplyDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastModifiedDate { get; set; }
        [Column("ApplicationStatusID")]
        public int? ApplicationStatusId { get; set; }
        public string Reason { get; set; }

        [ForeignKey(nameof(ApplicationStatusId))]
        [InverseProperty("Applications")]
        public virtual ApplicationStatus ApplicationStatus { get; set; }
        [ForeignKey(nameof(ApplicationTypeId))]
        [InverseProperty("Applications")]
        public virtual ApplicationType ApplicationType { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("Applications")]
        public virtual Employee Employee { get; set; }
        [ForeignKey(nameof(ShiftId))]
        [InverseProperty(nameof(WorkSchedule.Applications))]
        public virtual WorkSchedule Shift { get; set; }
    }
}
