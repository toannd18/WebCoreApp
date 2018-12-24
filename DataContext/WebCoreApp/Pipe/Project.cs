using DataContext.WebCoreApp.Pipe.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataContext.WebCoreApp.Pipe
{
    public partial class Project : IDateTime, IUser
    {
        [Key]
        public Guid Id { get; set; }

        [Column("varchar(5)")]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public string UserCreated { get; set; }
        public string UserUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}