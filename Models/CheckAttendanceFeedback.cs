using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("CheckAttendanceFeedback")]
    public partial class CheckAttendanceFeedback
    {
        [Key]
        [Column("FeedbackID")]
        public int FeedbackId { get; set; }
        [Column("EmployeeID")]
        [StringLength(20)]
        public string EmployeeId { get; set; }
        public byte[] Image { get; set; }
        public string Reason { get; set; }
        public TimeSpan? CheckTime { get; set; }
        [Column(TypeName = "date")]
        public DateTime? WorkingDate { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("CheckAttendanceFeedbacks")]
        public virtual Employee Employee { get; set; }
    }
}
