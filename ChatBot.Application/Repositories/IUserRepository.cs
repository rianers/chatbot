using ChatBot.Domain.Entities;

namespace ChatBot.Application.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<string> GetUserId(string email, string password);
    }
}
