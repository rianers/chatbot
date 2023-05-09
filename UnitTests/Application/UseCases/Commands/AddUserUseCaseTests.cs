using ChatBot.Application.Repositories;
using ChatBot.Application.UseCases.Commands;
using Moq;
using Xunit;

namespace UnitTests.Application.UseCases.Commands
{
    public sealed class AddUserUseCaseTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        public AddUserUseCaseTests()
        {
            _userRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task Add_User_Should_Return_The_UserId_Which_Has_Been_Added()
        {
            //Arrange
            string email = "email.test@gmail.com";
            string password = "12345";
            var addMessageUseCase = new AddUserUseCase(_userRepository.Object);

            //Act
            string userId = await addMessageUseCase.Execute(email, password);

            //Assert
            Assert.NotNull(userId);
        }
    }
}
