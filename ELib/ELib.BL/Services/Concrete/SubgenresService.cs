using ELib.BL.DtoEntities;
using ELib.BL.Mapper.Abstract;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Domain.Entities;

namespace ELib.BL.Services.Concrete
{
    public class SubgenreService : BaseService<Subgenre, SubgenreDto>, ISubgenreService
    {
        public SubgenreService(IUnitOfWorkFactory factory, IMapper<Subgenre, SubgenreDto> mapper)
            : base(factory, mapper)
        { }
    }
}
