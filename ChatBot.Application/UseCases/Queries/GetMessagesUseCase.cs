using ChatBot.Application.Repositories;

namespace ChatBot.Application.UseCases.Queries
{
    public class GetMessagesUseCase
    {
        private readonly IMessageRepository _messageRepository;

        public GetMessagesUseCase(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        /// <summary>
        /// Execute use case to return all messages
        /// </summary>
        /// <param name="chatroomId"> Respective chat room id which the user wants to access </param>
        /// <returns> As per challenge rules, only the last 50 messages must be returned </returns>
        public async Task<IEnumerable<object>> Execute(string chatroomId)
        {
            IEnumerable<object> messages = await _messageRepository.GetAll(chatroomId);
            return messages.Take(50);
        }
    }
}
