using DataContext.WebCoreApp.Pipe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebCoreApp.Infrastructure.Interfaces.Pipes
{
    public interface IIsometricRepository: IGeneric<Isometric,string>
    {
        Task<(IEnumerable<Isometric>, int totalRecords, int filterRecord)> GetTable(int length, int page, string search, string oderId, string oderDir);

        Task<bool> Exist(string isoName, Guid project);
    }
}
