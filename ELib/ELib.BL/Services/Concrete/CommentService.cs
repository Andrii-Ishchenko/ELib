using System.Collections.Generic;
using System.Linq;
using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Services.Concrete
{
    public class CommentService : BaseService<Comment, CommentDto>, ICommentService
    {
        public CommentService(IUnitOfWorkFactory factory, IMapper<Comment, CommentDto> mapper)
            : base(factory, mapper)
        { }

        public List<CommentDto> GetCommentsByBookId(int id)
        {
            using (var uow = _factory.Create())
            {
                List<CommentDto> commentList = new List<CommentDto>();

                var Book = uow.Repository<Book>().GetById(id);
                if (Book == null)
                {
                    return null;
                }
                var Comments = uow.Repository<Comment>().Get(x => x.BookId == Book.Id).ToList();
                foreach (var item in Comments)
                {
                    var entityDto = _mapper.Map(item);
                    commentList.Add(entityDto);
                }
                return commentList;
            }
        }
    }
}