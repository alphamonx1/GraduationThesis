using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.DataModels.SystemSettingDataModel
{
    public class SystemSettingUpdateModel
    {
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
