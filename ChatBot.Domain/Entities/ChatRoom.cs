namespace ChatBot.Domain.Entities
{
    public class ChatRoom : Entity
    {
        public string Name { get; set; }

        public ChatRoom(string name)
        {
            Name = name;
        }

        public ChatRoom(string name, DateTime createdAt, DateTime? updatedAt) :
            this(name)
        {
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
