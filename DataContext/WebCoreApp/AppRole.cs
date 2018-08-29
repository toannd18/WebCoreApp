using System;
using System.Collections.Generic;

namespace DataContext.WebCoreApp
{
    public partial class AppRole
    {
        public AppRole()
        {
            AppUserRoles = new HashSet<AppUserRoles>();
            Permissions = new HashSet<Permissions>();
        }

        public int Id { get; set; }
        public string Role { get; set; }
        public string Description { get; set; }

        public ICollection<AppUserRoles> AppUserRoles { get; set; }
        public ICollection<Permissions> Permissions { get; set; }
    }
}
