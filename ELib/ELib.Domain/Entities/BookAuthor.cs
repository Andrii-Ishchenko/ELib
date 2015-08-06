namespace ELib.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BookAuthor")]
    public partial class BookAuthor
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public virtual Book Book { get; set; }
    }
}
