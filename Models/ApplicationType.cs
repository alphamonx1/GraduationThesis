using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CAPSTONEPROJECT.Models
{
    [Table("ApplicationType")]
    public partial class ApplicationType
    {
        public ApplicationType()
        {
            Applications = new HashSet<Application>();
        }

        [Key]
        [Column("ApplicationTypeID")]
        public int ApplicationTypeId { get; set; }
        [StringLength(20)]
        public string ApplicationTypeName { get; set; }

        [InverseProperty(nameof(Application.ApplicationType))]
        public virtual ICollection<Application> Applications { get; set; }
    }
}
