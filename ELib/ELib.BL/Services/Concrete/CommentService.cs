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
                if (Book == null)
                {
                    return null;
                }


                var Comments = uow.Repository<Comment>().Get(x => x.BookId == Book.Id, skipCount: pageCount * (pageNumb - 1), topCount: pageCount).OrderByDescending(x => x.CommentDate).ToList();


               // var Comments = uow.Repository<Comment>().Get(x => x.BookId == Book.Id).OrderByDescending(x => x.CommentDate).ToList();//change 

                foreach (var item in Comments)
                {
                    if(item.ImageHash == null || item.UserName == null)
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

                return commentList;
            }
        }
    }
}