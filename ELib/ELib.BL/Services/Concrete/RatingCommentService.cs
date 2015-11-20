using ELib.BL.DtoEntities;
using ELib.BL.Mapper.Abstract;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Domain.Entities;
using ELib.Common;
using System.Collections.Generic;
using System.Linq;

namespace ELib.BL.Services.Concrete
{
    public class RatingCommentService : BaseService<RatingComment, RatingCommentDto>, IRatingCommentService
    {
        public RatingCommentService(IUnitOfWorkFactory factory, IMapper<RatingComment, RatingCommentDto> mapper)
            : base(factory, mapper)
        {

        }
        public void AddLike(RatingCommentDto rating)
        {
            using (var uow = _factory.Create())
            {
                Comment comment = uow.Repository<Comment>().GetById(rating.CommentId);

                var ratingComment = uow.Repository<RatingComment>().Get(x => x.CommentId == rating.CommentId && x.UserId == rating.UserId).SingleOrDefault();

                if (ratingComment != null)
                {
                    rating.Id = ratingComment.Id;
                    var entityToUpdate = _mapper.Map(rating);
                    if (rating.IsLike == ratingComment.IsLike)
                    {
                        rating.State = LibEntityState.Deleted;
                        base.Delete(rating);
                        comment.SumLike += rating.IsLike ? -1 : 0;
                        comment.SumDisLike += rating.IsLike ? 0 : -1;
                    }
                    else
                    {
                        rating.State = LibEntityState.Modified;
                        base.Update(rating);
                        comment.SumLike += rating.IsLike ? 1 : -1;
                        comment.SumDisLike += rating.IsLike ? -1 : 1;
                    }
                }
                else
                {
                    var entityToInsert = _mapper.Map(rating);
                    uow.Repository<RatingComment>().Insert(entityToInsert);
                    uow.Save();

                    if (rating.IsLike)
                        comment.SumLike++;
                    else
                        comment.SumDisLike++;
                }
                uow.Save();
            }
        }

        //public void AddLike(RatingCommentDto rating)
        //{
        //    using (var uow = _factory.Create())
        //    {
        //        int sumRatingLike = 0;
        //        int sumRatingDisLike = 0;
        //        List<RatingComment> tempComment = (List<RatingComment>)uow.Repository<RatingComment>().Get(x => x.CommentId == rating.CommentId);
        //        if(tempComment.Count!=0)
        //        {
        //            List<RatingComment> tempRatingComment = tempComment.FindAll(x => x.UserId == rating.UserId);
        //            if(tempRatingComment.Count!=0)
        //            {
        //                foreach(var temp in tempRatingComment)
        //                {
        //                    rating.Id = temp.Id;
        //                    var entityToUpdate = _mapper.Map(rating);
        //                    if(rating.IsLike == temp.IsLike)
        //                        base.Delete(rating);
        //                    else
        //                        base.Update(rating);
        //                }
        //                sumRatingLike = ReCalculateRatingPlus(tempComment, rating);
        //                sumRatingDisLike = ReCalculateRatingMinus(tempComment, rating);
        //            }
        //            else
        //            {
        //                var entityToInsert = _mapper.Map(rating);
        //                uow.Repository<RatingComment>().Insert(entityToInsert);
        //                uow.Save();
        //                sumRatingLike = ReCalculateRatingPlus(tempComment, rating);
        //                sumRatingDisLike = ReCalculateRatingMinus(tempComment, rating);
        //                if (rating.IsLike)
        //                    sumRatingLike++;
        //                else
        //                    sumRatingDisLike++;
        //            }
        //        }
        //        else
        //        {
        //            var entityToInsert = _mapper.Map(rating);
        //            uow.Repository<RatingComment>().Insert(entityToInsert);
        //            uow.Save();
        //            if (rating.IsLike)
        //                sumRatingLike++;
        //            else
        //                sumRatingDisLike++;
        //        }
        //        uow.Repository<Comment>().GetById(rating.CommentId).SumLike = sumRatingLike;
        //        uow.Repository<Comment>().GetById(rating.CommentId).SumDisLike = sumRatingDisLike;
        //        uow.Save();
        //    }
        //}

        //private int ReCalculateRatingPlus(List<RatingComment> comments, RatingCommentDto rating)
        //{
        //    int sum = 0;
        //    foreach (RatingComment comment in comments)
        //    {
        //        if (comment.UserId == rating.UserId && comment.IsLike && rating.IsLike)
        //            continue;
        //        else if (comment.UserId == rating.UserId && comment.IsLike && !rating.IsLike)
        //            continue;
        //        else if (comment.UserId == rating.UserId && !comment.IsLike && rating.IsLike)
        //            sum++;
        //        else
        //            sum += comment.IsLike ? 1 : 0;
        //    }
        //    return sum;
        //}

        //private int ReCalculateRatingMinus(List<RatingComment> comments, RatingCommentDto rating)
        //{
        //    int sum = 0;
        //    foreach (RatingComment comment in comments)
        //    {
        //        if (comment.UserId == rating.UserId && !comment.IsLike && !rating.IsLike)
        //            continue;
        //        else if (comment.UserId == rating.UserId && !comment.IsLike && rating.IsLike)
        //            continue;
        //        else if (comment.UserId == rating.UserId && comment.IsLike && !rating.IsLike)
        //            sum++;
        //        else
        //            sum += !comment.IsLike ? 1 : 0;
        //    }
        //    return sum;
        //}
    }
}