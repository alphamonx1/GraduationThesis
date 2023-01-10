using CAPSTONEPROJECT.DataModels.EmpDataModel;
using CAPSTONEPROJECT.DataModels.SalaryDataModel;
using CAPSTONEPROJECT.DataModels.SystemSettingDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CAPSTONEPROJECT.Services
{
    public class SalaryService
    {
        private readonly LugContext _context;
        private readonly SystemSettingService _service;

        public SalaryService(LugContext context,SystemSettingService service)
        {
            _context = context;
            _service = service;
        }

        public List<SalaryResponseModel> GetAll()
        {
            
            var query = _context.Salaries
                .Select(salary => new SalaryResponseModel
                {
                    SalaryID = salary.SalaryId,
                    EmployeeID = salary.EmployeeId,
                    FullName = _context.Employees.Where(x=>x.EmployeeId == salary.EmployeeId).Select(x=>x.FullName).First(),
                    Month = salary.Month,
                    Year = salary.Year,
                    ContractSalary = salary.CurrentContractSalary,
                    BasicWorkingTime = _context.ContractSalaries.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.BasicWorkingDayTime).FirstOrDefault(),
                    RealWorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == salary.EmployeeId && x.WorkingDate.Value.Month == salary.Month && x.WorkingDate.Value.Year == salary.Year && x.WorkScheduleStatusId == 1).Count() + _context.WorkSchedules.Where(x => x.EmployeeId == salary.EmployeeId && x.WorkingDate.Value.Month == salary.Month && x.WorkingDate.Value.Year == salary.Year && x.WorkScheduleStatusId == 2).Count(),
                    Income = salary.Income,
                    Notes = salary.Notes,
                    EmployeeTypeID = _context.Employees.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.EmployeeTypeId).FirstOrDefault(),
                    
                });

            var TempResult = new List<SalaryResponseModel>();
            foreach (var item in query)
            {
                TempResult.Add(item);
            }
            var result = new List<SalaryResponseModel>();
            foreach (var item in TempResult)
            {
                if (item.EmployeeTypeID == 1)
                {
                    item.RealWorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkingDate.Value.Month == item.Month && x.WorkingDate.Value.Year == item.Year && x.WorkScheduleStatusId == 1).Count() + _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkingDate.Value.Month == item.Month && x.WorkingDate.Value.Year == item.Year && x.WorkScheduleStatusId == 2).Count();

                    result.Add(item);
                }
                else 
                {
                    item.RealWorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkingDate.Value.Month == item.Month && x.WorkingDate.Value.Year == item.Year && x.WorkScheduleStatusId == 1).Sum(x => x.WorkHours) + (double)(_context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkingDate.Value.Month == item.Month && x.WorkingDate.Value.Year == item.Year && x.WorkScheduleStatusId == 2).Sum(x => x.WorkHours));
                    result.Add(item);
                }

            }

            

            return result;
        }

        public List<SalaryResponseModel> GetByMonthYear(int Month,int Year)
        {
            var query = _context.Salaries.Where(x=>x.Month == Month && x.Year == Year)
                .Select(salary => new SalaryResponseModel
                {
                    SalaryID = salary.SalaryId,
                    EmployeeID = salary.EmployeeId,
                    FullName = _context.Employees.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.FullName).First(),
                    Month = salary.Month,
                    Year = salary.Year,
                    ContractSalary = salary.CurrentContractSalary,
                    BasicWorkingTime = _context.ContractSalaries.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.BasicWorkingDayTime).FirstOrDefault(),
                    RealWorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == salary.EmployeeId && x.WorkingDate.Value.Month == salary.Month && x.WorkingDate.Value.Year == salary.Year && x.WorkScheduleStatusId == 1).Count() + _context.WorkSchedules.Where(x => x.EmployeeId == salary.EmployeeId && x.WorkingDate.Value.Month == salary.Month && x.WorkingDate.Value.Year == salary.Year && x.WorkScheduleStatusId == 2).Count(),
                    Income = salary.Income,
                    Notes = salary.Notes,
                    EmployeeTypeID = _context.Employees.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.EmployeeTypeId).FirstOrDefault(),

                }).OrderBy(x=> x.FullName);

            var TempResult = new List<SalaryResponseModel>();
            foreach (var item in query)
            {
                TempResult.Add(item);
            }
            var result = new List<SalaryResponseModel>();
            foreach (var item in TempResult)
            {
                if (item.EmployeeTypeID == 1)
                {
                    item.RealWorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkingDate.Value.Month == item.Month && x.WorkingDate.Value.Year == item.Year && x.WorkScheduleStatusId == 1).Count() + _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkingDate.Value.Month == item.Month && x.WorkingDate.Value.Year == item.Year && x.WorkScheduleStatusId == 2).Count();

                    result.Add(item);
                }
                else
                {
                    item.RealWorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkingDate.Value.Month == item.Month && x.WorkingDate.Value.Year == item.Year && x.WorkScheduleStatusId == 1).Sum(x => x.WorkHours) + (double)(_context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkingDate.Value.Month == item.Month && x.WorkingDate.Value.Year == item.Year && x.WorkScheduleStatusId == 2).Sum(x => x.WorkHours));
                    result.Add(item);
                }

            }



            return result;
        }

        public SalaryResponseModel GetSalaryDetaiLByMonthAndYear(string employeeID, int month, int year)
        {
            var EmployeeTypeID = _context.Employees.Where(x => x.EmployeeId == employeeID).Select(x => x.EmployeeTypeId).FirstOrDefault();
            if(EmployeeTypeID == 1)
            {
                var query = _context.Salaries.Where(x => x.EmployeeId == employeeID && x.Month == month && x.Year == year)
                .Select(salary => new SalaryResponseModel
                {
                    SalaryID = salary.SalaryId,
                    EmployeeID = salary.EmployeeId,
                    FullName = _context.Employees.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.FullName).First(),
                    Month = salary.Month,
                    Year = salary.Year,
                    BasicWorkingTime = _context.ContractSalaries.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.BasicWorkingDayTime).FirstOrDefault(),
                    RealWorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == salary.EmployeeId && x.WorkingDate.Value.Month == salary.Month && x.WorkingDate.Value.Year == salary.Year && x.WorkScheduleStatusId == 1).Count() + _context.WorkSchedules.Where(x => x.EmployeeId == salary.EmployeeId && x.WorkingDate.Value.Month == salary.Month && x.WorkingDate.Value.Year == salary.Year && x.WorkScheduleStatusId == 2).Count(),
                    ContractSalary = salary.CurrentContractSalary,
                    Income = salary.Income,
                    Notes = salary.Notes,
                    EmployeeTypeID = EmployeeTypeID,
                    
                }).FirstOrDefault();

                return query;

            }
            else
            {
                var query = _context.Salaries.Where(x => x.EmployeeId == employeeID && x.Month == month && x.Year == year)
                .Select(salary => new SalaryResponseModel
                {
                    SalaryID = salary.SalaryId,
                    EmployeeID = salary.EmployeeId,
                    FullName = _context.Employees.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.FullName).First(),
                    Month = salary.Month,
                    Year = salary.Year,
                    BasicWorkingTime = _context.ContractSalaries.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.BasicWorkingDayTime).FirstOrDefault(),
                    RealWorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == salary.EmployeeId && x.WorkingDate.Value.Month == salary.Month && x.WorkingDate.Value.Year == salary.Year && x.WorkScheduleStatusId == 1).Sum(x => x.WorkHours) + (double)(_context.WorkSchedules.Where(x => x.EmployeeId == salary.EmployeeId && x.WorkingDate.Value.Month == salary.Month && x.WorkingDate.Value.Year == salary.Year && x.WorkScheduleStatusId == 2).Sum(x => x.WorkHours)),
                    ContractSalary = salary.CurrentContractSalary,
                    Income = salary.Income,
                    Notes = salary.Notes,
                    EmployeeTypeID = EmployeeTypeID,
                   

                }).FirstOrDefault();

                return query;

            }

        }

        public SalaryResponseModel GetMonthBeforeSalary(string employeeID)
        {
            DateTime CurrentServerTime = DateTime.Now;
            DateTime CurrentLocalTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(CurrentServerTime, "SE Asia Standard Time");
            var EmployeeTypeID = _context.Employees.Where(x => x.EmployeeId == employeeID).Select(x => x.EmployeeTypeId).FirstOrDefault();
            if (EmployeeTypeID == 1)
            {
                var query = _context.Salaries.Where(x => x.EmployeeId == employeeID && x.Month == CurrentLocalTime.Month - 1)
                  .Select(salary => new SalaryResponseModel
                  {
                      SalaryID = salary.SalaryId,
                      EmployeeID = salary.EmployeeId,
                      FullName = _context.Employees.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.FullName).First(),
                      Month = salary.Month,
                      Year = salary.Year,
                      BasicWorkingTime = _context.ContractSalaries.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.BasicWorkingDayTime).FirstOrDefault(),
                      RealWorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == salary.EmployeeId && x.WorkingDate.Value.Month == salary.Month && x.WorkingDate.Value.Year == salary.Year && x.WorkScheduleStatusId == 1).Count() + _context.WorkSchedules.Where(x => x.EmployeeId == salary.EmployeeId && x.WorkingDate.Value.Month == salary.Month && x.WorkingDate.Value.Year == salary.Year && x.WorkScheduleStatusId == 2).Count(),
                      ContractSalary = _context.ContractSalaries.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.ContractSalary1).FirstOrDefault(),
                      Income = salary.Income,
                      Notes = salary.Notes,
                      EmployeeTypeID = _context.Employees.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.EmployeeTypeId).FirstOrDefault(),
                     

                  }).FirstOrDefault();

                return query;
            }
            else
            {
                var query = _context.Salaries.Where(x => x.EmployeeId == employeeID && x.Month == CurrentLocalTime.Month - 1)
                  .Select(salary => new SalaryResponseModel
                  {
                      SalaryID = salary.SalaryId,
                      EmployeeID = salary.EmployeeId,
                      FullName = _context.Employees.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.FullName).First(),
                      Month = salary.Month,
                      Year = salary.Year,
                      BasicWorkingTime = _context.ContractSalaries.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.BasicWorkingDayTime).FirstOrDefault(),
                      RealWorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == salary.EmployeeId && x.WorkingDate.Value.Month == salary.Month && x.WorkingDate.Value.Year == salary.Year && x.WorkScheduleStatusId == 1).Sum(x => x.WorkHours) + (double)(_context.WorkSchedules.Where(x => x.EmployeeId == salary.EmployeeId && x.WorkingDate.Value.Month == salary.Month && x.WorkingDate.Value.Year == salary.Year && x.WorkScheduleStatusId == 2).Sum(x => x.WorkHours)),
                      ContractSalary = _context.ContractSalaries.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.ContractSalary1).FirstOrDefault(),
                      Income = salary.Income,
                      Notes = salary.Notes,
                      EmployeeTypeID = _context.Employees.Where(x => x.EmployeeId == salary.EmployeeId).Select(x => x.EmployeeTypeId).FirstOrDefault(),
                      

                  }).FirstOrDefault();

                return query;
            }
                
        }

        public bool CalculateIncomeForEmployee()
        {
            bool status = false;
            try
            {
                DateTime CurrentServerTime = DateTime.Now;
                DateTime CurrentLocalTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(CurrentServerTime, "SE Asia Standard Time");

                var ListEmp = GetAllEmployee();

                var ListEmpHaveContract = new List<EmpResponse>();

                foreach (var item in ListEmp)
                {
                    if (ContractAvailable(item.EmployeeID))
                    {
                        ListEmpHaveContract.Add(item);
                    }
                }

                var ListEmpWorkedLastMonth = new List<EmpResponse>();
                
                foreach (var item in ListEmpHaveContract)
                {
                    if (WorkingTimeAvailable(item.EmployeeID))
                    {
                        ListEmpWorkedLastMonth.Add(item);
                    }
                }

                var ListSuitableEmp = new List<EmpResponse>();
                foreach (var item in ListEmpWorkedLastMonth)
                {
                    var salary = _context.Salaries.Any(x => x.EmployeeId == item.EmployeeID && x.Month == CurrentLocalTime.Month-1 && x.Year == CurrentLocalTime.Year);
                    if (salary != true)
                    {
                        ListSuitableEmp.Add(item);
                    }
                }
                   

                foreach (var item in ListSuitableEmp)
                {

                    var EmployeeType = _context.Employees.Where(x => x.EmployeeId == item.EmployeeID).Select(x => x.EmployeeTypeId).FirstOrDefault();
                    if (EmployeeType == 1)
                    {
                        var ContractSalary = _context.ContractSalaries.Where(x => x.EmployeeId == item.EmployeeID).Select(x => x.ContractSalary1).FirstOrDefault();
                        var RequireWorkingTime = _context.ContractSalaries.Where(x => x.EmployeeId == item.EmployeeID).Select(x => x.BasicWorkingDayTime).FirstOrDefault();
                        var WorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkScheduleStatusId == 1 && x.WorkingDate.Value.Month == CurrentLocalTime.Month - 1 && x.IsOt == false).Count() + _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkScheduleStatusId == 2 && x.WorkingDate.Value.Month == CurrentLocalTime.Month - 1 && x.IsOt == false).Count();
                        var OT = _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkScheduleStatusId == 1 && x.WorkingDate.Value.Month == CurrentLocalTime.Month - 1 && x.IsOt == true).Count()+ _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkScheduleStatusId == 2 && x.WorkingDate.Value.Month == CurrentLocalTime.Month - 1 && x.IsOt == true).Count();
                        var Income = (ContractSalary / RequireWorkingTime) * (WorkingTime+(OT*1.5));
                        var salary = new Salary
                        {
                            EmployeeId = item.EmployeeID,
                            CurrentContractSalary = ContractSalary,
                            Month = CurrentLocalTime.Month - 1,
                            Year = CurrentLocalTime.Year,
                            Income = Income,
                            Notes = " Đã tính lương tháng" + " " + (CurrentLocalTime.Month - 1),

                        };
                        if (SalaryEmpExist(salary.EmployeeId, salary.Month, salary.Year))
                        {
                            status = false;
                        }
                        else
                        {
                            _context.Add(salary);
                            status = _context.SaveChanges() > 0;
                        }
                    }
                    else
                    {
                        var ContractSalary = _context.ContractSalaries.Where(x => x.EmployeeId == item.EmployeeID).Select(x => x.ContractSalary1).FirstOrDefault();
                        var RequireWorkingTime = _context.ContractSalaries.Where(x => x.EmployeeId == item.EmployeeID).Select(x => x.BasicWorkingDayTime).FirstOrDefault();
                        var WorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkScheduleStatusId == 1 && x.WorkingDate.Value.Month == CurrentLocalTime.Month - 1 && x.IsOt ==false).Sum(x => x.WorkHours) + _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkScheduleStatusId == 2 && x.WorkingDate.Value.Month == CurrentLocalTime.Month - 1 && x.IsOt == false).Sum(x => x.WorkHours);
                        var OT = _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkScheduleStatusId == 1 && x.WorkingDate.Value.Month == CurrentLocalTime.Month - 1 && x.IsOt == true).Sum(x => x.WorkHours) + _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkScheduleStatusId == 2 && x.WorkingDate.Value.Month == CurrentLocalTime.Month - 1 && x.IsOt == true).Sum(x => x.WorkHours);
                        var Income = (ContractSalary / RequireWorkingTime) * (WorkingTime+(OT*1.5));
                        var salary = new Salary
                        {
                            EmployeeId = item.EmployeeID,
                            Month = CurrentLocalTime.Month - 1,
                            Year = CurrentLocalTime.Year,
                            CurrentContractSalary = ContractSalary,
                            Income = Income,

                            Notes = " Đã tính lương tháng" + " " + (CurrentLocalTime.Month - 1),
                        };
                        if (SalaryEmpExist(salary.EmployeeId, salary.Month, salary.Year))
                        {
                            status = false;
                        }
                        else
                        {
                            _context.Add(salary);
                            status = _context.SaveChanges() > 0;
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

        public List<EmpResponse> GetAllEmployee()
        {
            var query = _context.Employees
               .Select(emp => new EmpResponse
               {
                   EmployeeID = emp.EmployeeId,
               });

            var result = new List<EmpResponse>();

            foreach (var item in query)
            {
                result.Add(item);
            }
            return result;
        }

        public bool WorkingTimeAvailable(string EmployeeID)
        {
            bool status = false;
            DateTime CurrentServerTime = DateTime.Now;
            DateTime CurrentLocalTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(CurrentServerTime, "SE Asia Standard Time");
            var EmployeeType = _context.Employees.Where(x => x.EmployeeId == EmployeeID).Select(x => x.EmployeeTypeId).FirstOrDefault();
            if (EmployeeType == 1)
            {
                var ContractSalary = _context.ContractSalaries.Where(x => x.EmployeeId == EmployeeID).Select(x => x.ContractSalary1).FirstOrDefault();
                var WorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == EmployeeID && x.WorkScheduleStatusId == 1 && x.WorkingDate.Value.Month == CurrentLocalTime.Month - 1).Count() + _context.WorkSchedules.Where(x => x.EmployeeId == EmployeeID && x.WorkScheduleStatusId == 2 && x.WorkingDate.Value.Month == CurrentLocalTime.Month - 1).Count();
                if (WorkingTime <= 0)
                {
                    var employeeSalary = new Salary
                    {
                        EmployeeId = EmployeeID,
                        CurrentContractSalary = ContractSalary,
                        Income = 0,
                        Month = CurrentLocalTime.Month-1,
                        Year = CurrentLocalTime.Year,
                        Notes = "Nhân viên không làm việc trong tháng" +" "+(CurrentLocalTime.Month - 1)+" ",
                    };
                    if (!SalaryEmpExist(employeeSalary.EmployeeId, employeeSalary.Month, employeeSalary.Year))
                    {
                        _context.Salaries.Add(employeeSalary);
                        _context.SaveChanges();
                    }
                    

                    status = false;
                }
                else
                {
                    status = true;
                }

            }
            else
            {
                var ContractSalary = _context.ContractSalaries.Where(x => x.EmployeeId == EmployeeID).Select(x => x.ContractSalary1).FirstOrDefault();
                var WorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == EmployeeID && x.WorkScheduleStatusId == 1 && x.WorkingDate.Value.Month == CurrentLocalTime.Month - 1).Sum(x => x.WorkHours)+ _context.WorkSchedules.Where(x => x.EmployeeId == EmployeeID && x.WorkScheduleStatusId == 2 && x.WorkingDate.Value.Month == CurrentLocalTime.Month - 1).Sum(x => x.WorkHours);
                if (WorkingTime <= 0)
                {
                    var employeeSalary = new Salary
                    {
                        EmployeeId = EmployeeID,
                        CurrentContractSalary = ContractSalary,
                        Income = 0,
                        Month = CurrentLocalTime.Month-1,
                        Year = CurrentLocalTime.Year,
                        Notes = "Nhân viên không làm việc trong tháng" + " " + (CurrentLocalTime.Month - 1) + " ",
                    };
                    if (!SalaryEmpExist(employeeSalary.EmployeeId, employeeSalary.Month, employeeSalary.Year))
                    {
                        _context.Salaries.Add(employeeSalary);
                        _context.SaveChanges();
                    }
                    status = false;
                }
                else
                {
                    status = true;
                }
            }
            return status;

        }

        public bool InSalaryCalculatePeriod(DateTime CalculateDate)
        {
            bool status;
            var setting = _service.GetByID();
            if (CalculateDate.Day  >= setting.SalaryCalculateStartTime  && CalculateDate.Day <= setting.SalaryCalculateEndTime)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        public bool ContractAvailable(string EmployeeID)
        {
            DateTime CurrentServerTime = DateTime.Now;
            DateTime CurrentLocalTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(CurrentServerTime, "SE Asia Standard Time");
            var status =  _context.ContractSalaries.Any(x => x.EmployeeId == EmployeeID);
            if (status)
            {
                return status;
            }
            else
            {
                var employeeSalary = new Salary
                {
                    EmployeeId = EmployeeID,
                    Income = 0,
                    Month = CurrentLocalTime.Month - 1,
                    Year = CurrentLocalTime.Year,
                    Notes = "Nhân viên chưa có hợp đồng",
                };
                if (!SalaryEmpExist(employeeSalary.EmployeeId, employeeSalary.Month, employeeSalary.Year))
                {
                    _context.Salaries.Add(employeeSalary);
                    _context.SaveChanges();
                }
                status = false;
            }
            return status;

        }

        public bool UpdateSalaryForEmployee(int id, SalaryUpdateModel dataModel)
        {
            bool status = false;
            try
            {
                var salary = _context.Salaries.Where(x => x.SalaryId == id).FirstOrDefault();
                
                var backupSalary = new BackupSalary
                {
                    SalaryId = salary.SalaryId,
                    EmployeeId = salary.EmployeeId,
                    Month = salary.Month,
                    Year = salary.Year,
                    CurrentContractSalary = salary.CurrentContractSalary,
                    Income = salary.Income,
                    Notes = salary.Notes,
                };


                _context.BackupSalaries.Add(backupSalary);
                _context.SaveChanges();

                salary.CurrentContractSalary = dataModel.CurrentContractSalary;
                salary.Income = dataModel.Income;
                salary.Notes = dataModel.Notes;
                status = _context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            return status;
        }

        public bool InSalaryUpdatePeriod(DateTime CurrentDate)
        {
            bool status = false;
            var setting = _service.GetByID();
            if(CurrentDate.Day >= setting.SalaryUpdateStartTime && CurrentDate.Day <= setting.SalaryUpdateEndTime)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }

        public bool DeleteSalaryForEmployee(int id)
        {
            bool status = false;
            try
            {
                var backupSalaryList = _context.BackupSalaries.Where(x => x.SalaryId == id);
                foreach (var item in backupSalaryList)
                {
                    _context.BackupSalaries.Remove(item);
                }

                var salary = _context.Salaries.Where(x => x.SalaryId == id).FirstOrDefault();
                _context.Salaries.Remove(salary);
                status = _context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            return status;
        }

        public bool SalaryEmpExist(string EmpID, int? month, int? year)
        {
            return _context.Salaries.Any(x => x.EmployeeId == EmpID && x.Month == month && x.Year == year);
        }

  
    }
}
