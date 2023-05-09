using ChatBot.Application.Repositories;
using ChatBot.Domain.Entities;

namespace ChatBot.Application.UseCases.Commands
{
    public class AddUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public AddUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Execute(string email, string password)
        {
            User user = new(email, password);
            await _userRepository.Add(user);
            return user.Id;
        }
    }
}
