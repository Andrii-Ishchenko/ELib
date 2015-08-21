using ELib.BL.DtoEntities;
using ELib.Domain.Entities;

namespace ELib.BL.Services.Abstract
{
    public interface IRatingCommentService : IBaseService<RatingComment, RatingCommentDto>
    {
        void AddLike(RatingCommentDto rating);
    }
}
