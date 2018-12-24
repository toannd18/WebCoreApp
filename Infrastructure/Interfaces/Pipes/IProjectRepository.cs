using DataContext.WebCoreApp.Pipe;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCoreApp.Infrastructure.Interfaces.Pipes
{
    public interface IProjectRepository : IGeneric<Project, Guid>

    {
        Task<(IEnumerable<Project>, int totalRecords, int filterRecord)> GetTable(int length, int page, string search, string oderId, string oderDir);

        Task<bool> Exist(string Name);
    }
}