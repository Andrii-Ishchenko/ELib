using System.Collections.Generic;
using System.Linq;
using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;
using System;

namespace ELib.BL.Services.Concrete
{
    public class CommentService : BaseService<Comment, CommentDto>, ICommentService
    {
        public CommentService(IUnitOfWorkFactory factory, IMapper<Comment, CommentDto> mapper)
            : base(factory, mapper)
        {
            _defaultSort = q => q.OrderByDescending(c => c.CommentDate);
        }

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
                var Comments = uow.Repository<Comment>().Get(x => x.BookId == Book.Id, orderBy : _defaultSort).ToList();
                //var Persons = uow.Repository<Person>().Get(i => Comments.All(s => s.UserId == i.Id)).ToList();
                //var User = uow.Repository<ApplicationUser>().Get(k => Persons.All(t => t.ApplicationUserId == k.Id)).ToList();

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