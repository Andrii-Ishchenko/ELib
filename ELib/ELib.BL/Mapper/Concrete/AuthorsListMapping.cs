using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELib.Domain.Entities;
using ELib.BL.DtoEntities;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Mapper.Concrete
{
    public class AuthorsListMapper : IMapper<Author, AuthorListDto>
    {
        public Author Map(AuthorListDto input)
        {
            Author result = new Author()
            {
                Id = input.Id,
            };
            string[] names = input.Name.Split(' ');
            result.FirstName = names[0];
            result.LastName = names[1];
            return result;
        }

        public AuthorListDto Map(Author input)
        {
            AuthorListDto result = new AuthorListDto()
            {
                Id = input.Id,
                Name = input.FirstName + " " + input.LastName
            };
            return result;
        }
    }
}
