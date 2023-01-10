using CAPSTONEPROJECT.DataModels.PositionDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CAPSTONEPROJECT.Services
{
    public class PositionService
    {
        private readonly LugContext _context;

        public PositionService(LugContext context)
        {
            _context = context;
        }

        public List<PositionResponse> getAll()
        {
            var query = _context.Positions
                .Select(position => new PositionResponse
                {
                    PositionID = position.PositionId,
                    PositionName = position.PositionName,
                    DelFlag = position.DelFlag,


                }
                );

            var result = new List<PositionResponse>();
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

        public bool CreatePosition(PosCreateModel model)
        {
            bool flag = false;
            try
            {
                var pos = new Position
                {
                    PositionId = model.PositionID,
                    PositionName = model.PositionName,

                };
                if (PosExist(pos.PositionId))
                {
                    flag = false;
                }
                else
                {
                    _context.Add<Position>(pos);
                    flag = _context.SaveChanges() > 0;
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return flag;

        }

        public bool UpdatePos(string id, PosUpdateModel model)
        {
            bool flag = false;
            try
            {
                var pos = _context.Positions.Where(x => x.PositionId == id).FirstOrDefault();
                pos.PositionName = model.PositionName;
                flag = _context.SaveChanges() > 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return flag;
        }

        public bool DeletePos(string id)
        {
            bool flag = false;
            try
            {
                var pos = _context.Positions.Where(x => x.PositionId == id).FirstOrDefault();
                pos.DelFlag = true;
                flag = _context.SaveChanges() > 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return flag;
        }
        public bool PosExist(string id)
        {
            return _context.Positions.Any(x => x.PositionId == id);
        }
    }
}
