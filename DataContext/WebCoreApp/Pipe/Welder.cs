using DataContext.WebCoreApp.Pipe.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataContext.WebCoreApp.Pipe
{
    public partial class Welder : IDateTime, IUser
    {
        [Key]
        [StringLength(20)]
        public string Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        public DateTime BrithDay { get; set; }

        [DefaultValue(true)]
        public bool Status { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public string UserCreated { get; set; }
        public string UserUpdated { get; set; }
        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public ICollection<WelderCertification> WelderCertifications { get; set; }
    }
}