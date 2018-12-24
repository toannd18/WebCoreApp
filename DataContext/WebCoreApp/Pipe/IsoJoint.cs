using DataContext.WebCoreApp.Pipe.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContext.WebCoreApp.Pipe
{
    public partial class IsoJoint : IDateTime, IUser
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
        [Column(TypeName = "varchar(4)")]
        public string TypeJoint { get; set; }

        [Required]
        public string DrawName { get; set; }

        [StringLength(4)]
        [DefaultValue("0")]
        public string Rev { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Heate1 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Heate2 { get; set; }

        public DateTime? WeldingDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Welder1 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Welder2 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Welder3 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Welder4 { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string SF { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string Status { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public Isometric Isometric { get; set; }
        public string UserCreated { get; set; }
        public string UserUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}