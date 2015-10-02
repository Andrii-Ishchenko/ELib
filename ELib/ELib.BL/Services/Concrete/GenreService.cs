using ELib.BL.Services.Abstract;
using ELib.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using ELib.BL.DtoEntities;
using System.Linq.Expressions;
using ELib.DAL.Infrastructure.Abstract;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Services.Concrete
{
    public class GenreService : BaseService<Genre, GenreDto>, IGenreService
    {
        public GenreService(IUnitOfWorkFactory factory, IMapper<Genre, GenreDto> mapper) 
            :base(factory, mapper) 
        {

        }
    }
}
