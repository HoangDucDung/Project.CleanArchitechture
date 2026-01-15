
using Project.Libs.DependencyInjection;

namespace Project.Domain.Services
{
    public class ManagerServiceBase
    {
        public ILazyloadProvider lazyloadProvider { get; }
        public ManagerServiceBase(ILazyloadProvider lazyloadProvider)
        {
            this.lazyloadProvider = lazyloadProvider;
        }
    }
}
