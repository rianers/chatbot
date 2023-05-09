using System.ComponentModel.DataAnnotations;

namespace ChatBot.API.Inputs
{
    public class MessageInput
    {
        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Text is required.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "ChatRoomId is required.")]
        public string ChatRoomId { get; set; }
    }
}
