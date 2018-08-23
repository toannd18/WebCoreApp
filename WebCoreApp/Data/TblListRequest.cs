using System;

namespace WebCoreApp.Data
{
    public partial class TblListRequest
    {
        public int Id { get; set; }
        public int FirstId { get; set; }
        public string LateId { get; set; }
        public string MaBp { get; set; }
        public string DiaDiem { get; set; }
        public string HangMuc { get; set; }
        public string HopDong { get; set; }
        public string DeXuat { get; set; }
        public bool Co { get; set; }
        public bool Cq { get; set; }
        public bool Other { get; set; }
        public string Note { get; set; }
        public string UserNhap { get; set; }
        public DateTime DateNhap { get; set; }
        public string UserEdit { get; set; }
        public DateTime? DateEdit { get; set; }
        public string UserAutho { get; set; }
        public DateTime? DateAutho { get; set; }
        public string StatusAutho { get; set; }
        public string NoteAutho { get; set; }
        public DateTime? Date { get; set; }
    }
}