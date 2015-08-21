using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Domain.Entities;

namespace ELib.BL.Services.Concrete
{
    public class RatingService : BaseService<RatingBook, RatingBookDto>, IRatingService
    {
        public RatingService(IUnitOfWorkFactory factory)
            : base(factory)
        {
            
        }

        
        public void AddRating(RatingBookDto rating)
        {
            using (var uow = _factory.Create())
            {
              var tempRating = uow.Repository<RatingBook>().Get(x => x.BookId == rating.BookId && x.UserId == rating.UserId);
              if (tempRating != null)
                {
                    foreach(var temp in tempRating)
                    {
                       rating.Id = temp.Id;
                       var entityToUpdate = AutoMapper.Mapper.Map<RatingBook>(rating);
                       base.Update(rating);
                    }
                }
              else
                {
                    var entityToInsert = AutoMapper.Mapper.Map<RatingBook>(rating);
                    uow.Repository<RatingBook>().Update(entityToInsert);
                    uow.Save();
                }
                     
            }
        }

    }
}