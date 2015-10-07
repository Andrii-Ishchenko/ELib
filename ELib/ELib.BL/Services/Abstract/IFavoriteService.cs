using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELib.BL.Services.Abstract
{
    public interface IFavoriteService
    {
        bool IsInFavorite(int bookId, int userId);

        void ToggleFavorite(int bookId, int userId);

    }
}
