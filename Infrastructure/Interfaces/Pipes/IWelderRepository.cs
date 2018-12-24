using DataContext.WebCoreApp.Pipe;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCoreApp.Infrastructure.Interfaces.Pipes
{
    public interface IWelderRepository : IGeneric<Welder, string>
    {
        Task<(IEnumerable<Welder>, int totalRecords, int filterRecord)> GetTable(int length, int page, string search, string oderId, string oderDir);
        Task<IEnumerable<Welder>> FindWelder(string id);
    }
}