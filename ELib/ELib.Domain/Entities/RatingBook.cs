namespace ELib.Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RatingBook")]
    public partial class RatingBook
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public int ValueRating { get; set; }

        public virtual Book Book { get; set; }

        public virtual Person Person { get; set; }
    }
}
