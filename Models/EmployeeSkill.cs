using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("EmployeeSkill")]
    public partial class EmployeeSkill
    {
        [Key]
        [Column("EmployeeID")]
        [StringLength(20)]
        public string EmployeeId { get; set; }
        [Key]
        [Column("SkillID")]
        [StringLength(20)]
        public string SkillId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("EmployeeSkills")]
        public virtual Employee Employee { get; set; }
        [ForeignKey(nameof(SkillId))]
        [InverseProperty("EmployeeSkills")]
        public virtual Skill Skill { get; set; }
    }
}
