using System.Collections.Generic;
using ELib.BL.DtoEntities;
using ELib.Domain.Entities;

namespace ELib.BL.Services.Abstract
{
    public interface IAuthorService : IBaseService<Author, AuthorDto>
    {
        IEnumerable<AuthorDto> GetAll(string query, string authorName, string orderby, string orderDirection, int year, int pageNumb, int pageCount);
    }
}
