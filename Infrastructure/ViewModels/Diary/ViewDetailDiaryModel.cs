using System;

namespace WebCoreApp.Infrastructure.ViewModels.Diary
{
    public class ViewDetailDiaryModel
    {
        public long Id { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
        public string Content_Job { get; set; }

        public string Method { get; set; }

        public string Result { get; set; }
        public int DailyId { get; set; }

        public int JobId { get; set; }
        public string Total_Job { get; set; }

        public string Comment1 { get; set; }
        public int? Level_1 { get; set; }
        public string Comment2 { get; set; }
        public int? Level_2 { get; set; }
        public string Comment3 { get; set; }
        public int? Level_3 { get; set; }
        public string Ten_Job { get; set; }
    }
}