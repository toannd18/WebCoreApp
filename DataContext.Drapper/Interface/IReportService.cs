using DataContext.Drapper.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Drapper.Interface
{
    public interface IReportService
    {
        Task<IEnumerable<TotalBpViewModel>> GetTotalBpAsync(DateTime dateFrom, DateTime dateTo);
    }
}
