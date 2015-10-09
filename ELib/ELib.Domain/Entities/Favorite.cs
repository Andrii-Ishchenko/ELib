
namespace ELib.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Favorite")]
    public partial class Favorite
    {
        public Favorite() { }

        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime AdditionDate { get; set; }

        public virtual Book Book {get; set;}

        public virtual Person User { get; set; }
    }
}
