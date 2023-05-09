using ChatBot.Domain.Utils;

namespace ChatBot.Domain.Entities
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string email, string password)
        {
            Email = email;
            Password = EncryptionUtil.EncryptToSha256Hash(password);
        }

        public User(string email, string password, DateTime createdAt, DateTime? updatedAt) :
            this(email, password)
        {
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
