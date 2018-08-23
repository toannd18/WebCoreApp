namespace WebCoreApp.Data
{
    public partial class TblJob
    {
        public int Id { get; set; }
        public string TenJob { get; set; }
        public string MaBp { get; set; }
        public string MaTo { get; set; }

        public TblBp MaBpNavigation { get; set; }
    }
}