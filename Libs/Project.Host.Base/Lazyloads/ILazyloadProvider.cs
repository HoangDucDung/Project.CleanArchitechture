namespace Project.Host.Base.Lazyloads
{
    public interface ILazyloadProvider : IDisposable
    {
        T GetRequiredService<T>();

        T? GetService<T>();
    }
}