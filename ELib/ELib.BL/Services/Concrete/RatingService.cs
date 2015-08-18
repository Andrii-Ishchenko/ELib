using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Domain.Entities;

namespace ELib.BL.Services.Concrete
{
    public class RatingService : BaseService<RatingBook, RatingBookDto>, IRatingService
    {
        public RatingService(IUnitOfWorkFactory factory) 
            :base(factory) 
        {

        }
    }
}