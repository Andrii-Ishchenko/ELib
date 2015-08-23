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
            using (var uow = _factory.Create())
            {
                int sumRatingLike = 0;
                int sumRatingDisLike = 0;
                List<RatingComment> tempComment = (List<RatingComment>)uow.Repository<RatingComment>().Get(x => x.CommentId == rating.CommentId);
                if(tempComment.Count!=0)
                {
                    List<RatingComment> tempRatingComment = tempComment.FindAll(x => x.UserId == rating.UserId);
                    if(tempRatingComment.Count!=0)
                    {
                        foreach(var temp in tempRatingComment)
                        {
                            rating.Id = temp.Id;
                            var entityToUpdate = AutoMapper.Mapper.Map<RatingComment>(rating);
                            if(rating.IsLike == temp.IsLike)
                                base.Delete(rating);
                            else
                                base.Update(rating);
                        }
                        sumRatingLike = ReCalculateRatingPlus(tempComment, rating);
                        sumRatingDisLike = ReCalculateRatingMinus(tempComment, rating);
                    }
                    else
                    {
                        var entityToInsert = AutoMapper.Mapper.Map<RatingComment>(rating);
                        uow.Repository<RatingComment>().Insert(entityToInsert);
                        uow.Save();
                        sumRatingLike = ReCalculateRatingPlus(tempComment, rating);
                        sumRatingDisLike = ReCalculateRatingMinus(tempComment, rating);
                        if (rating.IsLike)
                            sumRatingLike++;
                        else
                            sumRatingDisLike++;
                    }
                }
                else
                {
                    var entityToInsert = AutoMapper.Mapper.Map<RatingComment>(rating);
                    uow.Repository<RatingComment>().Insert(entityToInsert);
                    uow.Save();
                    if (rating.IsLike)
                        sumRatingLike++;
                    else
                        sumRatingDisLike++;
                }
                uow.Repository<Comment>().GetById(rating.CommentId).SumLike = sumRatingLike;
                uow.Repository<Comment>().GetById(rating.CommentId).SumDisLike = sumRatingDisLike;
                uow.Save();
            }
        }

        private int ReCalculateRatingPlus(List<RatingComment> comments, RatingCommentDto rating)
        {
            int sum = 0;
            foreach (RatingComment comment in comments)
            {
                if (comment.UserId == rating.UserId && comment.IsLike && rating.IsLike)
                    continue;
                else if (comment.UserId == rating.UserId && comment.IsLike && !rating.IsLike)
                    continue;
                else if (comment.UserId == rating.UserId && !comment.IsLike && rating.IsLike)
                    sum++;
                else
                    sum += comment.IsLike ? 1 : 0;
            }
            return sum;
        }

        private int ReCalculateRatingMinus(List<RatingComment> comments, RatingCommentDto rating)
        {
            int sum = 0;
            foreach (RatingComment comment in comments)
            {
                if (comment.UserId == rating.UserId && !comment.IsLike && !rating.IsLike)
                    continue;
                else if (comment.UserId == rating.UserId && !comment.IsLike && rating.IsLike)
                    continue;
                else if (comment.UserId == rating.UserId && comment.IsLike && !rating.IsLike)
                    sum++;
                else
                    sum += !comment.IsLike ? 1 : 0;
            }
            return sum;
        }
    }
}