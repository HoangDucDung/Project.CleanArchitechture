using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Contract.MessageBroker;
using Project.Controller.Base.Controller;
using Project.Host.Base.Lazyloads;
using System.Threading.Tasks;

namespace Project.Controller.Test.Controllers
{
    [AllowAnonymous]
    public class TestController(ILazyloadProvider lazyloadProvider) : BaseController(lazyloadProvider)
    {
        private IMessageProducer<string, string> _messageProducer => _lazyloadProvider.GetRequiredService<IMessageProducer<string, string>>();

        [HttpPost("KafkaTest")]
        public async Task<IActionResult> KafkaTest()
        {
            await _messageProducer.ProduceAsync("TestKey", "TestValue");
            return Ok("Kafka Test Successful");
        }
    }
}
