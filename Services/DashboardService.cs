using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.Services
{
    public class DashboardService
    {
        private readonly LugContext _context;

        public DashboardService(LugContext context)
        {
            _context = context;
        }


        public int CountTotalEmployeeActive()
        {
            var result = _context.Employees.Count(x => x.DelFlag == false);
            return result;

        }

        public int CountTotalApplication()
        {
            var result = _context.Applications.Count(x =>x.ApplicationStatusId == 1);
            return result;
        }

        public int CountTotalWorkplace()
        {
            var result = _context.Workplaces.Count(x => x.DelFlag == false);
            return result;
        }
    }
}
