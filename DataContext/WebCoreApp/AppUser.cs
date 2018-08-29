using System;
using System.Collections.Generic;

namespace DataContext.WebCoreApp
{
    public partial class AppUser
    {
        public AppUser()
        {
            AppUserRoles = new HashSet<AppUserRoles>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public DateTime? BrithDay { get; set; }
        public bool? Status { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public string MaBp { get; set; }
        public string MaTo { get; set; }
        public string MaCv { get; set; }

        public TblBp MaBpNavigation { get; set; }
        public TblCv MaCvNavigation { get; set; }
        public TblTo MaToNavigation { get; set; }
        public ICollection<AppUserRoles> AppUserRoles { get; set; }
    }
}
