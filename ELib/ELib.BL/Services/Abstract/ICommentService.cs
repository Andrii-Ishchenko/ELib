using ELib.BL.DtoEntities;
using ELib.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Services.Abstract
{
    public interface ICommentService : IBaseService<Comment, CommentDto>
    {
        List<CommentDto> GetCommentsByBookId(int id);
    }
}