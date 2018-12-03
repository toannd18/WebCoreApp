using DataContext.Drapper.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Drapper.Interface
{
   public interface IWareHouseService
    {
        Task<IEnumerable<DMVTViewModel>> GetDMVT(string nhomVT);
        Task<IEnumerable<NhomVTViewModel>> GetNhomVT();
    }
}
