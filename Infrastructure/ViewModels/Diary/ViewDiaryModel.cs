using System;

namespace WebCoreApp.Infrastructure.ViewModels.Diary
{
    public class ViewDiaryModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string TotalJob { get; set; }
        public string Comment1 { get; set; }
        public string UserAutho1 { get; set; }
        public string UserAutho2 { get; set; }
        public bool StatusAutho1 { get; set; }
        public bool StatusAutho2 { get; set; }
        public string UserAutho3 { get; set; }
        public bool StatusAutho3 { get; set; }
        public string Comment2 { get; set; }
        public string FullName { get; set; }
        public string FullName1 { get; set; }

        public string TenCV { get; set; }
        public string TenTo { get; set; }
    }
}