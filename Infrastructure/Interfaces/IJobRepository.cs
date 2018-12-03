using DataContext.WebCoreApp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCoreApp.Infrastructure.Interfaces
{
    public interface IJobRepository : IGeneric<TblJob, int>
    {
        Task<IEnumerable<TblJob>> GetJobList(string to, string bp);
    }
}