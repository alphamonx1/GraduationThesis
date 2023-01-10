using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("Employee")]
    public partial class Employee
    {
        public Employee()
        {
            Applications = new HashSet<Application>();
            CheckAttendanceFeedbacks = new HashSet<CheckAttendanceFeedback>();
            ContractSalaries = new HashSet<ContractSalary>();
            EmployeeSkills = new HashSet<EmployeeSkill>();
            Salaries = new HashSet<Salary>();
            WorkSchedules = new HashSet<WorkSchedule>();
        }

        [Key]
        [Column("EmployeeID")]
        [StringLength(20)]
        public string EmployeeId { get; set; }
        public string Image { get; set; }
        [StringLength(50)]
        public string FullName { get; set; }
        [StringLength(10)]
        public string Gender { get; set; }
        [StringLength(20)]
        public string Ethnic { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(20)]
        public string PlaceOfBirth { get; set; }
        [StringLength(10)]
        public string PhoneNumber { get; set; }
        [StringLength(62)]
        public string Email { get; set; }
        [StringLength(10)]
        public string MaritalStatus { get; set; }
        [StringLength(10)]
        public string Religion { get; set; }
        [StringLength(50)]
        public string PermanentAddress { get; set; }
        [StringLength(50)]
        public string TemporaryAddress { get; set; }
        [Column("WorkplaceID")]
        [StringLength(10)]
        public string WorkplaceId { get; set; }
        [Column("EmployeeTypeID")]
        public int? EmployeeTypeId { get; set; }
        public int? TotalOffDay { get; set; }
        [Column("PositionID")]
        [StringLength(10)]
        public string PositionId { get; set; }
        [Column("IDCardDateOfIssue", TypeName = "date")]
        public DateTime? IdcardDateOfIssue { get; set; }
        [Column("IDCardIssueBy")]
        [StringLength(50)]
        public string IdcardIssueBy { get; set; }
        [StringLength(50)]
        public string Contact { get; set; }
        public bool? DelFlag { get; set; }

        [ForeignKey(nameof(EmployeeTypeId))]
        [InverseProperty("Employees")]
        public virtual EmployeeType EmployeeType { get; set; }
        [ForeignKey(nameof(PositionId))]
        [InverseProperty("Employees")]
        public virtual Position Position { get; set; }
        [ForeignKey(nameof(WorkplaceId))]
        [InverseProperty("Employees")]
        public virtual Workplace Workplace { get; set; }
        [InverseProperty("AccountNavigation")]
        public virtual Account Account { get; set; }
        [InverseProperty(nameof(Application.Employee))]
        public virtual ICollection<Application> Applications { get; set; }
        [InverseProperty(nameof(CheckAttendanceFeedback.Employee))]
        public virtual ICollection<CheckAttendanceFeedback> CheckAttendanceFeedbacks { get; set; }
        [InverseProperty(nameof(ContractSalary.Employee))]
        public virtual ICollection<ContractSalary> ContractSalaries { get; set; }
        [InverseProperty(nameof(EmployeeSkill.Employee))]
        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
        [InverseProperty(nameof(Salary.Employee))]
        public virtual ICollection<Salary> Salaries { get; set; }
        [InverseProperty(nameof(WorkSchedule.Employee))]
        public virtual ICollection<WorkSchedule> WorkSchedules { get; set; }
    }
}
