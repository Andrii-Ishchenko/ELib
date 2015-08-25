namespace ELib.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BookInstance")]
    public partial class BookInstance
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string FileHash { get; set; }

        [StringLength(400)]
        public string FileName { get; set; }

        [Column(TypeName = "date")]
        public DateTime InsertDate { get; set; }

        public virtual Book Book { get; set; }
    }
}
