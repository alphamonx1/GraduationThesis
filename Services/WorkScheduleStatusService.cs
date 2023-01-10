using CAPSTONEPROJECT.DataModels.WorkScheduleStatusDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.Services
{
    public class WorkScheduleStatusService
    {
        private readonly LugContext _context;
        public WorkScheduleStatusService(LugContext context)
        {
            _context = context;
        }

        public List<WorkScheduleStatusGetModel> GetAll()
        {
            var query = _context.WorkScheduleStatuses
                .Select(WSStatus => new WorkScheduleStatusGetModel
                {
                    WorkScheduleStatusID = WSStatus.WorkScheduleStatusId,
                    WorkSCheduleStatusName = WSStatus.WorkScheduleStatusName,


                });
            var result = new List<WorkScheduleStatusGetModel>();

            foreach (var item in query)
            {
                result.Add(item);
            }

            return result;

        }


    }
}
