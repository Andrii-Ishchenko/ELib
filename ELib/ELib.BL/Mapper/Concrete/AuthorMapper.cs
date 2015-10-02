using ELib.BL.DtoEntities;
using ELib.Domain.Entities;

namespace ELib.BL.Mapper.Concrete
{
    public class AuthorMapper : IMapper<Author, AuthorDto>
    {
        public Author Map(AuthorDto input)
        {
            return new Author()
            {
                Id = input.Id,
                FirstName = input.FirstName,
                LastName = input.LastName,
                DateOfBirth = input.DateOfBirth,
                DeathDate = input.DeathDate,
                Description = input.Description,
                ImageHash = input.ImageHash
            };
        }

        public AuthorDto Map(Author input)
        {
            return new AuthorDto()
            {
                Id = input.Id,
                FirstName = input.FirstName,
                LastName = input.LastName,
                DateOfBirth = input.DateOfBirth,
                DeathDate = input.DeathDate,
                Description = input.Description,
                ImageHash = input.ImageHash
            };
        }
    }
}
