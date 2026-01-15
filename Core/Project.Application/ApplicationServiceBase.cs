

using Project.Libs.DependencyInjection;

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
