using ChatBot.Application.Repositories;
using ChatBot.Application.UseCases.Queries;
using ChatBot.Domain.Constants;
using ChatBot.Domain.Utils;
using Moq;
using Xunit;

namespace UnitTests.Application.UseCases.Queries
{
    public sealed class GetUserUseCaseTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        public GetUserUseCaseTests()
        {
            _userRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task Get_User_Should_Return_The_UserId()
        {
            //Arrange
            string email = "email.test@gmail.com";
            string password = "12345";
            string userId = "123";
            _userRepository.Setup(u => u.GetUserId(email, EncryptionUtil.EncryptToSha256Hash(password))).ReturnsAsync(userId);
            var getUserUseCase = new GetUserUseCase(_userRepository.Object);

            //Act
            var result = await getUserUseCase.Execute(email, password);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result);
        }

        [Fact]
        public async Task Get_User_Should_Return_Invalid_Credentials_Provided()
        {
            //Arrange
            string email = "email.test@gmail.com";
            string password = "12345";
            _userRepository.Setup(u => u.GetUserId(email, password)).Returns(value: null);
            var getUserUseCase = new GetUserUseCase(_userRepository.Object);

            //Act
            var result = await getUserUseCase.Execute(email, password);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(ResultConstants.INVALID_CREDENTIALS, result);
        }
    }
}
