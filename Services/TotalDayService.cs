using CAPSTONEPROJECT.DataModels.TotalDayDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.Services
{
    public class TotalDayService
    {
        private readonly LugContext _context;
        public TotalDayService(LugContext context)
        {
            _context = context;
        }

        public TotalDayResponseModel CountEmployeeTotalDay(string EmployeeID,int month , int year)
        {
            DateTime CurrentServerTime = DateTime.Now;
            DateTime CurrentLocalTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(CurrentServerTime, "SE Asia Standard Time");
            var EmployeeType = _context.Employees.Where(x => x.EmployeeId == EmployeeID).Select(x => x.EmployeeTypeId).FirstOrDefault();
            if(EmployeeType == 1)
            {
                var TotalDayModel = new TotalDayResponseModel
                {
                    TotalWorkTimeInMonth = (double) _context.ContractSalaries.Where(x => x.EmployeeId == EmployeeID).Select(x => x.BasicWorkingDayTime).FirstOrDefault(),
                    TotalWorkedTime = _context.WorkSchedules.Where(x => x.EmployeeId == EmployeeID && x.WorkingDate.Value.Month == month && x.WorkingDate.Value.Year == year && x.WorkScheduleStatusId == 1).Count() + _context.WorkSchedules.Where(x => x.EmployeeId == EmployeeID && x.WorkingDate.Value.Month == month && x.WorkingDate.Value.Year == year && x.WorkScheduleStatusId == 2).Count(),
                    TotalAbsentDay  = _context.WorkSchedules.Where(x => x.EmployeeId == EmployeeID && x.WorkingDate.Value.Month == month && x.WorkingDate.Value.Year == year && x.WorkScheduleStatusId == 4 && x.WorkingDate < CurrentLocalTime.Date).Count() + _context.WorkSchedules.Where(x => x.EmployeeId == EmployeeID && x.WorkingDate.Value.Month == month && x.WorkingDate.Value.Year == year && x.WorkScheduleStatusId == 3 && x.WorkingDate < CurrentLocalTime.Date).Count(),
                    TotalLateDay = _context.WorkSchedules.Where(x => x.EmployeeId == EmployeeID && x.WorkingDate.Value.Month == month && x.WorkingDate.Value.Year == year && x.WorkScheduleStatusId == 2).Count(),
                    EmployeeTypeID = (int) EmployeeType,
                };

                return TotalDayModel;
            }
            else
            {
                var TotalDayModel = new TotalDayResponseModel
                {
                    TotalWorkTimeInMonth = (double) _context.ContractSalaries.Where(x =>x.EmployeeId == EmployeeID).Select(x => x.BasicWorkingDayTime).FirstOrDefault(),
                    TotalWorkedTime = (double) _context.WorkSchedules.Where(x => x.EmployeeId == EmployeeID && x.WorkingDate.Value.Month == month && x.WorkingDate.Value.Year == year && x.WorkScheduleStatusId == 1).Sum(x=>x.WorkHours) + (double) (_context.WorkSchedules.Where(x => x.EmployeeId == EmployeeID && x.WorkingDate.Value.Month == month && x.WorkingDate.Value.Year == year && x.WorkScheduleStatusId == 2).Sum(x => x.WorkHours)),
                    TotalAbsentDay = _context.WorkSchedules.Where(x => x.EmployeeId == EmployeeID && x.WorkingDate.Value.Month == month && x.WorkingDate.Value.Year == year && x.WorkScheduleStatusId == 4 && x.WorkingDate < CurrentLocalTime.Date).Count() + _context.WorkSchedules.Where(x => x.EmployeeId == EmployeeID && x.WorkingDate.Value.Month == month && x.WorkingDate.Value.Year == year && x.WorkScheduleStatusId == 3 && x.WorkingDate < CurrentLocalTime.Date).Count(),
                    TotalLateDay = _context.WorkSchedules.Where(x => x.EmployeeId == EmployeeID && x.WorkingDate.Value.Month == month && x.WorkingDate.Value.Year == year && x.WorkScheduleStatusId == 2).Count(),
                    EmployeeTypeID = (int) EmployeeType,
                };

                return TotalDayModel;
            }
            

        }

        public TotalOffResponseModel CountEmployeeTotalOffDayRemain(string EmployeeID)
        {
            var totalOffRemain = _context.Employees.Where(x => x.EmployeeId == EmployeeID)
                .Select(off => new TotalOffResponseModel
                {
                    TotalOffDayRemain = off.TotalOffDay,


                }).FirstOrDefault();

            return totalOffRemain;

        }
    }
}
