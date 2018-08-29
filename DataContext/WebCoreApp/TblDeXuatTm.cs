using System;
using System.Collections.Generic;

namespace DataContext.WebCoreApp
{
    public partial class TblDeXuatTm
    {
        public int Id { get; set; }
        public int DeXuatId { get; set; }
        public string LoaiTien { get; set; }
        public string HieuLuc { get; set; }
        public string ThoiGian { get; set; }
        public string DiaDiem { get; set; }
        public string DieuKien { get; set; }
        public string Bh { get; set; }
        public string CheDo { get; set; }
        public string GhiChu { get; set; }

        public TblDeXuat DeXuat { get; set; }
    }
}
