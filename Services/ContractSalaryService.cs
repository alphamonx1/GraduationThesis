using CAPSTONEPROJECT.DataModels.ContractSalaryModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CAPSTONEPROJECT.Services
{
    public class ContractSalaryService
    {
        private readonly LugContext _context;
        public ContractSalaryService(LugContext context)
        {
            _context = context;
        }

        public List<ContractSalaryResponseModel> getAll()
        {
            var query = _context.ContractSalaries.
                Select(cts => new ContractSalaryResponseModel
                {
                    ContractID = cts.ContractId,
                    EmployeeID = cts.EmployeeId,
                    Fullname = _context.Employees.Where(x => x.EmployeeId == cts.EmployeeId).Select(x => x.FullName).First(),
                    ContractSalary = cts.ContractSalary1,
                    BasiscWorkingTime = cts.BasicWorkingDayTime,
                    ContractTypeID = cts.ContractTypeId,
                    SignDate = cts.SignDate,
                    ContractStartDate = cts.ContractStartDate,
                    ContractEndDate = cts.ContractEndDate,


                });
            var result = new List<ContractSalaryResponseModel>();
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

        public bool CreateContractSalaryForEmployee(ContractSalaryCreateModel dataModel)
        {
            var EmployeeType = _context.Employees.Where(x => x.EmployeeId == dataModel.EmployeeID).Select(x => x.EmployeeTypeId).FirstOrDefault();
            var BasicWorkingTime = 0;
            if (EmployeeType == 1)
            {
                BasicWorkingTime = 26;
            }
            else
            {
                BasicWorkingTime = 260;
            }
            bool status = false;
            try
            {
                var cts = new ContractSalary
                {
                    ContractId = dataModel.ContractID,
                    EmployeeId = dataModel.EmployeeID,
                    ContractSalary1 = dataModel.ContractSalary,
                    ContractTypeId = dataModel.ContractTypeID,
                    BasicWorkingDayTime = BasicWorkingTime,
                    SignDate = dataModel.SignDate,
                    ContractStartDate = dataModel.ContractStartDate,
                    ContractEndDate = dataModel.ContractEndDate,


                };
                if (!CtsExist(cts.ContractId, cts.EmployeeId))
                {
                    _context.ContractSalaries.Add(cts);
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
            }
            return status;
        }

        public bool UpdateCTS(string id, ContractSalaryUpdateModel dataModel)
        {
            bool status = false;
            try
            {
                var cts = _context.ContractSalaries.Where(x => x.ContractId == id).FirstOrDefault();
                cts.ContractSalary1 = dataModel.ContractSalary;
                cts.SignDate = dataModel.SignDate;
                cts.ContractStartDate = dataModel.ContractStartDate;
                cts.ContractEndDate = dataModel.ContractEndDate;
                status = _context.SaveChanges() > 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return status;


        }

        public bool DeleteCTS(string id)
        {
            bool status = false;
            try
            {
                var cts = _context.ContractSalaries.Where(x => x.ContractId == id).FirstOrDefault();
                _context.ContractSalaries.Remove(cts);
                status = _context.SaveChanges() > 0;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
            return status;
        }

        public bool CtsExist(string CtID, string EmpID)
        {
            return _context.ContractSalaries.Any(x => x.ContractId == CtID && x.EmployeeId == EmpID);
        }
    }
}
