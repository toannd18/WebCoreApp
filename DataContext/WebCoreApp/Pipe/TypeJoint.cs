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

        public DateTime DateCreated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DateUpdated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserCreated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserUpdated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}