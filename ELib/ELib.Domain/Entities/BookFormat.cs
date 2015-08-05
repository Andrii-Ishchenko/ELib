namespace ELib.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BookFormat")]
    public partial class BookFormat
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int FormatId { get; set; }

        public string FilePath { get; set; }

        [Column(TypeName = "date")]
        public DateTime InsertDate { get; set; }

        public virtual Book Book { get; set; }

        public virtual FileFormat FileFormat { get; set; }
    }
}
