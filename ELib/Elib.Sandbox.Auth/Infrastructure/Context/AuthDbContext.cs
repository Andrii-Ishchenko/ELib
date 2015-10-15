using Elib.Sandbox.Auth.Infrastructure.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elib.Sandbox.Auth.Infrastructure.Context
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext() 
            : base("AuthDb")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public static AuthDbContext Create()
        {
            return new AuthDbContext();
        }
    }
}