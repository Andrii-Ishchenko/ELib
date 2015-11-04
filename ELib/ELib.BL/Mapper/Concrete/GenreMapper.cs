﻿using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Mapper.Concrete
{
    public class GenreMapper : IMapper<Genre, GenreDto>
    {
        public Genre Map(GenreDto input)
        {
            return new Genre()
            {
                Id = input.Id,
                Name = input.Name,
                State = input.State
            };
        }

        public GenreDto Map(Genre input)
        {
            return new GenreDto()
            {
                Id = input.Id,
                Name = input.Name,
                State = input.State
            };
        }
    }
}
