namespace ELib.Domain.Entities

{
    using Abstract;
    using ELib.Common;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserBookStatus")]
    public partial class UserBookStatus : IEntityState
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public bool? StatusBook { get; set; }

        public virtual Book Book { get; set; }

        public virtual Person Person { get; set; }

        [NotMapped]
        public LibEntityState State { get; set; }
    }
}
