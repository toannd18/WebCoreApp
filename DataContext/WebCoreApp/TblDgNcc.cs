using System;
using System.Collections.Generic;

namespace DataContext.WebCoreApp
{
    public partial class TblDgNcc
    {
        public int Id { get; set; }
        public int DeXuatId { get; set; }
        public string MaNcc { get; set; }
        public bool? DgKt { get; set; }
        public int? DgTm { get; set; }
        public int? Dg { get; set; }

        public TblDeXuat DeXuat { get; set; }
        public TblNcc MaNccNavigation { get; set; }
    }
}
