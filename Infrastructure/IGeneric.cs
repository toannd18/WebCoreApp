using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreApp.Infrastructure
{
   public interface IGeneric<T, K >where T:class
    {
        Task<IEnumerable<T>> GetAll();
        Task<bool> Save(T tbl);
        Task<bool> Update(T tbl);
        Task<bool> Delete(K ma);
        Task<T> GetByMa(K ma);
    }
}
