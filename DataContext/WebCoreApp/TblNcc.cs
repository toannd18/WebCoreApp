using System;
using System.Collections.Generic;

namespace DataContext.WebCoreApp
{
    public partial class TblNcc
    {
        public TblNcc()
        {
            TblDgNcc = new HashSet<TblDgNcc>();
        }

        public string MaNcc { get; set; }
        public string TenNcc { get; set; }
        public string DiaChi { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Attn { get; set; }
        public string Email { get; set; }
        public string HangHoa { get; set; }
        public string DichVu { get; set; }
        public string Diem { get; set; }
        public int? Time { get; set; }

        public ICollection<TblDgNcc> TblDgNcc { get; set; }
    }
}
