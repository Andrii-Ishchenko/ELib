namespace ELib.Sandbox.Auth.Migrations
{
    using Infrastructure.Context;
    using Infrastructure.Identity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ELib.Sandbox.Auth.Infrastructure.Context.AuthDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ELib.Sandbox.Auth.Infrastructure.Context.AuthDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new AuthDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "JohnDoe",
                Email = "john_doe@mymail.com",
                EmailConfirmed = true,
                FirstName = "John",
                LastName = "Doe"
            };

            manager.Create(user, "11");
        }
    }
}
