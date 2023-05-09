using Microsoft.Azure.ServiceBus;

namespace ChatBot.Infra.MessageBroker
{
    public sealed class ReceiveStockQueueClient : QueueManager
    {
        public ReceiveStockQueueClient(IQueueClient client) : base(client) { }
    }
}
