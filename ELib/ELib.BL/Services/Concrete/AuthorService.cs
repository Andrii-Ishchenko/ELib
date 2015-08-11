using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Domain.Entities;

namespace ELib.BL.Services.Concrete
{
    public class AuthorService : BaseService<Author, AuthorDto>, IAuthorService
    {
        public AuthorService(IUnitOfWorkFactory factory) 
            :base(factory) 
        {

        }
    }
}
