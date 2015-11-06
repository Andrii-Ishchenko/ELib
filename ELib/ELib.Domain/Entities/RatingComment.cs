namespace ELib.Domain.Entities
{
    using Abstract;
    using ELib.Common;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RatingComment")]
    public partial class RatingComment : IEntityState
    {
        public int Id { get; set; }

        public int CommentId { get; set; }

        public int UserId { get; set; }

        public bool IsLike { get; set; }

        public virtual Comment Comment { get; set; }

        public virtual Person Person { get; set; }

        [NotMapped]
        public LibEntityState State { get; set; }
    }
}