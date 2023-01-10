using CAPSTONEPROJECT.DataModels.WorkScheduleDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CAPSTONEPROJECT.Services
{
    public class WorkScheduleService
    {
        private readonly LugContext _context;
        public WorkScheduleService(LugContext context)
        {
            _context = context;
        }

        public List<WorkScheduleResponseModel> GetAll()
        {
            var query = _context.WorkSchedules
                .Select(WS => new WorkScheduleResponseModel
                {
                    WorkScheduleID = WS.WorkScheduleId,
                    EmployeeID = WS.EmployeeId,
                    FullName = _context.Employees.Where(x => x.EmployeeId == WS.EmployeeId).Select(x => x.FullName).FirstOrDefault(),
                    ShiftName = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.ShiftName).FirstOrDefault(),
                    ShiftStartTimeHours = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.StartTime).FirstOrDefault().Value.Hours,
                    ShiftStartTimeMinute = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.StartTime).FirstOrDefault().Value.Minutes,
                    ShiftEndTimeHours = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.EndTime).FirstOrDefault().Value.Hours,
                    ShiftEndTimeMinute = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.EndTime).FirstOrDefault().Value.Minutes,
                    WorkScheduleStatus = _context.WorkScheduleStatuses.Where(x => x.WorkScheduleStatusId == WS.WorkScheduleStatusId).Select(x => x.WorkScheduleStatusName).FirstOrDefault(),
                    WorkingDate = WS.WorkingDate,
                    Address = _context.Workplaces.Where(x => x.WorkplaceId == WS.WorkplaceId).Select(x => x.Address).FirstOrDefault(),
                    InTime = WS.InTime,
                    OutTime = WS.OutTime,
                    WorkHours = WS.WorkHours,
                }).OrderByDescending(x => x.WorkingDate);
            var result = new List<WorkScheduleResponseModel>();

            foreach (var item in query)
            {

                item.WorkHours = Math.Round(Convert.ToDouble(item.WorkHours), 2, MidpointRounding.AwayFromZero);
                result.Add(item);

            }
            return result;
        }

        public List<WorkScheduleResponseModel> GetByMonthYear(int Month,int Year)
        {
            var query = _context.WorkSchedules.Where(x=>x.WorkingDate.Value.Month == Month && x.WorkingDate.Value.Year == Year)
                .Select(WS => new WorkScheduleResponseModel
                {
                    WorkScheduleID = WS.WorkScheduleId,
                    EmployeeID = WS.EmployeeId,
                    FullName = _context.Employees.Where(x => x.EmployeeId == WS.EmployeeId).Select(x => x.FullName).FirstOrDefault(),
                    ShiftName = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.ShiftName).FirstOrDefault(),
                    ShiftStartTimeHours = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.StartTime).FirstOrDefault().Value.Hours,
                    ShiftStartTimeMinute = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.StartTime).FirstOrDefault().Value.Minutes,
                    ShiftEndTimeHours = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.EndTime).FirstOrDefault().Value.Hours,
                    ShiftEndTimeMinute = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.EndTime).FirstOrDefault().Value.Minutes,
                    WorkScheduleStatus = _context.WorkScheduleStatuses.Where(x => x.WorkScheduleStatusId == WS.WorkScheduleStatusId).Select(x => x.WorkScheduleStatusName).FirstOrDefault(),
                    WorkingDate = WS.WorkingDate,
                    Address = _context.Workplaces.Where(x => x.WorkplaceId == WS.WorkplaceId).Select(x => x.Address).FirstOrDefault(),
                    InTime = WS.InTime,
                    OutTime = WS.OutTime,
                    WorkHours = WS.WorkHours,
                }).OrderByDescending(x=>x.WorkingDate);
            var result = new List<WorkScheduleResponseModel>();

            foreach (var item in query)
            {

                item.WorkHours = Math.Round(Convert.ToDouble(item.WorkHours), 2, MidpointRounding.AwayFromZero);
                result.Add(item);

            }
            return result;
        }

        public List<WorkScheduleResponseModel> GetByIDAndDate(string employeeID, DateTime WorkingDate)
        {
            var query = _context.WorkSchedules.Where(x => x.EmployeeId == employeeID && x.WorkingDate.Value.Date == WorkingDate.Date)
                .Select(WS => new WorkScheduleResponseModel
                {
                    WorkScheduleID = WS.WorkScheduleId,
                    EmployeeID = WS.EmployeeId,
                    FullName = _context.Employees.Where(x => x.EmployeeId == WS.EmployeeId).Select(x => x.FullName).FirstOrDefault(),
                    ShiftName = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.ShiftName).FirstOrDefault(),
                    ShiftStartTimeHours = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.StartTime).FirstOrDefault().Value.Hours,
                    ShiftStartTimeMinute = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.StartTime).FirstOrDefault().Value.Minutes,
                    ShiftEndTimeHours = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.EndTime).FirstOrDefault().Value.Hours,
                    ShiftEndTimeMinute = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.EndTime).FirstOrDefault().Value.Minutes,
                    WorkScheduleStatus = _context.WorkScheduleStatuses.Where(x => x.WorkScheduleStatusId == WS.WorkScheduleStatusId).Select(x => x.WorkScheduleStatusName).FirstOrDefault(),
                    WorkingDate = WS.WorkingDate,
                    Address = _context.Workplaces.Where(x => x.WorkplaceId == WS.WorkplaceId).Select(x => x.Address).FirstOrDefault(),
                    InTime = WS.InTime,
                    OutTime = WS.OutTime,
                    WorkHours = WS.WorkHours,
                });
            var result = new List<WorkScheduleResponseModel>();

            foreach (var item in query)
            {

                item.WorkHours = Math.Round(Convert.ToDouble(item.WorkHours), 2, MidpointRounding.AwayFromZero);
                result.Add(item);

            }
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }

        }

        public List<WorkScheduleResponseModel> GetByWorkplace(string WorkplaceID)
        {
            DateTime CurrentServerDate = DateTime.Now;
            DateTime CurrentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(CurrentServerDate, "SE Asia Standard Time");

            var query = _context.WorkSchedules.Where(x => x.WorkplaceId == WorkplaceID)
                .Select(WS => new WorkScheduleResponseModel
                {
                    WorkScheduleID = WS.WorkScheduleId,
                    EmployeeID = WS.EmployeeId,
                    FullName = _context.Employees.Where(x => x.EmployeeId == WS.EmployeeId).Select(x => x.FullName).FirstOrDefault(),
                    ShiftName = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.ShiftName).FirstOrDefault(),
                    ShiftStartTimeHours = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.StartTime).FirstOrDefault().Value.Hours,
                    ShiftStartTimeMinute = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.StartTime).FirstOrDefault().Value.Minutes,
                    ShiftEndTimeHours = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.EndTime).FirstOrDefault().Value.Hours,
                    ShiftEndTimeMinute = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.EndTime).FirstOrDefault().Value.Minutes,
                    WorkScheduleStatus = _context.WorkScheduleStatuses.Where(x => x.WorkScheduleStatusId == WS.WorkScheduleStatusId).Select(x => x.WorkScheduleStatusName).FirstOrDefault(),
                    WorkingDate = WS.WorkingDate,
                    Address = _context.Workplaces.Where(x => x.WorkplaceId == WS.WorkplaceId).Select(x => x.Address).FirstOrDefault(),
                    InTime = WS.InTime,
                    OutTime = WS.OutTime,
                    WorkHours = WS.WorkHours,

                });
            var result = new List<WorkScheduleResponseModel>();

            foreach (var item in query)
            {

                item.WorkHours = Math.Round(Convert.ToDouble(item.WorkHours), 2, MidpointRounding.AwayFromZero);
                result.Add(item);

            }
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }

        }

        public WorkScheduleResponseModel GetRemainWS(string EmployeeID, DateTime WorkingDate)
        {
            var query = _context.WorkSchedules.Where(x => x.EmployeeId == EmployeeID && x.WorkingDate == WorkingDate && x.WorkScheduleStatusId == 4)
                .Select(WS => new WorkScheduleResponseModel
                {
                    WorkScheduleID = WS.WorkScheduleId,
                    EmployeeID = WS.EmployeeId,
                    FullName = _context.Employees.Where(x => x.EmployeeId == WS.EmployeeId).Select(x => x.FullName).FirstOrDefault(),
                    ShiftName = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.ShiftName).FirstOrDefault(),
                    ShiftStartTimeHours = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.StartTime).FirstOrDefault().Value.Hours,
                    ShiftStartTimeMinute = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.StartTime).FirstOrDefault().Value.Minutes,
                    ShiftEndTimeHours = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.EndTime).FirstOrDefault().Value.Hours,
                    ShiftEndTimeMinute = _context.Shifts.Where(x => x.ShiftId == WS.ShiftId).Select(x => x.EndTime).FirstOrDefault().Value.Minutes,
                    WorkScheduleStatus = _context.WorkScheduleStatuses.Where(x => x.WorkScheduleStatusId == WS.WorkScheduleStatusId).Select(x => x.WorkScheduleStatusName).FirstOrDefault(),
                    WorkingDate = WS.WorkingDate,
                    Address = _context.Workplaces.Where(x => x.WorkplaceId == WS.WorkplaceId).Select(x => x.Address).FirstOrDefault(),
                    InTime = WS.InTime,
                    OutTime = WS.OutTime,
                    WorkHours = WS.WorkHours,
                }).FirstOrDefault();

            var result = new List<WorkScheduleResponseModel>();

            return query;
        }

        public bool CreateWS(WorkScheduleCreateModel dataModel)
        {
            bool status = false;
            try
            {
                foreach (var item in dataModel.ListWorkingDate)
                {
                    var ws = new WorkSchedule
                    {
                        EmployeeId = dataModel.EmployeeID,
                        ShiftId = dataModel.ShiftID,
                        WorkingDate = item.Date,
                        WorkplaceId = dataModel.WorkplaceID,
                        WorkScheduleStatusId = 4,
                    };
                    if (!WorkScheduleExistedForEmployee(ws.EmployeeId, ws.ShiftId, ws.WorkingDate))
                    {
                        _context.WorkSchedules.Add(ws);
                        status = _context.SaveChanges() > 0;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }

            return status;


        }

        public bool AutomaticCreateWS(AutomaticWorkScheduleCreateModel dataModel)
        {
            DateTime CurrentServerDate = DateTime.Now;
            DateTime CurrentLocalDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(CurrentServerDate, "SE Asia Standard Time");

            var ListWorkingDate = GetDatesByLoop(CurrentLocalDate);

            foreach (var item in ListWorkingDate.ToList())
            {
                if(item.Day == CurrentLocalDate.Day)
                {
                    ListWorkingDate.Remove(item);
                }
            }


            if (dataModel.NoSaturday)
            {
                foreach (var item in ListWorkingDate.ToList())
                {
                    if(item.DayOfWeek == DayOfWeek.Saturday)
                    {
                        ListWorkingDate.Remove(item);
                    }
                }
            }
            if (dataModel.NoSunday)
            {
                foreach (var item in ListWorkingDate.ToList())
                {
                    if (item.DayOfWeek == DayOfWeek.Sunday)
                    {
                        ListWorkingDate.Remove(item);
                    }
                }
            }

            bool status = false;
            try
            {
                foreach (var Employee in dataModel.ListEmployeeID)
                {
                    foreach (var date in ListWorkingDate)
                    {
                        var ws = new WorkSchedule
                        {
                            EmployeeId = Employee,
                            ShiftId = dataModel.ShiftID,
                            WorkingDate = date.Date,
                            WorkplaceId = dataModel.WorkplaceID,
                            WorkScheduleStatusId = 4,
                        };
                        if (!WorkScheduleExistedForEmployee(ws.EmployeeId, ws.ShiftId, ws.WorkingDate))
                        {
                            _context.WorkSchedules.Add(ws);
                            status = _context.SaveChanges() > 0;
                        }
                        else
                        {
                            break;
                        }
                    }
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }

            return status;


        }

        public  List<DateTime> GetDatesByLinQ(DateTime dateTime)
        {

            var  temp = Enumerable.Range(dateTime.Day, DateTime.DaysInMonth(dateTime.Year, dateTime.Month))  // Days: 1, 2 ... 31 etc.
                          .Select(day => new DateTime(dateTime.Year, dateTime.Month, day)) // Map each day to a date
                           ;
           

            var ListDate = new List<DateTime>();

            foreach (var item in temp)
            {
                ListDate.Add(item);
            }
            foreach (var item in ListDate)
            {
                if(item.DayOfWeek == DayOfWeek.Sunday)
                {
                    ListDate.Remove(item);
                }
            }

            return ListDate;
        }

        public  List<DateTime> GetDatesByLoop(DateTime dateTime)
        { 
            var dates = new List<DateTime>();

            // Loop from the first day of the month until we hit the next month, moving forward a day at a time
            for (var date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day); date.Month == dateTime.Month; date = date.AddDays(1))
            {
                dates.Add(date);
            }

            return dates;
        }

        public bool UpdateWS(int id, WorkScheduleUpdateModel dataModel)
        {
            bool status = false;
            try
            {
                
                var ws = _context.WorkSchedules.Where(x => x.WorkScheduleId == id).FirstOrDefault();
                if(ws.WorkScheduleStatusId == 4)
                {
                    ws.EmployeeId = dataModel.EmployeeID;
                    ws.ShiftId = dataModel.ShiftID;
                    ws.WorkingDate = dataModel.WorkingDate;
                    ws.WorkplaceId = dataModel.WorkplaceID;
                    if (!WorkScheduleExistedForEmployee(ws.EmployeeId, ws.ShiftId, ws.WorkingDate))
                    {
                        status = _context.SaveChanges() > 0;
                    }
                }
                else
                {
                    status = false;
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            return status;
        }

        public bool CheckAttendanceByAdmin(int id, WorkScheduleCheckAttendanceByAdminModel dataModel)
        {
            bool status = false;
            try
            {
                var ws = _context.WorkSchedules.Where(x => x.WorkScheduleId == id).FirstOrDefault();
                ws.WorkScheduleStatusId = dataModel.WorkScheduleStatusID;
                ws.InTime = dataModel.InTime;
                ws.OutTime = dataModel.OutTime;
                var timediff = (dataModel.OutTime.Hour * 60.0 + dataModel.OutTime.Minute) - (dataModel.InTime.Hour * 60.0 + dataModel.InTime.Minute);
                double workhrs = timediff / 60.0;
                ws.WorkHours = workhrs;
                status = _context.SaveChanges() > 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            return status;
        }

        public bool DeleteWS(int id)
        {
            bool status = false;
            try
            {
                var ws = _context.WorkSchedules.Where(x => x.WorkScheduleId == id).FirstOrDefault();
                _context.WorkSchedules.Remove(ws);
                status = _context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            return status;
        }

        public bool WorkScheduleExistedForEmployee(string employeeID, int? shiftID, DateTime? WorkingDate)
        {
            return _context.WorkSchedules.Any(x => x.EmployeeId == employeeID && x.ShiftId == shiftID && x.WorkingDate == WorkingDate);
        }
    }
}
