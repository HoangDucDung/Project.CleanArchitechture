
using Project.Host.Base.Lazyloads;

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
