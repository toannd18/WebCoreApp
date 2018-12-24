using DataContext.WebCoreApp.Pipe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebCoreApp.Infrastructure.Interfaces.Pipes
{
    public interface IMaterialPipeRepository: IGeneric<MaterialPipe, string>
    {
        Task<(IEnumerable<MaterialPipe>, int totalRecords, int filterRecord)> GetTable(int length, int page, string search, string oderId, string oderDir);
        Task<IEnumerable<MaterialPipe>> Find(string code);
    }
}
