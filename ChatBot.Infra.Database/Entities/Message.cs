namespace ChatBot.Infra.DataProvider.Entities
{
    public class Message
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UserId { get; set; }
        public string ChatRoomId { get; set; }
        public string MessageText { get; set; }
    }
}
