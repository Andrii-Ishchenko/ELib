using System.Collections.Generic;

namespace ELib.BL.DtoEntities
{
    public class AuthorsInBookDto
    {
        public int BookId { get; set; }
        public List<int> AuthorsId = new List<int>();
    }
}
