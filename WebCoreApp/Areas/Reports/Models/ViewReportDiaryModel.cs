namespace WebCoreApp.Areas.Reports.Models
{
    public class ViewReportDiaryModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string MaBP { get; set; }
        public string Ten_phong { get; set; }
        public string MaTo { get; set; }
        public string Ten_to { get; set; }

        public int Total_Date { get; set; }
        public int Total_Work { get; set; }
        public decimal Rate { get; set; }
    }
}