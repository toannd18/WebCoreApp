using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContext.WebCoreApp;
using WebCoreApp.Infrastructure.Enume;

namespace WebCoreApp.Infrastructure.Interfaces
{
   public interface IUserRepository:IGeneric<AppUser,Guid>
    {
      
        Task<Status> IsUser(string user, string password);

        Task<(IEnumerable<AppUser>, int totalRecords, int filterRecord)> GetTable(int start, int lenght, string oderId, string oderDir, string search);

        Task<Status> RemoveFromRole(Guid user, Guid role);
    
    }
}
