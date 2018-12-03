using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.WebCoreApp.Pipe.Interfaces
{
    public interface IUser
    {
       string UserCreated { get; set; }
       string UserUpdated { get; set; }
    }
}
