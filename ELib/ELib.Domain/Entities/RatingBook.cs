namespace ELib.Domain.Entities
{
    using Abstract;
    using ELib.Common;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RatingBook")]
    public partial class RatingBook : IEntityState
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
                if((value > 0) && (value <= 100))
                {
                    _rating = value;
                }
            }
        }

        public virtual Book Book { get; set; }

        public virtual Person Person { get; set; }

        [NotMapped]
        public LibEntityState State { get; set; }
    }
}
