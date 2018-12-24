using DataContext.WebCoreApp.Pipe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebCoreApp.Infrastructure.Interfaces.Pipes
{
   public interface IIsoJointRepository:IGeneric<IsoJoint, Guid>
    {
        Task<(IEnumerable<IsoJoint>, int totalRecords, int filterRecord)> GetTable(int length, int page, string search, string oderId, string oderDir, string isoMetric);

        bool ExistJoint(string joint, string drawName);

        Task<IEnumerable<IsoJoint>> GetWelding(string isoName, bool weldingDate);
    }
}
