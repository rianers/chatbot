using Microsoft.Extensions.DependencyInjection;

namespace ChatBot.Infra.IoC
{
    public static class Bootstrapper
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            Dependencies.Application.RegisterUseCases(services);
            Dependencies.Infra.RegisterRepositories(services);
            Dependencies.Infra.RegisterAutoMapper(services);
        }

        public static void RegisterDatabase(this IServiceCollection services, string connectionString, string databaseName)
        {
            Dependencies.Infra.RegisterDatabase(services, connectionString, databaseName);
        }

        public static void RegisterReceiveStockListener(this IServiceCollection services, string connection, string queueName)
        {
            Dependencies.Infra.RegisterReceiveStockListener(services, connection, queueName);
        }
    }
}
