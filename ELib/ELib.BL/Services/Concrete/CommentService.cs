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

        public List<CommentDto> GetCommentsByBookId(int id, int pageCount, int pageNumb)
        {
            using (var uow = _factory.Create())
            {
                List<CommentDto> commentList = new List<CommentDto>();

                var Book = uow.Repository<Book>().GetById(id);
                if (Book == null | pageCount <= 0 | pageNumb <= 0)
                {
                    return null;
                }

                int TotalCount = uow.Repository<Comment>().Get(x => x.BookId == Book.Id).Count();

                int skip;

                if (pageCount * pageNumb > TotalCount)
                {

                    if (TotalCount % pageCount >= 0)
                    {
                        if (TotalCount / pageCount +1 >= pageNumb)
                            pageCount = TotalCount % pageCount;
                        else
                            return null;
                    }
                    skip = 0;
                }
                else
                {
                    skip = TotalCount - pageCount * pageNumb;
                }

                if (pageCount == 0 && skip == 0)
                    return null;
                else
                {
                    var Comments = uow.Repository<Comment>().Get(x => x.BookId == Book.Id, skipCount: skip, topCount: pageCount).Reverse().ToList().OrderByDescending(x => x.CommentDate);

                    foreach (var item in Comments)
                    {
                        if (item.ImageHash == null || item.UserName == null)
                        {
                            var Person = uow.Repository<Person>().Get(i => i.Id == item.UserId).First();
                            var User = uow.Repository<ApplicationUser>().Get(k => k.Id == Person.ApplicationUserId).First();
                            item.ImageHash = Person.ImageHash;
                            item.UserName = User.UserName;
                            uow.Repository<Comment>().Update(item);
                        }

                        var entityDto = _mapper.Map(item);
                        commentList.Add(entityDto);
                    }
                    uow.Save();
                }
                return commentList;
            }
        }
    }
}