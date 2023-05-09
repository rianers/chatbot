using ChatBot.Application.Repositories;
using ChatBot.Domain.Constants;
using ChatBot.Infra.MessageBroker.ModelReceiver;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Text;

namespace ChatBot.Infra.MessageBroker
{
    public class ReceiveStockListener : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IQueueClient _queueClient;

        public ReceiveStockListener(IServiceScopeFactory serviceScopeFactory, ReceiveStockQueueClient receiveStokeQueueClient)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _queueClient = receiveStokeQueueClient.Client;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _queueClient.RegisterMessageHandler(
                async (message, token) => await OnReceiveMessage(message),
                new MessageHandlerOptions(exceptionReceivedHandler =>
                {
                    return Task.CompletedTask;
                })
                {
                    AutoComplete = false,
                    MaxConcurrentCalls = 1
                });

            return Task.CompletedTask;
        }

        private async Task OnReceiveMessage(Message message)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                IMessageRepository messageRepository = scope.ServiceProvider.GetRequiredService<IMessageRepository>();
                string messageString = Encoding.UTF8.GetString(message.Body);
                StockReceiver stock = JsonConvert.DeserializeObject<StockReceiver>(messageString);
                await messageRepository.Add(new Domain.Entities.Message(BotConstants.STOCK_BOT, $"{stock.Symbol} quote is ${stock.Open} per share.", stock.ChatRoomId));
                await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
            }
        }
    }
}
