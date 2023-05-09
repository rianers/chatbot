using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Polly;
using System.Text;

namespace StockBot.MessageBroker
{
    public interface ISendStockQueue
    {
        Task SendMessage<T>(T model);
    }

    public class SendStockQueue : ISendStockQueue
    {
        private readonly IConfiguration _configuration;
        private string serviceBusConnString;
        private string queueName;
        public SendStockQueue(IConfiguration configuration)
        {
            _configuration = configuration;
            serviceBusConnString = _configuration.GetSection("QueueStrings")["QueueEndpoint"];
            queueName = _configuration.GetSection("QueueStrings")["QueueStockName"];
        }

        public Task SendMessage<T>(T model)
        {
            return Policy
                .Handle<ServiceBusException>()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    retryAttempt => TimeSpan.FromSeconds(2)
                )
                .AsAsyncPolicy<Task>()
                .ExecuteAsync(async () =>
                {
                    string jsonModel = JsonConvert.SerializeObject(model);
                    byte[] messageBytes = Encoding.UTF8.GetBytes(jsonModel);
                    Message message = new Message(messageBytes);
                    QueueClient queueClient = new QueueClient(serviceBusConnString, queueName, ReceiveMode.PeekLock);
                    await queueClient.SendAsync(message);

                    return Task.CompletedTask;
                });
        }
    }
}
