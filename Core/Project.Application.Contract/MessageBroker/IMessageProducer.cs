namespace Project.Application.Contract.MessageBroker
{
    public interface IMessageProducer<TKey, TValue> : IDisposable
    {
        //Đẩy message lên message broker
        Task ProduceAsync(TKey key, TValue value);
    }
}
