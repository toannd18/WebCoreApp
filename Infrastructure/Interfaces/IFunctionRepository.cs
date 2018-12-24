using DataContext.WebCoreApp;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.ViewModels.System;

namespace WebCoreApp.Infrastructure.Interfaces
{
    public interface IFunctionRepository : IGeneric<Functions, string>
    {
        Task<IEnumerable<Functions>> GetAllId();

        Task UpdatePermission(CustomPermissionViewModel model);
    }
}