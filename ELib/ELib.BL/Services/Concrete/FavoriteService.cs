using ELib.BL.DtoEntities;
using ELib.BL.Mapper.Abstract;
using ELib.BL.Services.Abstract;
using ELib.DAL.Infrastructure.Abstract;
using ELib.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Services.Concrete
{
    public class FavoriteService : BaseService<Favorite, FavoriteDto>, IFavoriteService
    {
        public FavoriteService(IUnitOfWorkFactory factory, IMapper<Favorite, FavoriteDto> mapper)
            : base(factory, mapper)
        { }

        public bool IsInFavorite(int bookId, int userId)
        {
            return false;
        }

        public void ToggleFavorite(int bookId, int userId)
        {
           
        }
    }
}
