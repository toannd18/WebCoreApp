namespace WebCoreApp.Data
{
    public partial class TblDeXuatKt
    {
        public int Id { get; set; }
        public int DeXuatId { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public int SoLuong { get; set; }
        public string Dvt { get; set; }
        public string GhiChu { get; set; }

        public TblDeXuat DeXuat { get; set; }
    }
}