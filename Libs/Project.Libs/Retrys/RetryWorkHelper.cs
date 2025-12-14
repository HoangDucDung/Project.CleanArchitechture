namespace Project.Libs.Retrys
{
    /// <summary>
    /// Xử lý công việc với khả năng thử lại khi gặp lỗi tạm thời.
    /// </summary>
    public class RetryWorkHelper : IRetryWorkHelper
    {
        private readonly int _maxRetries;
        private readonly int _delayMilliseconds;
        private readonly Func<Exception, bool>? _retryIf;

        public RetryWorkHelper(int maxRetries, int delayMilliseconds)
        {
            _maxRetries = maxRetries;
            _delayMilliseconds = delayMilliseconds;
            _retryIf = null; // Retry on all exceptions (sau cần thì thêm)
        }

        public async Task RetryAsync(Func<Task> work, CancellationToken cancellationToken = default)
        {
            await RetryAsync<object>(async () =>
            {
                await work();
                return null!;
            }, cancellationToken);
        }

        public async Task<T> RetryAsync<T>(Func<Task<T>> work, CancellationToken cancellationToken = default)
        {
            for (int attempt = 1; attempt <= _maxRetries; attempt++)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    return await work();
                }
                catch (Exception ex) when (attempt < _maxRetries &&
                                           (_retryIf == null || _retryIf(ex)))
                {
                    Console.WriteLine(
                        $"[Retry] Attempt {attempt}/{_maxRetries} failed: {ex.Message}. " +
                        $"Waiting {_delayMilliseconds}ms before retry...");

                    await Task.Delay(_delayMilliseconds, cancellationToken);
                }
            }

            return await work(); // Throw original exception after retries
        }
    }
}
