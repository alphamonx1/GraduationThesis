using CAPSTONEPROJECT.DataModels.ApplicationTypeDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CAPSTONEPROJECT.Services
{
    public class ApplicationTypeService
    {
        private readonly LugContext _context;

        public ApplicationTypeService(LugContext context)
        {
            _context = context;
        }

        public List<ApplicationTypeResponseModel> GetAll()
        {
            var query = _context.ApplicationTypes
                .Select(appType => new ApplicationTypeResponseModel
                {
                    ApplicationTypeID = appType.ApplicationTypeId,
                    ApplicationTypeName = appType.ApplicationTypeName,



                });
            var result = new List<ApplicationTypeResponseModel>();
            foreach (var item in query)
            {
                result.Add(item);
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

        public bool CreateApplicationType(ApplicationTypeCreateModel dataModel)
        {
            bool status = false;
            try
            {
                var appType = new ApplicationType
                {
                    ApplicationTypeName = dataModel.ApplicationTypeName,

                };
                if (!ApplicationTypeExist(appType.ApplicationTypeName))
                {
                    _context.ApplicationTypes.Add(appType);
                    status = _context.SaveChanges() > 0;
                }
                else
                {
                    return status;
                }
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            return status;


        }

        public bool ApplicationTypeExist(string name)
        {
            return _context.ApplicationTypes.Any(x => x.ApplicationTypeName == name);
        }


    }
}
