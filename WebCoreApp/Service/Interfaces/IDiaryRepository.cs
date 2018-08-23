using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreApp.Areas.Diaries.Models;
using WebCoreApp.Data;

namespace WebCoreApp.Service.Interfaces
{
    public interface IDiaryRepository:IGeneric<TblDaily,int>
    {
        Task<(IEnumerable<ViewDiaryModel>, int totalRecords, int filterRecord)> GetTable(int length, int page, string search, string oderId, string oderDir,string maBp,string maTo);
        Task<(IEnumerable<ViewDiaryModel>, int totalRecords, int filterRecord)> UserGetTable(int length, int page, string search, string oderId, string oderDir);

        Task<ViewDiaryModel> GetViewByMa(int ma);

        Task<bool> SendDiary(int id);
    }
}
