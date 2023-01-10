using CAPSTONEPROJECT.DataModels.ShiftDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CAPSTONEPROJECT.Services
{
    public class ShiftService
    {
        private readonly LugContext _context;
        public ShiftService(LugContext context)
        {
            _context = context;
        }

        public List<ShiftResponseModel> GetAll()
        {
            var query = _context.Shifts
                .Select(Shift => new ShiftResponseModel
                {
                    ShiftId = Shift.ShiftId,
                    ShiftName = Shift.ShiftName,
                    StartTimeHours = Shift.StartTime.Value.Hours,
                    StartTimeMin = Shift.StartTime.Value.Minutes,
                    EndTimeHours = Shift.EndTime.Value.Hours,
                    EndTimeMin = Shift.EndTime.Value.Minutes,
                    DelFlag = Shift.DelFlag,
                });
            var result = new List<ShiftResponseModel>();
            foreach (var item in query)
            {
                if(item.DelFlag != true)
                {
                    result.Add(item);
                }

            }
            if(result != null)
            {
                return result;
            }
            else
            {
                return null;    
            }
            
        }

        public List<WorkScheduleShiftResponseModel> GetByWorkingDate(String EmployeeID,DateTime WorkingDate)
        {
            var query = _context.WorkSchedules.Where(x => x.WorkingDate == WorkingDate && x.EmployeeId == EmployeeID && x.WorkScheduleStatusId == 4)
                .Select( shiftemp => new WorkScheduleShiftResponseModel
                {
                    ShiftID = (int) shiftemp.ShiftId,
                    ShiftName = _context.Shifts.Where(x=>x.ShiftId == shiftemp.ShiftId).Select(x=>x.ShiftName).FirstOrDefault(),
                    ShiftStartHours = _context.Shifts.Where(x => x.ShiftId == shiftemp.ShiftId).Select(x=>x.StartTime.Value.Hours).FirstOrDefault(),
                    ShiftStartMinutes = _context.Shifts.Where(x => x.ShiftId == shiftemp.ShiftId).Select(x=>x.StartTime.Value.Minutes).FirstOrDefault(),
                    ShiftEndHours = _context.Shifts.Where(x => x.ShiftId == shiftemp.ShiftId).Select(x=>x.EndTime.Value.Hours).FirstOrDefault(),
                    ShiftEndMinutes = _context.Shifts.Where(x => x.ShiftId == shiftemp.ShiftId).Select(x=> x.EndTime.Value.Minutes).FirstOrDefault(),

                });
            var result = new List<WorkScheduleShiftResponseModel>();
            foreach (var item in query)
            {
                result.Add(item);
            }

            return result;
            
        }

        public bool CreateShift(ShiftCreateModel dataModel)
        {
            bool status = false;
            try
            {
                TimeSpan startT = TimeSpan.Parse(dataModel.StartTime);
                TimeSpan endT = TimeSpan.Parse(dataModel.EndTime);
                if(startT > endT)
                {
                    status = false;
                }
                else
                {
                    var shift = new Shift
                    {
                        ShiftName = dataModel.ShiftName,
                        StartTime = startT,
                        EndTime = endT,
                    };
                    if (!ShiftExist(shift.ShiftId, shift.ShiftName))
                    {
                        _context.Shifts.Add(shift);
                        status = _context.SaveChanges() > 0;
                    }
                    else
                    {
                        return status;
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

        public bool UpdateShift(int id, ShiftUpdateModel dataModel)
        {
            bool status = false;
            try
            {
                TimeSpan startT = TimeSpan.Parse(dataModel.StartTime);
                TimeSpan endT = TimeSpan.Parse(dataModel.EndTime);

                var shift = _context.Shifts.Where(x => x.ShiftId == id).FirstOrDefault();
                shift.ShiftName = dataModel.ShiftName;
                shift.StartTime = startT;
                shift.EndTime = endT;
                status = _context.SaveChanges() > 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            return status;


        }

        public bool DeleteShift(int id)
        {
            bool status = false;
            try
            {
                var shift = _context.Shifts.Where(x => x.ShiftId == id).FirstOrDefault();
                _context.Shifts.Remove(shift);
                status = _context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            return status;
        }


        public bool ShiftExist(int id,string name)
        {
            return _context.Shifts.Any(x => x.ShiftId == id && x.ShiftName == name);
        }

    }
}
