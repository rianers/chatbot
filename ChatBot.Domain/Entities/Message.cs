namespace ChatBot.Domain.Entities
{
    public class Message : Entity
    {
        public string UserId { get; set; }
        public string ChatRoomId { get; set; }
        public string MessageText { get; set; }
        public Message() { }
        public Message(string userId, string messageText, string chatRoomId)
        {
            UserId = userId;
            MessageText = messageText;
            ChatRoomId = chatRoomId;
        }

        public Message(string userId, string messageText, string chatRoomId, DateTime createdAt, DateTime? updatedAt)
            : this(userId, messageText, chatRoomId)
        {
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
