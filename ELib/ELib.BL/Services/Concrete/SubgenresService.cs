using ELib.BL.DtoEntities;
using ELib.BL.Mapper;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Services.Concrete
{
    public class SubgenreService : BaseService<Subgenre, SubgenreDto>, ISubgenreService
    {
        public SubgenreService(IUnitOfWorkFactory factory, IMapper<Subgenre, SubgenreDto> mapper)
            : base(factory, mapper)
        { }
    }
}
