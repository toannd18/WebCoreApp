using System.Collections.Generic;

namespace WebCoreApp.Data
{
    public partial class TblBp
    {
        public TblBp()
        {
            AppUser = new HashSet<AppUser>();
            TblJob = new HashSet<TblJob>();
            TblTo = new HashSet<TblTo>();
        }

        public string MaBp { get; set; }
        public string TenBp { get; set; }

        public ICollection<AppUser> AppUser { get; set; }
        public ICollection<TblJob> TblJob { get; set; }
        public ICollection<TblTo> TblTo { get; set; }
    }
}