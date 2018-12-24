using DataContext.WebCoreApp.Pipe.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataContext.WebCoreApp.Pipe
{
    public partial class TypeJoint : IDateTime, IUser
    {
        [Key]
        [StringLength(4)]
        public string Type { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
        public string UserCreated { get; set; }
        public string UserUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}