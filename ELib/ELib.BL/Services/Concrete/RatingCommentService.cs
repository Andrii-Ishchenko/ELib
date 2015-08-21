using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Domain.Entities;
using System.Collections.Generic;

namespace ELib.BL.Services.Concrete
{
    public class RatingCommentService : BaseService<RatingComment, RatingCommentDto>, IRatingCommentService
    {
        public RatingCommentService(IUnitOfWorkFactory factory)
            : base(factory)
        {

        }

        public void AddLike(RatingCommentDto rating)
        {

        }
    }
}