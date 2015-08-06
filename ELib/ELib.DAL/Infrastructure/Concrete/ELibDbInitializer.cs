using System.Collections.Generic;
using System.Linq;
using ELib.Domain.Entities;
using System.Data.Entity;

namespace ELib.DAL.Infrastructure.Concrete
{
    public class ELibDbInitializer : CreateDatabaseIfNotExists<ELibDbContext>
    {
        protected override void Seed(ELibDbContext context)
        {
            base.Seed(context);
        }
    }
}
