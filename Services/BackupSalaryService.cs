using CAPSTONEPROJECT.DataModels.BackupSalaryDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.Services
{
    public class BackupSalaryService
    {
        private readonly LugContext _context;
        public BackupSalaryService(LugContext context)
        {
            _context = context;
        }

        public List<BackupSalaryResponseModel> GetAll()
        {
            var query = _context.BackupSalaries
                .Select(bkSalary => new BackupSalaryResponseModel
                {
                    BackupSalaryID = bkSalary.BackupSalaryId,
                    SalaryID = bkSalary.SalaryId,
                    EmployeeID = bkSalary.EmployeeId,
                    FullName = _context.Employees.Where(x=>x.EmployeeId == bkSalary.EmployeeId).Select(x=>x.FullName).FirstOrDefault(),
                    Month = bkSalary.Month,
                    Year = bkSalary.Year,
                    ContractSalary = bkSalary.CurrentContractSalary,
                    Income = bkSalary.Income,
                    Notes  = bkSalary.Notes,
                });
            var TempResult = new List<BackupSalaryResponseModel>();
            foreach (var item in query)
            {
                TempResult.Add(item);
            }
            var result = new List<BackupSalaryResponseModel>();
            foreach (var item in TempResult)
            {
                if (item.EmployeeTypeID == 1)
                {
                    item.BasicWorkingTime = 26;
                    item.RealWorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkingDate.Value.Month == item.Month && x.WorkingDate.Value.Year == item.Year && x.WorkScheduleStatusId == 1).Count() + _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkingDate.Value.Month == item.Month && x.WorkingDate.Value.Year == item.Year && x.WorkScheduleStatusId == 2).Count();
                    result.Add(item);
                }
                else 
                {
                    item.BasicWorkingTime = 260;
                    item.RealWorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkingDate.Value.Month == item.Month && x.WorkingDate.Value.Year == item.Year && x.WorkScheduleStatusId == 1).Sum(x => x.WorkHours) + (double)(_context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkingDate.Value.Month == item.Month && x.WorkingDate.Value.Year == item.Year && x.WorkScheduleStatusId == 2).Sum(x => x.WorkHours));
                    result.Add(item);
                }

            }
            return result;

        }
        
        public List<BackupSalaryResponseModel> GetListByID(int SalaryID)
        {
            var query = _context.BackupSalaries.Where(x=>x.SalaryId == SalaryID)
               .Select(bkSalary => new BackupSalaryResponseModel
               {
                   BackupSalaryID = bkSalary.BackupSalaryId,
                   SalaryID = bkSalary.SalaryId,
                   EmployeeID = bkSalary.EmployeeId,
                   FullName = _context.Employees.Where(x => x.EmployeeId == bkSalary.EmployeeId).Select(x => x.FullName).FirstOrDefault(),
                   Month = bkSalary.Month,
                   Year = bkSalary.Year,
                   ContractSalary = bkSalary.CurrentContractSalary,
                   Income = bkSalary.Income,
                   Notes = bkSalary.Notes,
               });
            var TempResult = new List<BackupSalaryResponseModel>();
            foreach (var item in query)
            {
                TempResult.Add(item);
            }
            var result = new List<BackupSalaryResponseModel>();
            foreach (var item in TempResult)
            {
                if (item.EmployeeTypeID == 1)
                {
                    item.BasicWorkingTime = 26;
                    item.RealWorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkingDate.Value.Month == item.Month && x.WorkingDate.Value.Year == item.Year && x.WorkScheduleStatusId == 1).Count() + _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkingDate.Value.Month == item.Month && x.WorkingDate.Value.Year == item.Year && x.WorkScheduleStatusId == 2).Count();
                    result.Add(item);
                }
                else
                {
                    item.BasicWorkingTime = 260;
                    item.RealWorkingTime = _context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkingDate.Value.Month == item.Month && x.WorkingDate.Value.Year == item.Year && x.WorkScheduleStatusId == 1).Sum(x => x.WorkHours) + (double)(_context.WorkSchedules.Where(x => x.EmployeeId == item.EmployeeID && x.WorkingDate.Value.Month == item.Month && x.WorkingDate.Value.Year == item.Year && x.WorkScheduleStatusId == 2).Sum(x => x.WorkHours));
                    result.Add(item);
                }

            }
            return result;
        }

        public bool DeleteBackupSalary(int id,int SalaryID)
        {
            bool status = false;
            try
            {
                var bksalary = _context.BackupSalaries.Where(x => x.BackupSalaryId == id && x.SalaryId == SalaryID).FirstOrDefault();
                _context.BackupSalaries.Remove(bksalary);
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
