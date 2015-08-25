using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ELib.BL.DtoEntities
{
    public class RatingBookDto
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public int ValueRating { get; set; }

        //public ICollection<int> BookIds { get; set; }

        //public ICollection<int> UserIds { get; set; }
    }
}