using ChatBot.Application.UseCases.Commands;
using ChatBot.Application.UseCases.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBot.Infra.IoC.Dependencies
{
    internal static class Application
    {
        internal static void RegisterUseCases(IServiceCollection services)
        {
            services.AddTransient<AddMessageUseCase, AddMessageUseCase>();
            services.AddTransient<AddUserUseCase, AddUserUseCase>();
            services.AddTransient<GetMessagesUseCase, GetMessagesUseCase>();
            services.AddTransient<GetUserUseCase, GetUserUseCase>();
        }
    }
}
