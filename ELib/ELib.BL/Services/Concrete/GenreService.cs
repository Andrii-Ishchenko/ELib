using ELib.BL.Services.Abstract;
using ELib.Domain.Entities;
using ELib.BL.DtoEntities;
using ELib.DAL.Infrastructure.Abstract;
using ELib.BL.Mapper.Abstract;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ELib.BL.Services.Concrete
{
    public class GenreService : BaseService<Genre, GenreDto>, IGenreService
    {
        public GenreService(IUnitOfWorkFactory factory, IMapper<Genre, GenreDto> mapper)
            : base(factory, mapper)
        {
            _defaultSort = q => q.OrderBy(g => g.Name);
        }

    }
}
