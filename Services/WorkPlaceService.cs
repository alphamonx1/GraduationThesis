using CAPSTONEPROJECT.DataModels.WorkDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CAPSTONEPROJECT.Services
{
    public class WorkplaceService
    {
        private readonly LugContext _context;

        public WorkplaceService(LugContext context)
        {
            _context = context;
        }

        public List<WorkplaceResponse> GetAll()
        {
            var query = _context.Workplaces
                .Select(Workplace => new WorkplaceResponse
                {
                    WorkplaceID = Workplace.WorkplaceId,
                    WorkplaceName = Workplace.WorkplaceName,
                    Address = Workplace.Address,
                    BSSID = Workplace.Bssid,
                    DelFlag = Workplace.DelFlag,


                }
                );

            var result = new List<WorkplaceResponse>();
            foreach (var item in query)
            {
                if(item.DelFlag != true)
                {
                    result.Add(item);
                }
                
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

        public WorkplaceResponse GetByID(string WorkplaceID)
        {
            var query = _context.Workplaces.Where(x=>x.WorkplaceId == WorkplaceID)
                            .Select(Workplace => new WorkplaceResponse
                            {
                                WorkplaceID = Workplace.WorkplaceId,
                                WorkplaceName = Workplace.WorkplaceName,
                                Address = Workplace.Address,
                                BSSID = Workplace.Bssid,
                                DelFlag = Workplace.DelFlag,
                            }
                            ).FirstOrDefault();

            return query;

        }

        public List<WorkplaceEmployeeModel> GetByEmployeeID(string EmployeeID)
        {
            var query = _context.Employees.Where(x => x.EmployeeId == EmployeeID)
                            .Select(EmpWP => new WorkplaceEmployeeModel
                            {
                                WorkplaceID = EmpWP.WorkplaceId,
                                WorkplaceName = _context.Workplaces.Where(x => x.WorkplaceId == EmpWP.WorkplaceId).Select(x => x.WorkplaceName).FirstOrDefault(),
                                Address = _context.Workplaces.Where(x => x.WorkplaceId == EmpWP.WorkplaceId).Select(x=>x.Address).FirstOrDefault()
                            }
                            );
            var result = new List<WorkplaceEmployeeModel>();
            foreach (var item in query)
            {
                result.Add(item);
            }


            return result;
        }


        public bool CreateWorkplace(WPCreateModel dataModel)
        {
            bool status = false;
            try
            {
                var wp = new Workplace
                {
                    WorkplaceId = dataModel.WorkplaceID,
                    WorkplaceName = dataModel.WorkplaceName,
                    Address = dataModel.Address,
                    Bssid = dataModel.BSSID,
                };
                if (WorkplaceExist(wp.WorkplaceId))
                {
                    status = false;
                }
                else
                {
                    _context.Workplaces.Add(wp);
                    status = _context.SaveChanges() > 0;
                }

                   
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return status;

        }

        public bool UpdateWorkplace(string id, WPUpdateModel dataModel)
        {
            bool status = false;
            try
            {
                var wp = _context.Workplaces.Where(x => x.WorkplaceId == id).FirstOrDefault();
                wp.WorkplaceName = dataModel.WorkplaceName;
                wp.Address = dataModel.Address;
                wp.Bssid = dataModel.BSSID;
                status = _context.SaveChanges() > 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return status;
        }

        public bool DeleteWorkPlace(string id)
        {
            bool status = false;
            try
            {
                var pos = _context.Workplaces.Where(x => x.WorkplaceId == id).FirstOrDefault();
                pos.DelFlag = true;
                status = _context.SaveChanges() > 0 ? true : false;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return status;
        }
        public bool WorkplaceExist(string id)
        {
            return _context.Workplaces.Any(x => x.WorkplaceId == id);
        }
    }












}
