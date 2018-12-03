using System;
using System.Collections.Generic;
using System.Text;

namespace DataContext.WebCoreApp.Pipe.Interfaces
{
    public interface IDateTime
    {
         DateTime DateCreated { get; set; }

         DateTime DateUpdated { get; set; }
    }
}
