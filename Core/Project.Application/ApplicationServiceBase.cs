
using Project.Host.Base.Lazyloads;

namespace Project.Application
{
    public class ApplicationServiceBase
    {
        public ILazyloadProvider lazyloadProvider { get; }
        public ApplicationServiceBase(ILazyloadProvider lazyloadProvider)
        {
            this.lazyloadProvider = lazyloadProvider;
        }
    }
}
