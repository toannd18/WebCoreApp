using System.Collections.Generic;

namespace WebCoreApp.Data
{
    public partial class TblTo
    {
        public TblTo()
        {
            AppUser = new HashSet<AppUser>();
        }

        public string MaTo { get; set; }
        public string TenTo { get; set; }
        public string MaBp { get; set; }

        public TblBp MaBpNavigation { get; set; }
        public ICollection<AppUser> AppUser { get; set; }
    }
}