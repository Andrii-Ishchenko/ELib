namespace ELib.Domain.Entities
{
    using Abstract;
    using ELib.Common;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("BookInstance")]
    public partial class BookInstance : IEntityState
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string FileHash { get; set; }

        [StringLength(400)]
        public string FileName { get; set; }

        public int DownloadCount { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime InsertDate { get; set; }

        public virtual Book Book { get; set; }

        [NotMapped]
        public LibEntityState State { get; set; }
    }
}
