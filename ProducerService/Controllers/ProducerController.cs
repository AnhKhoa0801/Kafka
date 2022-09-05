using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ProducerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProducerController : ControllerBase
    {

        private readonly ILogger<ProducerController> _logger;

        private ProducerConfig _producerConfig;

        public ProducerController(ProducerConfig producerConfig)
        {
            _producerConfig = producerConfig;
        }

        [HttpPost("send")]
        public async Task<ActionResult> Get(string topic, [FromBody]Employee employee)
        {
            var serializedObj = JsonConvert.SerializeObject(employee);
            using( var producer = new ProducerBuilder<Null,string>(_producerConfig).Build())
            {
                await producer.ProduceAsync(topic, new Message<Null, string> { Value = serializedObj });
                producer.Flush(TimeSpan.FromSeconds(10));
                return Ok(true);
            }
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}