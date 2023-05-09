using Microsoft.Azure.ServiceBus;

namespace ChatBot.Infra.MessageBroker
{
    public abstract class QueueManager
    {
        public IQueueClient Client { get; private set; }

        protected QueueManager(IQueueClient client)
        {
            Client = client;
        }
    }
}
