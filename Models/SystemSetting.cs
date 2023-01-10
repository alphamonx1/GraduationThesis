using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("SystemSetting")]
    public partial class SystemSetting
    {
        [Key]
        [Column("SystemSettingID")]
        public int SystemSettingId { get; set; }
        public int? AttendanceStartTime { get; set; }
        public int? AttendanceEndTime { get; set; }
        public int? SalaryCalculateStartTime { get; set; }
        public int? SalaryCalculateEndTime { get; set; }
        public int? SalaryUpdateStartTime { get; set; }
        public int? SalaryUpdateEndTime { get; set; }
        public int? ApplicationSendBefore { get; set; }
        public bool? IsEnable { get; set; }
    }
}
