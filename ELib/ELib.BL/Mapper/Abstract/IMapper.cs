namespace ELib.BL.Mapper.Abstract
{
    public interface IMapper<T1, T2>
    {
        T2 Map(T1 input);
        T1 Map(T2 input);
    }
}
