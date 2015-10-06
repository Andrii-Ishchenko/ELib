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
    public class FavoriteMapper : IMapper<Favorite, FavoriteDto>
    {
        public Favorite Map(FavoriteDto input)
        {
            return new Favorite() {
                UserId = input.UserId,
                BookId = input.BookId,
                AdditionDate = input.AdditionDate,
                Id = input.Id
            
            };
        }

        public FavoriteDto Map(Favorite input)
        {
            return new FavoriteDto()
            {
                Id = input.Id,
                UserId = input.UserId,
                BookId = input.BookId,
                AdditionDate = input.AdditionDate
            };
        }
    }
}
