namespace ELib.Domain.Entities
{
    using Abstract;
    using ELib.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Comment")]
    public partial class Comment  : IEntityState
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comment()
        {
            RatingComments = new HashSet<RatingComment>();
        }

        public int Id { get; set; }

        [StringLength(400)]
        public string Text { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CommentDate { get; set; }

        public int SumLike { get; set; }

        public int SumDisLike { get; set; }

        public string UserName { get; set; }

        public string ImageHash { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RatingComment> RatingComments { get; set; }

        [NotMapped]
        public LibEntityState State { get; set; }
    }
}