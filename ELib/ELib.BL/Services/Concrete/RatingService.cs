using ELib.BL.DtoEntities;
using ELib.BL.Mapper;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Domain.Entities;
using System.Collections.Generic;

namespace ELib.BL.Services.Concrete
{
    public class RatingService : BaseService<RatingBook, RatingBookDto>, IRatingService
    {
        public RatingService(IUnitOfWorkFactory factory, IMapper<RatingBook, RatingBookDto> mapper)
            : base(factory, mapper)
        {
            
        }

        
        public void AddRating(RatingBookDto rating)
        {
            if(rating.ValueRating <=0 || rating.ValueRating > 100)
            {
                return;
            }
            using (var uow = _factory.Create())
            {
                int sumRating = 0;
                List<RatingBook> tempBook = new List<RatingBook>();
                var colection = uow.Repository<RatingBook>().Get(x => x.BookId == rating.BookId);
                foreach (var book in colection)
                {
                    tempBook.Add(book);
                }
                if (tempBook.Count != 0)
                {
                    List<RatingBook> tempRatingBook = tempBook.FindAll(x => x.UserId == rating.UserId);
                    if (tempRatingBook.Count != 0)
                    {
                        foreach (var temp in tempRatingBook)
                        {
                            rating.Id = temp.Id;
                            var entityToUpdate = AutoMapper.Mapper.Map<RatingBook>(rating);
                            base.Update(rating);
                            sumRating = (ReCalculateRatingBook(tempBook) - temp.ValueRating + rating.ValueRating) / tempBook.Count;
                        }

                    }
                    else
                    {
                        var entityToInsert = AutoMapper.Mapper.Map<RatingBook>(rating);
                        uow.Repository<RatingBook>().Insert(entityToInsert);
                        uow.Save();
                        sumRating = (ReCalculateRatingBook(tempBook) + rating.ValueRating) / (tempBook.Count + 1);
                    }
                }
                else
                {
                    var entityToInsert = AutoMapper.Mapper.Map<RatingBook>(rating);
                    uow.Repository<RatingBook>().Insert(entityToInsert);
                    uow.Save();
                    sumRating = rating.ValueRating;
                }
                uow.Repository<Book>().GetById(rating.BookId).SumRatingValue = sumRating;
                uow.Save();
            }
        }

        private int ReCalculateRatingBook(List<RatingBook> books)
        {
            int sum = 0;
            foreach(RatingBook book in books)
            {
                sum += book.ValueRating;
            }
            return sum;
        }
    }
}