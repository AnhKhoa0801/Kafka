using Confluent.Kafka;

namespace ProducerService.HostedServices
{
    public class ConsumerService : BackgroundService
    {
        private ConsumerConfig _consumerConfig;
        public ConsumerService(ConsumerConfig consumerConfig)
        {
            _consumerConfig = consumerConfig;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build())
            {
                consumer.Subscribe("KhoaTopic");
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(1000), stoppingToken);
                    var consumeResult = consumer.Consume(stoppingToken);
                    Console.WriteLine(consumeResult.Message.Value);
                }
                consumer.Close();
            }
        }
    }
}