    using CAPSTONEPROJECT.DataModels.EmployeeSkillDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CAPSTONEPROJECT.Services
{
    public class EmployeeSkillService
    {
        private readonly LugContext _context;

        public EmployeeSkillService(LugContext context)
        {
            _context = context;
        }


        public List<EmployeeSkillResponseModel> GetAll()
        {
            var query = _context.EmployeeSkills
                .Select(EmpSkill => new EmployeeSkillResponseModel
                {
                    EmployeeID = EmpSkill.EmployeeId,
                    EmployeeName = _context.Employees.Where(x => x.EmployeeId == EmpSkill.EmployeeId).Select(x => x.FullName).FirstOrDefault(),
                    SkillID = EmpSkill.SkillId,
                    SkillName = _context.Skills.Where(x => x.SkillId == EmpSkill.SkillId).Select(x => x.SkillName).FirstOrDefault(),



                });
            var result = new List<EmployeeSkillResponseModel>();
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

        public List<EmployeeSkillResponseModel> GetByID(string id)
        {
            var query = _context.EmployeeSkills.Where(x => x.EmployeeId == id)
                .Select(EmpSkill => new EmployeeSkillResponseModel
                {
                    EmployeeID = EmpSkill.EmployeeId,
                    EmployeeName = _context.Employees.Where(x => x.EmployeeId == EmpSkill.EmployeeId).Select(x => x.FullName).FirstOrDefault(),
                    SkillID = EmpSkill.SkillId,
                    SkillName = _context.Skills.Where(x => x.SkillId == EmpSkill.SkillId).Select(x => x.SkillName).FirstOrDefault(),



                });

            var result = new List<EmployeeSkillResponseModel>();
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

        public bool CreateEmployeeSkill(EmployeeSkillCreateModel dataModel)
        {
            bool status = false;
            try
            {
                var EmpSkill = new EmployeeSkill
                {
                    EmployeeId = dataModel.EmployeeID,
                    SkillId = dataModel.SkillID,
                };
                if (!EmployeeSkillExist(EmpSkill.EmployeeId, EmpSkill.SkillId))
                {
                    _context.EmployeeSkills.Add(EmpSkill);
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
                throw;
            }
            return status;
        }


        public bool DeleteEmployeeSkill(string EmpID, string SkillID)
        {
            bool status = false;
            try
            {
                var EmpSkill = _context.EmployeeSkills.Where(x => x.EmployeeId == EmpID && x.SkillId == SkillID).FirstOrDefault();
                _context.Remove(EmpSkill);
                status = _context.SaveChanges() > 0;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }

            return status;
        }

        public bool EmployeeSkillExist(string EmpID, string SkillID)
        {
            return _context.EmployeeSkills.Any(x => x.EmployeeId == EmpID && x.SkillId == SkillID);
        }

    }
}
