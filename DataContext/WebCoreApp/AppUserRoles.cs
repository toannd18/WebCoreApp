using System;
using System.Collections.Generic;

namespace DataContext.WebCoreApp
{
    public partial class AppUserRoles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public AppRole Role { get; set; }
        public AppUser User { get; set; }
    }
}
