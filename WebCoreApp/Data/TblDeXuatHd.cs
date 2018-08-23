using System;

namespace WebCoreApp.Data
{
    public partial class TblDeXuatHd
    {
        public int Id { get; set; }
        public int DeXuatId { get; set; }
        public int SoHd { get; set; }
        public DateTime Date { get; set; }
        public string MaNcc { get; set; }
        public int ChatLuong { get; set; }
        public int TienDo { get; set; }
        public int GiaCa { get; set; }
        public int ThaiDo { get; set; }
        public string Diem { get; set; }
        public string Author { get; set; }

        public TblDeXuat DeXuat { get; set; }
    }
}