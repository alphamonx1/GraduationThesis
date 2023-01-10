using CAPSTONEPROJECT.DataModels.SystemSettingDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.Services
{
    public class SystemSettingService
    {
        private readonly LugContext _context;
        
        public SystemSettingService(LugContext context)
        {
            _context = context;
        }

        public List<SystemSettingResponseModel> GetAll()
        {
            var query = _context.SystemSettings
                .Select(SystemSetting => new SystemSettingResponseModel
                {
                    SystemSystemID = SystemSetting.SystemSettingId,
                    AttendanceStartTime = SystemSetting.AttendanceStartTime,
                    AttendanceEndTime = SystemSetting.AttendanceEndTime,
                    SalaryCalculateStartTime = SystemSetting.SalaryCalculateStartTime,
                    SalaryCalculateEndTime = SystemSetting.SalaryCalculateEndTime,
                    SalaryUpdateStartTime = SystemSetting.SalaryUpdateStartTime,
                    SalaryUpdateEndTime = SystemSetting.SalaryUpdateEndTime,
                    ApplicationSendBefore = SystemSetting.ApplicationSendBefore,
                    IsEnable = SystemSetting.IsEnable,
                });

            var result = new List<SystemSettingResponseModel>();
            foreach (var item in query)
            {
                result.Add(item);
            }
            return result;
        }

        public SystemSettingResponseModel GetByID()
        {
            var query = _context.SystemSettings
                .Select(SystemSetting => new SystemSettingResponseModel
                {
                    SystemSystemID = SystemSetting.SystemSettingId,
                    AttendanceStartTime = SystemSetting.AttendanceStartTime,
                    AttendanceEndTime = SystemSetting.AttendanceEndTime,
                    SalaryCalculateStartTime = SystemSetting.SalaryCalculateStartTime,
                    SalaryCalculateEndTime = SystemSetting.SalaryCalculateEndTime,
                    SalaryUpdateStartTime = SystemSetting.SalaryUpdateStartTime,
                    SalaryUpdateEndTime = SystemSetting.SalaryUpdateEndTime,
                    ApplicationSendBefore = SystemSetting.ApplicationSendBefore,
                    IsEnable = SystemSetting.IsEnable,
                }).FirstOrDefault();

            return query;
        }



        public bool UpdateSetting(int SystemSettingID, SystemSettingUpdateModel dataModel)
        {
            var status = false;
            try
            {
                var SysSetting = _context.SystemSettings.Where(x => x.SystemSettingId == SystemSettingID).FirstOrDefault();
                SysSetting.SalaryCalculateStartTime = dataModel.SalaryCalculateStartTime;
                SysSetting.SalaryCalculateEndTime = dataModel.SalaryCalculateEndTime;
                SysSetting.SalaryUpdateStartTime = dataModel.SalaryUpdateStartTime;
                SysSetting.SalaryUpdateEndTime = dataModel.SalaryUpdateEndTime;
                SysSetting.ApplicationSendBefore = dataModel.ApplicationSendBefore;
                SysSetting.IsEnable = dataModel.IsEnable;
                status = _context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }

            return status;
        }


    }
}
