using ELib.BL.DtoEntities;
using ELib.BL.Mapper.Abstract;
using ELib.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Mapper.Concrete
{
    public class FavoriteListMapper : IMapper<Favorite, FavoriteListDto>
    {
        IMapper<Book, BookDto> _bookMapper;

        public Favorite Map(FavoriteListDto input)
        {
            return new Favorite()
            {
                Id = input.Id,
                UserId = input.UserId,
                BookId = input.BookId,
                AdditionDate = input.AdditionDate
            };
        }

        public FavoriteListDto Map(Favorite input)
        {
            return new FavoriteListDto()
            {
                Id = input.Id,
                BookId = input.Id,
                UserId = input.UserId,
                UserName = input.User.FirstName + " " + input.User.LastName,
                Book = _bookMapper.Map(input.Book),
                AdditionDate = input.AdditionDate
            };
        }
    }
}
