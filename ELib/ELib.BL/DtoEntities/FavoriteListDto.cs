using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.DtoEntities
{
    public class FavoriteListDto
    {
        public FavoriteListDto() { }

        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public String UserName { get; set; }

        public DateTime AdditionDate { get; set; }

        public BookDto Book { get; set; }

        
    }
}
