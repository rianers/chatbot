namespace ChatBot.Domain.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid().ToString().ToLowerInvariant();
            CreatedAt = DateTime.UtcNow;
        }

        public string Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }

        public virtual void Updated() => UpdatedAt = DateTime.UtcNow;
    }
}
