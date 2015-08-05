namespace ELib.Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AuthorGenre")]
    public partial class AuthorGenre
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public int GenreId { get; set; }

        public virtual Author Author { get; set; }

        public virtual Genre Genre { get; set; }
    }
}
