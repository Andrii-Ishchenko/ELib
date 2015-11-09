using ELib.Common;
using ELib.BL.DtoEntities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ELib.BL.DtoEntities
{
    public class RatingBookDto : IDtoEntityState
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public int ValueRating { get; set; }

        [Required]
        public LibEntityState State { get; set; }

        //public ICollection<int> BookIds { get; set; }

        //public ICollection<int> UserIds { get; set; }
    }
}