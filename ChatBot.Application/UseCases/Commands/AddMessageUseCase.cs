using ChatBot.Application.Repositories;
using ChatBot.Domain.Entities;

namespace ChatBot.Application.UseCases.Commands
{
    public class AddMessageUseCase
    {
        private readonly IMessageRepository _messageRepository;

        public AddMessageUseCase(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<Message> Execute(string userId, string text, string chatRoomId)
        {
            Message message = new(userId, text, chatRoomId);
            await _messageRepository.Add(message);
            return message;
        }
    }
}
