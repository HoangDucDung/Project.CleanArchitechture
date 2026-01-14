namespace Project.Application.Contract.MessageBroker
{
    public interface IMessageProducer<TKey, TValue> : IDisposable
    {
        /// <summary>
        /// Đẩy message lên message broker
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task ProduceAsync(TKey key, TValue value);
    }
}
