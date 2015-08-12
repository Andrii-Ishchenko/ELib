using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELib.Domain.Entities;
using ELib.BL.DtoEntities;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;

namespace ELib.BL.Services.Concrete
{
    public class PublisherService:  BaseService<Publisher,PublisherDto>, IPublisherService
    {
        public PublisherService(IUnitOfWorkFactory factory) 
            :base(factory) 
        {

        }
    }
}
