using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreApp.Data;
using WebCoreApp.Enume;

namespace WebCoreApp.Service.Interfaces
{
   public interface IUserRepository:IGeneric<AppUser,string>
    {
      
        Task<Status> IsUser(string user, string password);

        Task<(IEnumerable<AppUser>, int totalRecords, int filterRecord)> GetTable(int start, int lenght, string oderId, string oderDir, string search);
    
    }
}
