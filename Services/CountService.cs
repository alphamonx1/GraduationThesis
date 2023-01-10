
using CAPSTONEPROJECT.DataModels.ReportEmployeeWorkStatusModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.Services
{
    public class CountService
    {
        private readonly LugContext _context;

        public CountService(LugContext context)
        {
            _context = context;
        }

        public StoreWorkingStatusModel GetStoreWorkingStatus(string? WorkplaceID)
        {
            var TotalWorkingEmployee = 0;
            var TotalLateEmployee = 0;
            var TotalOffEmployee = 0;
            var TotalAbsentEmployee = 0;

            StoreWorkingStatusModel StoreStatus = new StoreWorkingStatusModel
            {
                TotalWorkingEmployee = TotalWorkingEmployee,
                TotalLateEmployee = TotalLateEmployee,
                TotalAbsentEmployee = TotalAbsentEmployee,
                TotalOffEmployee = TotalOffEmployee,

            };
            if (WorkplaceID == null)
            {
                DateTime CurrentServerDateTime = DateTime.Now;
                DateTime CurrentDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(CurrentServerDateTime, "SE Asia Standard Time");
                var ListSuitableShift = GetSuitableShiftByTime(CurrentDateTime);

                
                var query = _context.WorkSchedules.Where(x => x.WorkingDate.Value.Date == CurrentDateTime.Date);

                foreach (var shift in ListSuitableShift)
                {

                    foreach (var empsws in query)
                    {
                        if (empsws.ShiftId == shift)
                        {
                            if (empsws.WorkScheduleStatusId == 1)
                            {
                                TotalWorkingEmployee += 1;
                            }
                            else if (empsws.WorkScheduleStatusId == 2)
                            {
                                TotalLateEmployee += 1;
                            }
                            else if (empsws.WorkScheduleStatusId == 4)
                            {
                                TotalAbsentEmployee += 1;
                            }
                            else if (empsws.WorkScheduleStatusId == 3)
                            {
                                TotalOffEmployee += 1;
                            }
                        }
                    }
                }
                StoreStatus = new StoreWorkingStatusModel
                {
                    TotalWorkingEmployee = TotalWorkingEmployee,
                    TotalLateEmployee = TotalLateEmployee,
                    TotalAbsentEmployee = TotalAbsentEmployee,
                    TotalOffEmployee = TotalOffEmployee,
                };
            }
            else
            {
                DateTime CurrentServerDateTime = DateTime.Now;
                DateTime CurrentDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(CurrentServerDateTime, "SE Asia Standard Time");
                var ListSuitableShift = GetSuitableShiftByTime(CurrentDateTime);

     
                var query = _context.WorkSchedules.Where(x => x.WorkingDate.Value.Date == CurrentDateTime.Date && x.WorkplaceId == WorkplaceID);

                foreach (var shift in ListSuitableShift)
                {
                    foreach (var empsws in query)
                    {
                        if (empsws.ShiftId == shift)
                        {
                            if (empsws.WorkScheduleStatusId == 1)
                            {
                                TotalWorkingEmployee += 1;
                            }
                            else if (empsws.WorkScheduleStatusId == 2)
                            {
                                TotalLateEmployee += 1;
                            }
                            else if (empsws.WorkScheduleStatusId == 4)
                            {
                                TotalAbsentEmployee += 1;
                            }
                            else if (empsws.WorkScheduleStatusId == 3)
                            {
                                TotalOffEmployee += 1;
                            }
                        }
                    }
                    StoreStatus = new StoreWorkingStatusModel
                    {
                        TotalWorkingEmployee = TotalWorkingEmployee,
                        TotalLateEmployee = TotalLateEmployee,
                        TotalAbsentEmployee = TotalAbsentEmployee,
                        TotalOffEmployee = TotalOffEmployee,

                    };


                }
                
            }
            return StoreStatus;
        }

        public List<int?> GetSuitableShiftByTime(DateTime? CheckTimeStamp)
        {
            var result = new List<int?>();
            var ListShift = _context.WorkSchedules.Select(x => x.ShiftId);

            var listShiftID = new List<int?>();

            foreach (var item in ListShift)
            {
                listShiftID.Add(item);
            }
            foreach (var item in listShiftID)
            {
                var ShiftStartTime = _context.Shifts.Where(x => x.ShiftId == item.Value).Select(x => x.StartTime).FirstOrDefault();
                var ShiftEndTme = _context.Shifts.Where(x => x.ShiftId == item.Value).Select(x => x.EndTime).FirstOrDefault();
                if (CheckTimeStamp.Value.Hour >= ShiftStartTime.Value.Hours && CheckTimeStamp.Value.Hour <= ShiftEndTme.Value.Hours)
                {
                    result.Add(item);
                }
            }
            var finalResult = result.Distinct().ToList();
            return finalResult;
        }

     


        

    }
}



