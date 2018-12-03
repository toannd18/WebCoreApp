using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreApp.Extensions.Polices
{
    public class OperationRequirement: IAuthorizationRequirement
    {
       public string Actions { get; }
       public string Functions { get; }
       public OperationRequirement(string action, string function)
        {
            Actions = action;
            Functions = function;
        }
    }
}
