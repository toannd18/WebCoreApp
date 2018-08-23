namespace WebCoreApp.Data
{
    public partial class TblDgKt
    {
        public int Id { get; set; }
        public int DgNccId { get; set; }
        public int DgKtId { get; set; }
        public int DeXuatId { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public string GhiChu { get; set; }
        public bool Dg { get; set; }
        public int? DonGia { get; set; }

        public TblDeXuat DeXuat { get; set; }
    }
}