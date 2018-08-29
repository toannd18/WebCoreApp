using System.Collections.Generic;
using System.Threading.Tasks;
using WebCoreApp.Areas.Diaries.Models;
using DataContext.WebCoreApp;

namespace WebCoreApp.Service.Interfaces
{
    public interface IDetailDiaryRepository : IGeneric<TblDailyDetail, long>

    {
        Task<(IEnumerable<ViewDetailDiaryModel>, int totalRecords, int filterRecord)> GetTable(int length, int page, string search, string oderId, string oderDir, int Id);

        void Calculator(int ma);

        Task<bool> SaveCommentAll(ViewCommentModel tbl,int display);
    }
}