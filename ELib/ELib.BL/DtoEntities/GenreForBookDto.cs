using ELib.Common;
using ELib.BL.DtoEntities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace ELib.BL.DtoEntities
{
    public class GenreForBookDto : IDtoEntityState
    {
        public string GenreName { get; set; }

        public int GenreId { get; set; }

        public int BookGenreId { get; set; }

        [Required]
        public LibEntityState State { get; set; }
    }
}