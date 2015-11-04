using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Mapper.Concrete
{
    public class RatingCommentMapper : IMapper<RatingComment, RatingCommentDto>
    {
        public RatingCommentDto Map(RatingComment input)
        {
            return new RatingCommentDto() {Id = input.Id, CommentId = input.CommentId, UserId = input.UserId, IsLike = input.IsLike, State = input.State };
        }

        public RatingComment Map(RatingCommentDto input)
        {
            return new RatingComment() { Id = input.Id, CommentId = input.CommentId, UserId = input.UserId, IsLike = input.IsLike, State = input.State };
        }
    }
}