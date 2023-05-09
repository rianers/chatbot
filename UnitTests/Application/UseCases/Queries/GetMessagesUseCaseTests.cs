using Bogus;
using ChatBot.Application.Repositories;
using ChatBot.Application.UseCases.Queries;
using Moq;
using Xunit;

namespace UnitTests.Application.UseCases.Queries
{
    public sealed class GetMessagesUseCaseTests
    {
        private readonly Mock<IMessageRepository> _messageRepository;
        public GetMessagesUseCaseTests()
        {
            _messageRepository = new Mock<IMessageRepository>();
        }

        [Fact]
        public async Task Get_Messages_Should_Return_Only_The_Last_50()
        {
            //Arrange
            string chatRoomId = "1";
            _messageRepository.Setup(m => m.GetAll(chatRoomId)).ReturnsAsync(DummyDataGenerator());
            var getMessagesUseCase = new GetMessagesUseCase(_messageRepository.Object);

            //Act
            IEnumerable<object> result = await getMessagesUseCase.Execute(chatRoomId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(50, result.Count());
        }

        private IEnumerable<object> DummyDataGenerator()
        {
            Faker faker = new Faker();
            List<object> dummyData = new();

            return Enumerable.Range(1, 51)
                             .Select(_ => Tuple.Create(faker.Lorem.Random, faker.Date.Random))
                             .ToList();
        }
    }
}
