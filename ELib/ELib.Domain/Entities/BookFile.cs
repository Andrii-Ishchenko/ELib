namespace ELib.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BookFormat")]
    public partial class BookFile
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string FileHash { get; set; }

        [Column(TypeName = "date")]
        public DateTime InsertDate { get; set; }

        public virtual Book Book { get; set; }
    }
}
