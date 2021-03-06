﻿using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Mapper.Concrete
{
    public class AuthorMapper : IMapper<Author, AuthorDto>
    {
        public Author Map(AuthorDto input)
        {
            if (input == null)
                return null;
            return new Author()
            {
                Id = input.Id,
                FirstName = input.FirstName,
                LastName = input.LastName,
                DateOfBirth = input.DateOfBirth,
                DeathDate = input.DeathDate,
                Description = input.Description,
                ImageHash = input.ImageHash,
                State = input.State
            };
        }

        public AuthorDto Map(Author input)
        {
            if (input == null)
                return null;
            return new AuthorDto()
            {
                Id = input.Id,
                FirstName = input.FirstName,
                LastName = input.LastName,
                DateOfBirth = input.DateOfBirth,
                DeathDate = input.DeathDate,
                Description = input.Description,
                ImageHash = input.ImageHash,
                State = input.State
            };
        }
    }
}
