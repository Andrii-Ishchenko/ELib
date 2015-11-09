using ELib.Common;
using ELib.BL.DtoEntities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ELib.BL.DtoEntities
{
    public class RatingCommentDto : IDtoEntityState
    {
        public int Id { get; set; }

        public int CommentId { get; set; }

        public int UserId { get; set; }

        public bool IsLike { get; set; }

        [Required]
        public LibEntityState State { get; set; }
    }
}