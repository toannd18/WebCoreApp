using System.Collections.Generic;

namespace WebCoreApp.Data
{
    public partial class TblCv
    {
        public TblCv()
        {
            AppUser = new HashSet<AppUser>();
        }

        public string MaCv { get; set; }
        public string TenCv { get; set; }
        public int Display { get; set; }

        public ICollection<AppUser> AppUser { get; set; }
    }
}