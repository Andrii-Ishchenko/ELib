using ELib.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ELib.Tests.Fake
{
    public class FakeContext : IdentityDbContext<ApplicationUser>
    {
        public FakeContext(){ }

        public IDbSet<Book> Books { get; set; }
    }
}
