namespace ELib.Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BookGenre")]
    public partial class BookGenre
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int GenreId { get; set; }

        public virtual Book Book { get; set; }

        public virtual Genre Genre { get; set; }
    }
}
