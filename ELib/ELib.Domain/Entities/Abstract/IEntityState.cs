using ELib.Common;

namespace ELib.Domain.Entities.Abstract
{
    public interface IEntityState
    {
        LibEntityState State { get; set; }
    }
}
