using ELib.Common;
using ELib.BL.DtoEntities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace ELib.BL.DtoEntities
{
    public class CommentDto : IDtoEntityState
    {
        public int Id { get; set; }

        [StringLength(400)]
        public string Text { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        //[Column(TypeName = "date")]
        public DateTime CommentDate { get; set; }

        public int SumLike { get; set; }

        public int SumDisLike { get; set; }

        public string UserName { get; set; }

        public string ImageHash { get; set; }

        [Required]
        public LibEntityState State { get; set; }
    }
}