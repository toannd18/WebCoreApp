using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DataContext.WebCoreApp
{
    public partial class AppUser:IdentityUser<Guid>
    {
        public AppUser()
        {
           
        }

        
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public DateTime? BrithDay { get; set; }
        public bool? Status { get; set; }
        public bool Gender { get; set; }
        
        public string MaBp { get; set; }
        public string MaTo { get; set; }
        public string MaCv { get; set; }

        public TblCv TblCv { get; set; }
        public TblBp TblBp { get; set; }
        public TblTo TblTo { get; set; }

    }
}
