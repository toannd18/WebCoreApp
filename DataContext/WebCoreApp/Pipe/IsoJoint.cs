using DataContext.Enums;
using DataContext.WebCoreApp.Pipe.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataContext.WebCoreApp.Pipe
{
    public partial class IsoJoint:IDateTime,IUser
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(4)]
        public string Joint { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Size { get; set; }

        [Required]
        [StringLength(4)]
        public string TypeJoint { get; set; }

        public string DrawName { get; set; }

        [StringLength(4)]
        [DefaultValue("0")]
        public string Rev { get; set; }

        [StringLength(20)]
        public string Heate1 { get; set; }

        [StringLength(20)]
        public string Heate2 { get; set; }

        public DateTime? WeldingDate { get; set; }

        public string Welder1 { get; set; }
        public string Welder2 { get; set; }
        public string Welder3 { get; set; }
        public string Welder4 { get; set; }

        public SFJoint SF { get; set; }

        public JointStatus Status { get; set; }

        public Isometric Isometric { get; set; }
        public string UserCreated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserUpdated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DateCreated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DateUpdated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}