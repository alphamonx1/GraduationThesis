using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("Skill")]
    public partial class Skill
    {
        public Skill()
        {
            EmployeeSkills = new HashSet<EmployeeSkill>();
        }

        [Key]
        [Column("SkillID")]
        [StringLength(20)]
        public string SkillId { get; set; }
        [StringLength(50)]
        public string SkillName { get; set; }
        public bool? DelFlag { get; set; }

        [InverseProperty(nameof(EmployeeSkill.Skill))]
        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
