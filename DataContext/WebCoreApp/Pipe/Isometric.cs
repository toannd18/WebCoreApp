﻿using DataContext.WebCoreApp.Pipe.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataContext.WebCoreApp.Pipe
{
    public partial class Isometric : IDateTime, IUser
    {
        [Key]
        public string DrawName { get; set; }

        [StringLength(4)]
        [DefaultValue("0")]
        public string Rev { get; set; }

        [Required]
        [StringLength(4)]
        public string Unit { get; set; }

        [Required]
        [StringLength(4)]
        public string PipeClass { get; set; }

        [Required]
        [StringLength(4)]
        public string Line { get; set; }

        [DefaultValue(0)]
        public decimal Size { get; set; }

        [Required]
        [StringLength(4)]
        public string Type { get; set; }

        [Required]
        [StringLength(20)]
        public string Material { get; set; }

        public Guid Project { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public string UserCreated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserUpdated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DateCreated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DateUpdated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ICollection<IsoJoint> IsoJoints { get; set; }
    }
}