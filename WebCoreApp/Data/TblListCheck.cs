using System;

namespace WebCoreApp.Data
{
    public partial class TblListCheck
    {
        public long Id { get; set; }
        public int RequestId { get; set; }
        public string MaNcc { get; set; }
        public string MaTb { get; set; }
        public string YcKt { get; set; }
        public string TtKt { get; set; }
        public int YcSl { get; set; }
        public int TtSl { get; set; }
        public string DonVi { get; set; }
        public bool? Co { get; set; }
        public bool? Cq { get; set; }
        public bool? Mtr { get; set; }
        public bool? Sn { get; set; }
        public bool? Pn { get; set; }
        public bool? Other { get; set; }
        public string NoteOther { get; set; }
        public bool? Result { get; set; }
        public string Reason { get; set; }
        public string UserNhap { get; set; }
        public DateTime? DateNhap { get; set; }
        public string UserEdit { get; set; }
        public DateTime? DateEdit { get; set; }
    }
}