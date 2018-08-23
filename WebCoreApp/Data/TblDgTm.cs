namespace WebCoreApp.Data
{
    public partial class TblDgTm
    {
        public int Id { get; set; }
        public int DgNccId { get; set; }
        public int DeXuatId { get; set; }
        public string HieuLuc { get; set; }
        public string ThoiGian { get; set; }
        public string DiaDiem { get; set; }
        public string DieuKien { get; set; }
        public string Bh { get; set; }
        public string CheDo { get; set; }
        public bool? VanChuyen { get; set; }
        public string GhiChu { get; set; }

        public TblDeXuat DeXuat { get; set; }
    }
}