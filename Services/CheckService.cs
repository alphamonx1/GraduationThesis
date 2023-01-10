using CAPSTONEPROJECT.DataModels.CheckDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CAPSTONEPROJECT.Services
{
    public class CheckService
    {
        private readonly LugContext _context;
        private readonly SystemSettingService _service;

        public CheckService(LugContext context,SystemSettingService service)
        {
            _context = context;
            _service = service;


        }

        public CheckResponseModel GetByID(string id, string BSSID)
        {
            DateTime currentServerDateTime = DateTime.Now;
            DateTime currentLocalDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentServerDateTime, "SE Asia Standard Time");

            bool exist = _context.Workplaces.Any(x => x.Bssid == BSSID);
 
            if (!exist)
            {
                var query = _context.Employees.Where(x => x.EmployeeId == id)
                     .Select(empCheck => new CheckResponseModel
                     {
                         EmployeeID = empCheck.EmployeeId,
                         FullName = empCheck.FullName,
                         ProfileImage = empCheck.Image,
                         Birthday = empCheck.DateOfBirth.Value.Date,
                         WorkingDate = currentLocalDateTime.Date.Date,
                         CheckTimeHours = currentLocalDateTime.Hour,
                         CheckTimeMin = currentLocalDateTime.Minute,
                         WorkplaceID = "Không có thông tin về địa điểm",
                         Address = "Không có thông tin về địa điểm ",

                     }).FirstOrDefault();
                return query;
            }
            else
            {
                var workplaceByWifi = _context.Workplaces.Where(x => x.Bssid == BSSID).FirstOrDefault();
                var query = _context.Employees.Where(x => x.EmployeeId == id)
                    .Select(empCheck => new CheckResponseModel
                    {
                        EmployeeID = empCheck.EmployeeId,
                        FullName = empCheck.FullName,
                        ProfileImage = empCheck.Image,
                        Birthday = empCheck.DateOfBirth.Value.Date,
                        WorkingDate = currentLocalDateTime.Date.Date,
                        CheckTimeHours = currentLocalDateTime.Hour,
                        CheckTimeMin = currentLocalDateTime.Minute,
                        WorkplaceID = workplaceByWifi.WorkplaceId,
                        Address = workplaceByWifi.Address,

                    }).FirstOrDefault();
                return query;
            }



        }

        public bool CheckConfirm(CheckConfirmModel dataModel)
        {
            bool status = false;
            try
            {
                if (HaveWorkSchedule(dataModel.EmployeeID, dataModel.CheckTimestamp.Value.Date, dataModel.WorkplaceID))
                {
                    var ShiftOfEmployee = GetShiftByCheckTime(dataModel.EmployeeID, dataModel.CheckTimestamp);
                    if (ShiftOfEmployee != 0)
                    {
                        if (InWorkingTime(dataModel.CheckTimestamp, ShiftOfEmployee))
                        {
                            var ShiftStartTime = _context.Shifts.Where(x => x.ShiftId == ShiftOfEmployee).Select(x => x.StartTime).FirstOrDefault();

                            var InTimeExist = _context.WorkSchedules.Where(x => x.EmployeeId == dataModel.EmployeeID &&
                                                                           x.WorkingDate == dataModel.CheckTimestamp.Value.Date &&
                                                                           x.ShiftId == ShiftOfEmployee)
                                                                    .Select(x => x.InTime).FirstOrDefault();
                            if (InTimeExist == null)
                            {
                                bool isLate = IsLate(dataModel.CheckTimestamp, ShiftStartTime);

                                var ws = _context.WorkSchedules.Where(x => x.EmployeeId == dataModel.EmployeeID &&
                                                                         x.WorkingDate.Value.Date == dataModel.CheckTimestamp.Value.Date &&
                                                                         x.ShiftId == ShiftOfEmployee)
                                                               .FirstOrDefault();
                                ws.InTime = dataModel.CheckTimestamp.Value;
                                if (isLate)
                                {
                                    ws.WorkScheduleStatusId = 2;
                                }
                                else
                                {
                                    ws.WorkScheduleStatusId = 1;
                                }

                                status = _context.SaveChanges() > 0;


                            }
                            else if (InTimeExist != null)
                            {

                                var ws = _context.WorkSchedules.Where(x => x.EmployeeId == dataModel.EmployeeID &&
                                                                         x.WorkingDate.Value.Date == dataModel.CheckTimestamp.Value.Date &&
                                                                         x.ShiftId == ShiftOfEmployee)
                                                               .FirstOrDefault();

                                ws.OutTime = dataModel.CheckTimestamp.Value;
                                var timediff = (ws.OutTime.Value.Hour * 60.0 + ws.OutTime.Value.Minute) - (ws.InTime.Value.Hour * 60.0 + ws.InTime.Value.Minute);
                                double workhrs = timediff / 60.0;

                                if (ws.WorkScheduleStatusId == 2)
                                {
                                    var calculateLateTime = (ws.InTime.Value.Hour * 60 + ws.InTime.Value.Minute) - (ShiftStartTime.Value.Hours * 60 + ShiftStartTime.Value.Minutes);
                                    var LateTime = calculateLateTime;
                                    var DeductionTime = 0.0;
                                    if (LateTime > 0 && LateTime < 10)
                                    {
                                        DeductionTime = 0.5;

                                    }
                                    else if (LateTime > 10)
                                    {
                                        if (LateTime < 30)
                                        {
                                            DeductionTime = 0;
                                        }
                                        else if (LateTime >= 30 && LateTime <= 60)
                                        {
                                            DeductionTime = 1;
                                        }
                                        else if (LateTime > 60)
                                        {
                                            DeductionTime = workhrs;
                                        }
                                    }
                                    ws.WorkHours = workhrs - DeductionTime;
                                }
                                ws.WorkHours = workhrs;

                                status = _context.SaveChanges() > 0;
                            }
                        }
                        else
                        {
                            status = false;
                        }

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

        public bool IsWorkplaceExist(string bssid)
        {
            return _context.Workplaces.Any(x => x.Bssid == bssid);
        }

        public bool IsWorkPlaceCorrect(string employeeID, string bssid)
        {
            var workplaceID = _context.Workplaces.Where(x => x.Bssid == bssid).Select(x => x.WorkplaceId).FirstOrDefault();
            return _context.WorkSchedules.Any(x => x.EmployeeId == employeeID && x.WorkplaceId == workplaceID);
        }

        
        public bool HaveWorkSchedule(string EmployeeID, DateTime CheckTimestamp, string WorkplaceID)
        {
            return _context.WorkSchedules.Any(x => x.EmployeeId == EmployeeID && x.WorkingDate == CheckTimestamp.Date && x.WorkplaceId == WorkplaceID);
        }

        public int? GetShiftByCheckTime(string EmployeeID, DateTime? CheckTimeStamp)
        {
            int? result = 0;
            var ListShift = _context.WorkSchedules.Where(x => x.EmployeeId == EmployeeID && x.WorkingDate == CheckTimeStamp.Value.Date).Select(x => x.ShiftId);

            var listShiftID = new List<int?>();

            foreach (var item in ListShift)
            {
                listShiftID.Add(item);
            }
            foreach (var item in listShiftID)
            {
                var ShiftStartTime = _context.Shifts.Where(x => x.ShiftId == item.Value).Select(x => x.StartTime).FirstOrDefault();
                var ShiftEndTime = _context.Shifts.Where(x => x.ShiftId == item.Value).Select(x => x.EndTime).FirstOrDefault();
                if (CheckTimeStamp.Value.Hour >= ShiftStartTime.Value.Hours && CheckTimeStamp.Value.Hour <= ShiftEndTime.Value.Hours)
                {
                    result = item;
                    break;
                }
                else if (CheckTimeStamp.Value.Hour < ShiftStartTime.Value.Hours)
                {
                    result = item;
                    break;
                }
                else if (CheckTimeStamp.Value.Hour > ShiftEndTime.Value.Hours)
                {
                    result = item;
                    break;

                }
            }

            return result;
        }

        public bool InWorkingTime(DateTime? CheckTimestamp, int? ShiftOfEmployee)
        {
            var setting = _service.GetByID();
            var ShiftStartTime = _context.Shifts.Where(x => x.ShiftId == ShiftOfEmployee).Select(x => x.StartTime).FirstOrDefault();
            var ShiftEndTime = _context.Shifts.Where(x => x.ShiftId == ShiftOfEmployee).Select(x => x.EndTime).FirstOrDefault();

            bool status = false;

            if (CheckTimestamp.Value.Hour >= ShiftStartTime.Value.Hours && CheckTimestamp.Value.Hour <= ShiftEndTime.Value.Hours)
            {

                if (CheckTimestamp.Value.Minute >= ShiftStartTime.Value.Minutes && CheckTimestamp.Value.Minute <= ShiftEndTime.Value.Minutes)
                {
                    status = true;
                }
                else if (CheckTimestamp.Value.Minute < ShiftStartTime.Value.Minutes && CheckTimestamp.Value.Minute <= ShiftEndTime.Value.Minutes)
                {
                    status = (ShiftStartTime.Value.Minutes - CheckTimestamp.Value.Minute) <= 15;

                }
                else if (CheckTimestamp.Value.Minute < ShiftStartTime.Value.Minutes && CheckTimestamp.Value.Minute >= ShiftEndTime.Value.Minutes)
                {
                    if ((ShiftStartTime.Value.Minutes - CheckTimestamp.Value.Minute) <= 15)
                    {
                        status = true;
                    }
                    else if ((CheckTimestamp.Value.Minute - ShiftEndTime.Value.Minutes) <= 30)
                    {
                        status = true;
                    }
                }
                else if (CheckTimestamp.Value.Minute >= ShiftStartTime.Value.Minutes && CheckTimestamp.Value.Minute > ShiftEndTime.Value.Minutes)
                {
                    status = (CheckTimestamp.Value.Minute - ShiftEndTime.Value.Minutes) <= 30;
                }
                else if (CheckTimestamp.Value.Minute <= ShiftStartTime.Value.Minutes && CheckTimestamp.Value.Minute > ShiftEndTime.Value.Minutes)
                {
                    status = status = (CheckTimestamp.Value.Minute - ShiftEndTime.Value.Minutes) <= 30;
                }
                

            }

            else if (CheckTimestamp.Value.Hour < ShiftStartTime.Value.Hours)
            {
                if (Math.Abs((CheckTimestamp.Value.Hour * 60 + CheckTimestamp.Value.Minute) - (ShiftStartTime.Value.Hours * 60 + ShiftStartTime.Value.Minutes)) > setting.AttendanceStartTime)
                {
                    status = false;
                }
                else
                {
                    status = true;
                }
            }
            else if (CheckTimestamp.Value.Hour > ShiftEndTime.Value.Hours)
            {
                if (Math.Abs((CheckTimestamp.Value.Hour * 60 + CheckTimestamp.Value.Minute) - (ShiftEndTime.Value.Hours * 60 + ShiftEndTime.Value.Minutes)) <= setting.AttendanceEndTime)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            else
            {
                status = false;
            }

            return status;

        }

        public bool IsLate(DateTime? CheckTimeStamp, TimeSpan? ShiftStartTime)
        {
            bool status = false; ;
            if ((CheckTimeStamp.Value.Hour * 60 + CheckTimeStamp.Value.Minute) - (ShiftStartTime.Value.Hours * 60 + ShiftStartTime.Value.Minutes) > 3)
            {
                status = true;
            }

            return status;
        }




    }


}


