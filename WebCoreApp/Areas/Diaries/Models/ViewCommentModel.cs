using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreApp.Areas.Diaries.Models
{
    public class ViewCommentModel
    {
        public long id { get; set; }
        public int idDaily { get; set; }
        public int level { get; set; }
        public string comment { get; set; }
        public string method { get; set; }
        public string contentJob { get; set; }
        public string result { get; set; }
        public int jobId { get; set; }
    }
}
