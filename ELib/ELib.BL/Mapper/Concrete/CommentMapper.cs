﻿using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Mapper.Concrete
{
    public class CommentMapper : IMapper<Comment, CommentDto>
    {
        public Comment Map(CommentDto input)
        {
            if (input == null)
                return null;
            return new Comment()
            {
                Id = input.Id,
                Text = input.Text,
                BookId = input.BookId,
                UserId = input.UserId,
                CommentDate = input.CommentDate,
                SumDisLike = input.SumDisLike,
                SumLike = input.SumLike,
                ImageHash = input.ImageHash,
                UserName = input.UserName,
                State = input.State

            };
        }

        public CommentDto Map(Comment input)
        {
            if (input == null)
                return null;
            return new CommentDto()
            {
                Id = input.Id,
                Text = input.Text,
                BookId = input.BookId,
                UserId = input.UserId,
                CommentDate = input.CommentDate,
                SumDisLike = input.SumDisLike,
                SumLike = input.SumLike,
                ImageHash = input.ImageHash,
                UserName = input.UserName,
                State = input.State
            };
        }
    }
}
