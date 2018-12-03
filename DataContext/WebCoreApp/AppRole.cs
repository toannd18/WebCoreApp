using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DataContext.WebCoreApp
{
    public partial class AppRole:IdentityRole<Guid>
    {
        public AppRole()
        {
           
        
        }

        
        
        public string Description { get; set; }

      
       
    }
}
