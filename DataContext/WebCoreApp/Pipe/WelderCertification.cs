using DataContext.WebCoreApp.Pipe.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataContext.WebCoreApp.Pipe
{
    public partial class WelderCertification: IDateTime, IUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Certification { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public string IdWelder { get; set; }
        [Required]
        public DateTime CerDate { get; set; }
        public string UserCreated { get; set; }
        public string UserUpdated { get; set; }
        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
        [StringLength(255)]
        public string Remark { get; set; }

        public Welder Welder { get; set; }
    }
}