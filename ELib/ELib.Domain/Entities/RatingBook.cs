namespace ELib.Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RatingBook")]
    public partial class RatingBook
    {
        private int _rating;

        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public int ValueRating
        {
            get { return _rating; }
            set
            {
                if((value > 0) && (value <= 10))
                {
                    _rating = value;
                }
            }
        }

        public virtual Book Book { get; set; }

        public virtual Person Person { get; set; }
    }
}
