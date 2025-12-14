namespace Project.Host.Base.Lazyloads
{
    public interface ILazyloadProvider : IDisposable
    {
        T LazyGetRequiredService<T>();

        T? LazyGetService<T>();
    }
}