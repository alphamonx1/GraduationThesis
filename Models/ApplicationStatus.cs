using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("ApplicationStatus")]
    public partial class ApplicationStatus
    {
        public ApplicationStatus()
        {
            Applications = new HashSet<Application>();
        }

        [Key]
        [Column("ApplicationStatusID")]
        public int ApplicationStatusId { get; set; }
        [StringLength(50)]
        public string ApplicationStatusName { get; set; }

        [InverseProperty(nameof(Application.ApplicationStatus))]
        public virtual ICollection<Application> Applications { get; set; }
    }
}
