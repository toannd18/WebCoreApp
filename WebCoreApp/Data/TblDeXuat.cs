using System;
using System.Collections.Generic;

namespace WebCoreApp.Data
{
    public partial class TblDeXuat
    {
        public TblDeXuat()
        {
            TblDeXuatHd = new HashSet<TblDeXuatHd>();
            TblDeXuatKt = new HashSet<TblDeXuatKt>();
            TblDeXuatTm = new HashSet<TblDeXuatTm>();
            TblDgKt = new HashSet<TblDgKt>();
            TblDgNcc = new HashSet<TblDgNcc>();
            TblDgTm = new HashSet<TblDgTm>();
        }

        public int Id { get; set; }
        public string Ma { get; set; }
        public string TieuDe { get; set; }
        public bool Kieu { get; set; }
        public string TenDx { get; set; }
        public string TenDx1 { get; set; }
        public string TenDx2 { get; set; }
        public string TenDx3 { get; set; }
        public string TenDx4 { get; set; }
        public string TenDx5 { get; set; }
        public string TenDg { get; set; }
        public string TenDg1 { get; set; }
        public string TenDg2 { get; set; }
        public string TenDg3 { get; set; }
        public string TenDg4 { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayExp { get; set; }
        public DateTime? NgayEval { get; set; }
        public DateTime? NgayGui { get; set; }
        public DateTime? NgayPd { get; set; }
        public DateTime? NgayHd { get; set; }
        public DateTime? NgayPhd { get; set; }
        public DateTime? NgayKy { get; set; }
        public DateTime? NgayTh { get; set; }
        public DateTime? NgayThtt { get; set; }
        public DateTime? NgayNt { get; set; }
        public DateTime? NgayNtQc { get; set; }
        public bool Status { get; set; }
        public string GhiChu { get; set; }

        public ICollection<TblDeXuatHd> TblDeXuatHd { get; set; }
        public ICollection<TblDeXuatKt> TblDeXuatKt { get; set; }
        public ICollection<TblDeXuatTm> TblDeXuatTm { get; set; }
        public ICollection<TblDgKt> TblDgKt { get; set; }
        public ICollection<TblDgNcc> TblDgNcc { get; set; }
        public ICollection<TblDgTm> TblDgTm { get; set; }
    }
}