using AutoMapper;
using ChatBot.Application.Repositories;
using ChatBot.Domain.Entities;
using MongoDB.Driver;

namespace ChatBot.Infra.DataProvider.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMapper mapper;
        private readonly IMongoCollection<Entities.Message> MessageCollection;

        public MessageRepository(Context context, IMapper mapper)
        {
            MessageCollection = context.MessageCollection;
            this.mapper = mapper;
        }

        public async Task Add(Message message)
        {
            Entities.Message messageMapper = MapperData<Entities.Message>(message);

            await MessageCollection.InsertOneAsync(messageMapper);
        }

        public async Task<IEnumerable<object>> GetAll(string chatRoomId)
        {
            var messages = MessageCollection
                            .AsQueryable()
                            .Where(x => x.ChatRoomId == chatRoomId)
                            .Select(x => new { x.MessageText, x.CreatedAt })
                            .OrderBy(x => x.CreatedAt)
                            .ToList();
            return messages;
        }

        protected T MapperData<T>(object data)
        {
            return mapper.Map<T>(data);
        }
    }
}
