using ChatBot.Application.Repositories;
using ChatBot.Domain.Constants;
using ChatBot.Domain.Utils;

namespace ChatBot.Application.UseCases.Queries
{
    public class GetUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Execute(string email, string password)
        {
            var userId = await _userRepository.GetUserId(email, EncryptionUtil.EncryptToSha256Hash(password));
            if (userId is not null)
                return userId;
            else
                return ResultConstants.INVALID_CREDENTIALS;
        }
    }
}
