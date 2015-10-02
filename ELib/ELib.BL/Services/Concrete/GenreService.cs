using ELib.BL.Services.Abstract;
using ELib.Domain.Entities;
using ELib.BL.DtoEntities;
using ELib.DAL.Infrastructure.Abstract;
using ELib.BL.Mapper;

namespace ELib.BL.Services.Concrete
{
    public class GenreService : BaseService<Genre, GenreDto>, IGenreService
    {
        public GenreService(IUnitOfWorkFactory factory, IMapper<Genre, GenreDto> mapper)
            : base(factory, mapper)
        { }
    }
}
