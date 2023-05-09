using ChatBot.Application.Repositories;
using ChatBot.Application.UseCases.Commands;
using ChatBot.Domain.Entities;
using Moq;
using Xunit;

namespace UnitTests.Application.UseCases.Commands
{
    public sealed class AddMessageUseCaseTests
    {
        private readonly Mock<IMessageRepository> _messageRepository;
        public AddMessageUseCaseTests()
        {
            _messageRepository = new Mock<IMessageRepository>();
        }
        [Fact]
        public async Task Add_Message_Should_Return_The_New_Message()
        {
            //Arrange
            string userId = "12345";
            string messageText = "This is just a test";
            string chatRoomId = "1";
            var addMessageUseCase = new AddMessageUseCase(_messageRepository.Object);

            //Act
            Message message = await addMessageUseCase.Execute(userId, messageText, chatRoomId);

            //Assert
            Assert.NotNull(message);
            Assert.Equal(userId, message.UserId);
            Assert.Equal(messageText, message.MessageText);
        }
    }
}
