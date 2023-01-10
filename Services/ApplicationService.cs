using CAPSTONEPROJECT.DataModels.ApplicationDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CAPSTONEPROJECT.Services
{
    public class ApplicationService
    {
        private readonly LugContext _context;
        private readonly WorkScheduleService _service;
        private readonly SystemSettingService _sysservice;
        public ApplicationService(LugContext context, WorkScheduleService service, SystemSettingService sysservice)
        {
            _context = context;
            _service = service;
            _sysservice = sysservice;

        }

        public List<ApplicationResponseModel> GetAll()
        {
            var query = _context.Applications
                .Select(application => new ApplicationResponseModel
                {
                    ApplicationID = application.ApplicationId,
                    Fullname = _context.Employees.Where(x => x.EmployeeId == application.EmployeeId).Select(x => x.FullName).FirstOrDefault(),
                    EmployeeID = application.EmployeeId,
                    ApplicationContent = application.ApplicationContent,
                    ApplicationType = _context.ApplicationTypes.Where(x => x.ApplicationTypeId == application.ApplicationTypeId).Select(x => x.ApplicationTypeName).Single(),
                    ApplicationMakingDate = application.ApplicationMakingDate,
                    ApplyDate = application.ApplyDate,
                    ShiftID = application.ShiftId,
                    ShiftName = _context.Shifts.Where(x => x.ShiftId == application.ShiftId).Select(x => x.ShiftName).First(),
                    LastModifiedDate = application.LastModifiedDate,
                    LastReviewByAboveDate = application.LastReviewByAboveDate,
                    ApplicationStatusID = application.ApplicationStatusId,
                    Reason = application.Reason,
                    TotalOffDayRemain = _context.Employees.Where(x => x.EmployeeId == application.EmployeeId).Select(x => x.TotalOffDay).FirstOrDefault(),

                }).OrderBy(x => x.ApplicationMakingDate);
            var result = new List<ApplicationResponseModel>();
            foreach (var item in query)
            {

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

        public ApplicationResponseModel GetByID(int ApplicationID, string EmployeeID)
        {
            var query = _context.Applications.Where(x => x.ApplicationId == ApplicationID && x.EmployeeId == EmployeeID)
               .Select(application => new ApplicationResponseModel
               {
                   ApplicationID = application.ApplicationId,
                   Fullname = _context.Employees.Where(x => x.EmployeeId == application.EmployeeId).Select(x => x.FullName).FirstOrDefault(),
                   EmployeeID = application.EmployeeId,
                   ApplicationContent = application.ApplicationContent,
                   ApplicationType = _context.ApplicationTypes.Where(x => x.ApplicationTypeId == application.ApplicationTypeId).Select(x => x.ApplicationTypeName).Single(),
                   ApplicationMakingDate = application.ApplicationMakingDate,
                   ApplyDate = application.ApplyDate,
                   ShiftID = application.ShiftId,
                   ShiftName = _context.Shifts.Where(x => x.ShiftId == application.ShiftId).Select(x => x.ShiftName).First(),
                   LastModifiedDate = application.LastModifiedDate,
                   LastReviewByAboveDate = application.LastReviewByAboveDate,
                   ApplicationStatusID = application.ApplicationStatusId,
                   Reason = application.Reason,
                   TotalOffDayRemain = _context.Employees.Where(x => x.EmployeeId == application.EmployeeId).Select(x => x.TotalOffDay).FirstOrDefault(),
               }).FirstOrDefault();


            return query;
        }

        public List<ApplicationResponseModel> GetApplicationByMonthAndYear(int month, int year)
        {
            var query = _context.Applications.Where(x => x.ApplicationMakingDate.Value.Month == month && x.ApplicationMakingDate.Value.Year == year)
                .Select(application => new ApplicationResponseModel
                {
                    ApplicationID = application.ApplicationId,
                    Fullname = _context.Employees.Where(x => x.EmployeeId == application.EmployeeId).Select(x => x.FullName).FirstOrDefault(),
                    EmployeeID = application.EmployeeId,
                    ApplicationContent = application.ApplicationContent,
                    ApplicationType = _context.ApplicationTypes.Where(x => x.ApplicationTypeId == application.ApplicationTypeId).Select(x => x.ApplicationTypeName).Single(),
                    ApplicationMakingDate = application.ApplicationMakingDate,
                    ApplyDate = application.ApplyDate,
                    ShiftID = application.ShiftId,
                    ShiftName = _context.Shifts.Where(x => x.ShiftId == application.ShiftId).Select(x => x.ShiftName).First(),
                    LastModifiedDate = application.LastModifiedDate,
                    LastReviewByAboveDate = application.LastReviewByAboveDate,
                    ApplicationStatusID = application.ApplicationStatusId,
                    Reason = application.Reason,
                    TotalOffDayRemain = _context.Employees.Where(x => x.EmployeeId == application.EmployeeId).Select(x => x.TotalOffDay).FirstOrDefault(),

                }).OrderBy(x=>x.EmployeeID).ThenBy(x=>x.ApplicationType).ThenBy(x=>x.ApplicationStatusID);
            var result = new List<ApplicationResponseModel>();
            foreach (var item in query)
            {

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

        public List<ApplicationResponseModel> GetApplicationByIDAndMonthAndYear(string EmpID, int month, int year)
        {
            var query = _context.Applications.Where(x => x.EmployeeId == EmpID && x.ApplicationMakingDate.Value.Month == month && x.ApplicationMakingDate.Value.Year == year)
                 .Select(application => new ApplicationResponseModel
                 {
                     ApplicationID = application.ApplicationId,
                     Fullname = _context.Employees.Where(x => x.EmployeeId == application.EmployeeId).Select(x => x.FullName).FirstOrDefault(),
                     EmployeeID = application.EmployeeId,
                     ApplicationContent = application.ApplicationContent,
                     ApplicationType = _context.ApplicationTypes.Where(x => x.ApplicationTypeId == application.ApplicationTypeId).Select(x => x.ApplicationTypeName).Single(),
                     ApplicationMakingDate = application.ApplicationMakingDate,
                     ApplyDate = application.ApplyDate,
                     ShiftID = application.ShiftId,
                     ShiftName = _context.Shifts.Where(x => x.ShiftId == application.ShiftId).Select(x => x.ShiftName).First(),
                     LastModifiedDate = application.LastModifiedDate,
                     LastReviewByAboveDate = application.LastReviewByAboveDate,
                     ApplicationStatusID = application.ApplicationStatusId,
                     Reason = application.Reason,
                     TotalOffDayRemain = _context.Employees.Where(x => x.EmployeeId == application.EmployeeId).Select(x => x.TotalOffDay).FirstOrDefault(),

                 });

            var result = new List<ApplicationResponseModel>();
            foreach (var item in query)
            {
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

        public bool CreateApplication(ApplicationCreateModel dataModel)
        {
            bool status = false;
            DateTime currentServerDate = DateTime.Now;
            DateTime currentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentServerDate, "SE Asia Standard Time");
            try
            {
                var app = new Application
                {
                    EmployeeId = dataModel.EmployeeID,
                    ShiftId = dataModel.ShiftID,
                    ApplicationContent = dataModel.ApplicationContent,
                    ApplicationTypeId = dataModel.ApplicationTypeID,
                    ApplicationMakingDate = currentDate,
                    ApplyDate = dataModel.ApplyDate,
                    ApplicationStatusId = 1,
                };


                if(app.ApplicationTypeId == 1)
                {
                    if (CheckApplicationSendDate(app.ApplicationMakingDate, app.ApplyDate))
                    {
                        if (CheckApplicationSendCondition(app.EmployeeId, app.ApplyDate, app.ShiftId, app.ApplicationTypeId))
                        {
                            _context.Applications.Add(app);
                            status = _context.SaveChanges() > 0;
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

                }
                else
                {
                    if (CheckApplicationSendCondition(app.EmployeeId, app.ApplyDate, app.ShiftId, app.ApplicationTypeId))
                    {
                        _context.Applications.Add(app);
                        status = _context.SaveChanges() > 0;
                    }
                    else
                    {
                        status = false;
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

        public bool ApprovedApplication(int id, string EmpID, int? ShiftID, ApplicationApproveModel dataModel)
        {
            bool status = false;
            DateTime currentServerDate = DateTime.Now;
            DateTime currentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentServerDate, "SE Asia Standard Time");
            try
            {
                var app = _context.Applications.Where(x => x.ApplicationId == id && x.EmployeeId == EmpID).FirstOrDefault();
                if (app.ApplicationTypeId == 1)
                {
                    if (ShiftID != null)
                    {
                        app.ApplicationStatusId = 3;
                        app.Reason = dataModel.Reason;
                        app.LastReviewByAboveDate = currentDate;
                        var ws = _context.WorkSchedules.Where(x => x.EmployeeId == EmpID && x.WorkingDate == app.ApplyDate && x.ShiftId == ShiftID).FirstOrDefault();


                        var empOff = _context.Employees.Where(x => x.EmployeeId == EmpID).FirstOrDefault();

                        if (empOff.TotalOffDay > 0)
                        {
                            ws.WorkScheduleStatusId = 3;
                            var ShiftStartTime = _context.Shifts.Where(x => x.ShiftId == ws.ShiftId).Select(x => x.StartTime).FirstOrDefault();
                            var ShiftEndTime = _context.Shifts.Where(x => x.ShiftId == ws.ShiftId).Select(x => x.EndTime).FirstOrDefault();

                            var InTime = ws.WorkingDate + ShiftStartTime;
                            ws.InTime = InTime;
                            var OutTime = ws.WorkingDate + ShiftEndTime;
                            ws.OutTime = OutTime;

                            empOff.TotalOffDay -= 1;

                            ws.WorkHours = ((ShiftEndTime.Value.Hours * 60 + ShiftEndTime.Value.Minutes) - (ShiftStartTime.Value.Hours * 60 + ShiftEndTime.Value.Minutes)) / 60;
                            
                        }
                        else
                        {
                            ws.WorkScheduleStatusId = 5;
                        }

                    }

                    status = _context.SaveChanges() > 0;
                }
                else if (app.ApplicationTypeId == 2)
                {
                    app.ApplicationStatusId = 3;
                    app.Reason = dataModel.Reason;
                    app.LastModifiedDate = currentDate;
                    app.LastReviewByAboveDate = currentDate;
                    if (ShiftID != null)
                    {
                        var ws = new WorkSchedule
                        {
                            EmployeeId = EmpID,
                            ShiftId = ShiftID,
                            WorkingDate = app.ApplyDate,
                            WorkplaceId = _context.Employees.Where(x => x.EmployeeId == EmpID).Select(x => x.WorkplaceId).FirstOrDefault(),
                            WorkScheduleStatusId = 4,
                            IsOt = true,
                        };
                        if (!_service.WorkScheduleExistedForEmployee(ws.EmployeeId, ws.ShiftId, ws.WorkingDate))
                        {
                            _context.WorkSchedules.Add(ws);
                            status = _context.SaveChanges() > 0;
                        }
                    }
                }
                else if (app.ApplicationTypeId == 3)
                {
                    if (ShiftID != null)
                    {
                        app.ApplicationStatusId = 3;
                        app.Reason = dataModel.Reason;
                        app.LastReviewByAboveDate = currentDate;
                        var ws = _context.WorkSchedules.Where(x => x.EmployeeId == EmpID && x.WorkingDate == app.ApplyDate && x.ShiftId == ShiftID).FirstOrDefault();
                        ws.WorkScheduleStatusId = 5;
                    }

                    status = _context.SaveChanges() > 0;
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

        public bool UpdateApplication(int id, string EmpID, ApplicationUpdateModel dataModel)
        {
            bool status = false;
            DateTime currentServerDate = DateTime.Now;
            DateTime currentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentServerDate, "SE Asia Standard Time");
            try
            {
                var app = _context.Applications.Where(x => x.ApplicationId == id && x.EmployeeId == EmpID).FirstOrDefault();
                app.ApplicationContent = dataModel.ApplycationContent;
                app.ApplyDate = dataModel.ApplyDate;
                app.ShiftId = dataModel.ShiftID;
                app.LastModifiedDate = currentDate;
                status = _context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            return status;
        }

        public bool CancelApplication(int id, string EmpID)
        {
            bool status = false;
            DateTime currentServerDate = DateTime.Now;
            DateTime currentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentServerDate, "SE Asia Standard Time");
            try
            {
                var app = _context.Applications.Where(x => x.ApplicationId == id && x.EmployeeId == EmpID).FirstOrDefault();
                app.ApplicationStatusId = 2;
                app.LastModifiedDate = currentDate;
                app.LastReviewByAboveDate = currentDate;
                status = _context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            return status;

        }

        public bool RejectApplication(int id, string EmpID,ApplicationApproveModel dataModel)
        {
            bool status = false;
            DateTime currentServerDate = DateTime.Now;
            DateTime currentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(currentServerDate, "SE Asia Standard Time");
            try
            {
                var app = _context.Applications.Where(x => x.ApplicationId == id && x.EmployeeId == EmpID).FirstOrDefault();
                app.ApplicationStatusId = 2;
                app.Reason = dataModel.Reason;
                app.LastModifiedDate = currentDate;
                app.LastReviewByAboveDate = currentDate;
                status = _context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.ToString());
                throw;
            }
            return status;
        }

        public bool CheckApplicationCondition(int id)
        {
            bool status = false;
            var application = _context.Applications.Where(x => x.ApplicationId == id).FirstOrDefault();
            if(application.ApplicationStatusId == 2 || application.ApplicationStatusId == 3)
            {
                status = false;
            }
            else
            {
                status = true;
            }
            return status;
        }

        public bool CheckApplicationSendCondition(string id,DateTime? ApplyDate, int? ShiftID, int? ApplicationTypeID)
        {
            var setting = _sysservice.GetByID();
            bool status = true;
            if (_context.Applications.Any(x => x.EmployeeId == id && x.ApplyDate.Value.Date == ApplyDate.Value.Date && x.ShiftId == ShiftID && x.ApplicationTypeId == ApplicationTypeID && x.ApplicationStatusId == 1))
            {
                status = false;
            }
            else
            {
                status = true;
            }
            return status;
        }

        public bool CheckApplicationSendDate(DateTime? ApplicationMakingDate,DateTime? ApplyDate)
        {
            var setting = _sysservice.GetByID();
            bool status = true;
            if((ApplyDate.Value.Day - ApplicationMakingDate.Value.Day) >= setting.ApplicationSendBefore )
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        public bool IsWSExist(string EmployeeID, int? ShiftID, DateTime? ApplyDate)
        {
            return _context.WorkSchedules.Any(x => x.EmployeeId == EmployeeID && x.ShiftId == ShiftID && x.WorkingDate.Value.Date == ApplyDate.Value.Date);
        }

    }
}
