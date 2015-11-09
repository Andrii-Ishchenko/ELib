namespace ELib.Domain.Entities
{
    using Abstract;
    using ELib.Common;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BookAuthor")]
    public partial class BookAuthor : IEntityState
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public virtual Book Book { get; set; }

        [NotMapped]
        public LibEntityState State { get; set; }
    }
}
