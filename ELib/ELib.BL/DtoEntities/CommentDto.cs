using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.DtoEntities
{
    public class CommentDto
    {
        [Required]
        public int Id { get; set; }

        [StringLength(400)]
        public string Text { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        //[Column(TypeName = "date")]
        public DateTime CommentDate { get; set; }
    }
}