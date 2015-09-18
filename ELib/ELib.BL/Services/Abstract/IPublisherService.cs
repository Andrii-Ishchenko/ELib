using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELib.BL.DtoEntities;
using ELib.Domain.Entities;

namespace ELib.BL.Services.Abstract
{
    public interface IPublisherService : IBaseService<Publisher, PublisherDto>
    {
        IEnumerable<PublisherDto> GetAll(string query, int pageCount, int pageNumb);
    }
}
