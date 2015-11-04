using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Mapper.Concrete
{
    public class RatingBookMapper : IMapper<RatingBook, RatingBookDto>
    {
        public RatingBookDto Map(RatingBook input)
        {
            return new RatingBookDto() { Id = input.Id, BookId = input.BookId, UserId = input.UserId, ValueRating = input.ValueRating, State = input.State };
        }

        public RatingBook Map(RatingBookDto input)
        {
            return new RatingBook() { Id = input.Id, BookId = input.BookId, UserId = input.UserId, ValueRating = input.ValueRating, State = input.State };
        }
    }
}
