using CAPSTONEPROJECT.DataModels.SkillDataModel;
using CAPSTONEPROJECT.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace CAPSTONEPROJECT.Services
{
    public class SkillService
    {
        private readonly LugContext _context;
        public SkillService(LugContext context)
        {
            _context = context;

        }

        public List<SkillResponseModel> GetAll()
        {
            var query = _context.Skills
                .Select(skill => new SkillResponseModel
                {
                    SkillID = skill.SkillId,
                    SkillName = skill.SkillName,
                    DelFlag = skill.DelFlag,
                });
            var result = new List<SkillResponseModel>();
            foreach (var item in query)
            {
                if (item.DelFlag != true)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public bool CreateSkill(SkillCreateModel dataModel)
        {
            bool status = false;
            try
            {
                var skill = new Skill
                {
                    SkillId = dataModel.SkillID,
                    SkillName = dataModel.SkillName,
                };
                if (!SkillExist(skill.SkillId))
                {
                    _context.Skills.Add(skill);
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

        public bool UpdateSkill(string id, SkillUpdateModel dataModel)
        {
            bool status = false;
            try
            {
                var skill = _context.Skills.Where(x => x.SkillId == id).FirstOrDefault();
                skill.SkillName = dataModel.SkillName;
                status = _context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            return status;
        }

        public bool DeleteSkill(string id) { 
            bool status = false;
            try
            {
                var skill = _context.Skills.Where(x => x.SkillId == id).FirstOrDefault();
                skill.DelFlag = true;
                status = _context.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            return status;
        }


        public bool SkillExist(string id)
        {
            return _context.Skills.Any(x => x.SkillId == id);
        }


    }
}
