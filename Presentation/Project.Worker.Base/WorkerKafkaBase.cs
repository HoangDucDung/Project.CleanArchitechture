using Project.Application.Contract.MessageBroker;
using Project.Extensions.Extensions;
using Project.Host.Base.Lazyloads;

namespace Project.Worker.Base
{
    public abstract class WorkerKafkaBase<TService, TValue> : BackgroundService
    {
        protected readonly ILogger<TService> _logger;
        protected readonly ILazyloadProvider _lazyloadProvider;

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly int _workerCount = 0; // Cho phép cấu hình số lượng worker

        /// <summary>
        /// Cấu hình số lượng consumer
        /// </summary>
        /// <returns></returns>
        protected abstract int WorkerCount();

        protected WorkerKafkaBase(ILazyloadProvider lazyloadProvider)
        {
            _lazyloadProvider = lazyloadProvider;
            _scopeFactory = _lazyloadProvider.GetRequiredService<IServiceScopeFactory>();
            _logger = _lazyloadProvider.GetRequiredService<ILogger<TService>>();
            _workerCount = WorkerCount();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("WorkerKafkaBase is starting with {count} workers.", _workerCount);

            // Tạo danh sách các Task để quản lý vòng đời chặt chẽ
            var tasks = new List<Task>();

            for (int i = 0; i < _workerCount; i++)
            {
                Guid workerId = GenerateExtentions.NewGuid(); // Fix closure issue
                tasks.Add(Task.Run(() => RunConsumerLoop(workerId, stoppingToken), stoppingToken));
            }

            // Chờ tất cả các task hoàn thành (hoặc khi cancellationToken được kích hoạt)
            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Xử lý khởi tạo consumer và lắng nghe message và execute action
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        private async Task RunConsumerLoop(Guid workerId, CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker instance {id} started.", workerId);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Mỗi worker nên có một Scope riêng biệt để tránh xung đột DbContext/UnitOfWork
                    using var scope = _scopeFactory.CreateScope();
                    var consumer = scope.ServiceProvider.GetRequiredService<IMessageConsumer<TValue>>();

                    _logger.LogDebug("Worker {id} is consuming...", workerId);

                    // Giả sử ConsumeAsync sẽ block cho đến khi có message hoặc token cancel
                    await consumer.ConsumeAsync(DoWork, workerId, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Worker {id} stopping gracefully.", workerId);
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in worker {id}. Retrying in 5 seconds...", workerId);

                    // Quan trọng: Tránh loop vô tận khi lỗi nặng (Kafka down, Network issues)
                    try { await Task.Delay(5000, stoppingToken); }
                    catch (OperationCanceledException) { break; }
                }
            }
        }

        /// <summary>
        /// Hàm thực hiện action khi nhận được message từ kafka
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected abstract Task DoWork(TValue message, CancellationToken cancellationToken);
    }
}
