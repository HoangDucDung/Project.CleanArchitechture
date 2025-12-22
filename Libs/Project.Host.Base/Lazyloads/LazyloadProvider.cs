using System.Collections.Concurrent;

namespace Project.Host.Base.Lazyloads
{
    public class LazyloadProvider : ILazyloadProvider
    {
        IServiceProvider _serviceProvider;

        private readonly ConcurrentDictionary<Type, Lazy<object?>> _lazyServices = new();

        public LazyloadProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T GetRequiredService<T>()
        {
            var service = _lazyServices.GetOrAdd(typeof(T), type => new Lazy<object?>(() => _serviceProvider.GetRequiredService(type)));
            return (T)service.Value!;
        }

        public T? GetService<T>()
        {
            var service = _lazyServices.GetOrAdd(typeof(T), type => new Lazy<object?>(() => _serviceProvider.GetService(type)));
            return (T)service.Value!;
        }

        #region dispose
        private readonly object _disposeLock = new();
        private bool _disposed;

        public void Dispose()
        {
            lock (_disposeLock)
            {
                if (_disposed) return;

                foreach (var lazyService in _lazyServices)
                {
                    if (lazyService.Value.IsValueCreated && lazyService.Value.Value is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }

                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}