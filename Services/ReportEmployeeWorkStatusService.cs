using CAPSTONEPROJECT.DataModels.EmployeeReportModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.Services
{
    public class ReportEmployeeWorkStatusService
    {
        private readonly LugContext _context;

        public ReportEmployeeWorkStatusService(LugContext context)
        {
            _context = context;
        }

        public List<EmployeeReportModel> GetEmployeeByWorkingStatusByWorkplace(string WorkplaceID)
        {
            DateTime CurrentDate = DateTime.Now;
            


            var query = _context.WorkSchedules.Where(x => x.WorkplaceId == WorkplaceID && x.WorkingDate.Value.Date == CurrentDate.Date)
                .Select(empworks => new EmployeeReportModel
                {
                    EmployeeID = empworks.EmployeeId,
                    FullName = _context.Employees.Where(x=>x.EmployeeId == empworks.EmployeeId).Select(x=>x.FullName).FirstOrDefault(),
                    WorkingStatus = "1",


                });
            var result = new List<EmployeeReportModel>(); 
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

        




    }
}
