using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Domain.Entities;

namespace ELib.BL.Services.Concrete
{
    public class CommentService : BaseService<Comment, CommentDto>, ICommentService
    {
        public CommentService(IUnitOfWorkFactory factory)
            : base(factory)
        {
        }
   }
}