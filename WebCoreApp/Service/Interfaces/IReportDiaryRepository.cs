using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreApp.Areas.Reports.Models;

namespace WebCoreApp.Service.Interfaces
{
    public interface IReportDiaryRepository
    {
        IQueryable<ViewReportDiaryModel> GetData(DateTime dateFrom, DateTime dateTo);
    }
}
