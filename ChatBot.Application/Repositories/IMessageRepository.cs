using ChatBot.Domain.Entities;

namespace ChatBot.Application.Repositories
{
    public interface IMessageRepository
    {
        Task Add(Message message);
        Task<IEnumerable<object>> GetAll(string chatRoomId);
    }
}
