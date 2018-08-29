using System;
using System.Collections.Generic;

namespace DataContext.WebCoreApp
{
    public partial class TblJob
    {
        public int Id { get; set; }
        public string TenJob { get; set; }
        public string MaBp { get; set; }
        public string MaTo { get; set; }

        public TblBp MaBpNavigation { get; set; }
    }
}
