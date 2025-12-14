namespace Project.Libs.Retrys
{
    public interface IRetryWorkHelper
    {
        Task RetryAsync(Func<Task> work, CancellationToken cancellationToken = default);
        Task<T> RetryAsync<T>(Func<Task<T>> work, CancellationToken cancellationToken = default);
    }
}
