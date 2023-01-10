using CAPSTONEPROJECT.DataModels.EmployeeTypeDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.Services
{
    public class EmployeeTypeService
    {
        private readonly LugContext _context;

        public EmployeeTypeService(LugContext context)
        {
            _context = context;
        }

        public List<EmployeeTypeResponseModel> GetAll()
        {
            var query = _context.EmployeeTypes
                .Select(EmployeeType => new EmployeeTypeResponseModel
                {
                    EmployeeTypeID = EmployeeType.EmployeeTypeId,
                    EmployeeTypeName = EmployeeType.EmployeeTypeName,




                });
            var result = new List<EmployeeTypeResponseModel>();
            foreach (var item in query)
            {
                result.Add(item);
            }

            return result;
        }

    }
}
