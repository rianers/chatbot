using AutoMapper;
using ChatBot.Application.Repositories;
using ChatBot.Domain.Entities;
using ChatBot.Infra.DataProvider;
using ChatBot.Infra.DataProvider.Repositories;
using ChatBot.Infra.MessageBroker;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ChatBot.Infra.IoC.Dependencies
{
    internal static class Infra
    {
        internal static void RegisterDatabase(IServiceCollection services, string connectionString, string databaseName)
        {
            IMongoClient mongoClient = new MongoClient(connectionString);
            Context context = new Context(mongoClient, databaseName);
            services.AddSingleton(context);
        }

        internal static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
        }

        internal static void RegisterReceiveStockListener(IServiceCollection services, string connection, string queueName)
        {
            IQueueClient client = new QueueClient(connection, queueName, ReceiveMode.PeekLock);
            services.AddSingleton(x => new ReceiveStockQueueClient(client));
            services.AddHostedService<ReceiveStockListener>();
        }

        internal static void RegisterAutoMapper(IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DataProvider.Entities.User, User>();
                cfg.CreateMap<User, DataProvider.Entities.User>();

                cfg.CreateMap<DataProvider.Entities.Message, Domain.Entities.Message>();
                cfg.CreateMap<Domain.Entities.Message, DataProvider.Entities.Message>();

                cfg.CreateMap<IEnumerable<DataProvider.Entities.Message>, IEnumerable<Domain.Entities.Message>>();
                cfg.CreateMap<IEnumerable<Domain.Entities.Message>, IEnumerable<DataProvider.Entities.Message>>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
