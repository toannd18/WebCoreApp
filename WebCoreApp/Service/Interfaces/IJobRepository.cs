using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContext.WebCoreApp;

namespace WebCoreApp.Service.Interfaces
{
    public interface IJobRepository:IGeneric<TblJob,int>
    {
        Task<IEnumerable<TblJob>> GetJobList(string to, string bp);
    }
}
