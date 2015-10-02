using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Mapper.Concrete
{
    public class CommentMapper : IMapper<Comment, CommentDto>
    {
        public Comment Map(CommentDto input)
        {
            return new Comment()
            {
                Id = input.Id,
                Text = input.Text,
                BookId = input.BookId,
                UserId = input.UserId,
                CommentDate = input.CommentDate,
                SumDisLike = input.SumDisLike,
                SumLike = input.SumLike
            };
        }

        public CommentDto Map(Comment input)
        {
            return new CommentDto()
            {
                Id = input.Id,
                Text = input.Text,
                BookId = input.BookId,
                UserId = input.UserId,
                CommentDate = input.CommentDate,
                SumDisLike = input.SumDisLike,
                SumLike = input.SumLike
            };
        }
    }
}
