using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContext.WebCoreApp;

namespace WebCoreApp.Service.Interfaces
{
   public interface IToRepository
    {
        Task<IEnumerable<TblTo>> GetAll();
        Task<bool> Save(TblTo tbl,bool update);
        Task<bool> Delete(string ma);
        Task<TblTo> GetByMa(string ma);

       
    }
}
