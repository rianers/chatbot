using AutoMapper;
using ChatBot.Application.Repositories;
using ChatBot.Domain.Entities;
using MongoDB.Driver;

namespace ChatBot.Infra.DataProvider.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper mapper;
        private readonly IMongoCollection<Entities.User> UserCollection;

        public UserRepository(Context context, IMapper mapper)
        {
            UserCollection = context.UserCollection;
            this.mapper = mapper;
        }

        public async Task Add(User user)
        {
            Entities.User userMapper = MapperData<Entities.User>(user);

            await UserCollection.InsertOneAsync(userMapper);
        }

        public async Task<string> GetUserId(string email, string password)
        {
            var userId = UserCollection.
                            AsQueryable().
                            Where(x => x.Email == email && x.Password == password).
                            Select(x => x.Id).
                            Single();
            return userId;
        }

        protected T MapperData<T>(object data)
        {
            return mapper.Map<T>(data);
        }
    }
}
