namespace Project.Application.Contract.MessageBroker
{
    public interface IMessageConsumer<TValue> : IDisposable
    {
        /// <summary>
        /// Nhận và xử lý message bất đồng bộ
        /// </summary>
        /// <param name="messageHandler"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task ConsumeAsync(Func<TValue, CancellationToken, Task> messageHandler, CancellationToken cancellationToken);
    }
}
