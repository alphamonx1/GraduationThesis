using CAPSTONEPROJECT.DataModels.RoleDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CAPSTONEPROJECT.Services
{
    public class RoleService
    {
        private readonly LugContext _context;
        public RoleService(LugContext context)
        {
            _context = context;
        }

        public List<RoleResponseModel> GetAll()
        {
            var query = _context.Roles
                .Select(role => new RoleResponseModel
                {
                    RoleID = role.RoleId,
                    RoleName = role.RoleName,
                    DelFlag = role.DelFlag,
                });

            var result = new List<RoleResponseModel>();
            foreach (var item in query)
            {
                if(item.DelFlag == false)
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

        public bool CreateRole(RoleCreateModel dataModel)
        {
            bool status = false;
            try
            {
                var role = new Role
                {
                    RoleId = dataModel.RoleID,
                    RoleName = dataModel.RoleName,
                };

                if (!roleExist(role.RoleId)) {
                    _context.Roles.Add(role);
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

        public bool UpdateRole(int id , RoleUpdateModel dataModel)
        {
            bool status = false;
            try
            {
                var role = _context.Roles.Where(x => x.RoleId == id).FirstOrDefault();
                role.RoleName = dataModel.RoleName;

                status = _context.SaveChanges() > 0;


            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
            return status;


        }

        public bool DeleteRole(int id)
        {
            bool status = false;
            try
            {
                var role = _context.Roles.Where(x => x.RoleId == id).FirstOrDefault();
                role.DelFlag = true;

                status = _context.SaveChanges() > 0;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
            return status;
        }

        public bool roleExist(int id)
        {
            return _context.Roles.Any(x => x.RoleId == id);
        }
    }
}
