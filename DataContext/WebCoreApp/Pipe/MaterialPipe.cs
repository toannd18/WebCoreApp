using DataContext.WebCoreApp.Pipe.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataContext.WebCoreApp.Pipe
{
    public partial class MaterialPipe: IDateTime, IUser
    {
        [Key]
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public string UserCreated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserUpdated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DateCreated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DateUpdated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
