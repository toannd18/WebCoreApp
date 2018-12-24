using DataContext.WebCoreApp.Pipe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebCoreApp.Infrastructure.Interfaces.Pipes
{
   public interface ITypeJointRepository: IGeneric<TypeJoint, string>
    {
        Task<(IEnumerable<TypeJoint>, int totalRecords, int filterRecord)> GetTable(int length, int page, string search, string oderId, string oderDir);
        Task<IEnumerable<TypeJoint>> Find(string id);
    }
}
