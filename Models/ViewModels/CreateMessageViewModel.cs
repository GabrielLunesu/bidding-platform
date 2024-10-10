using System.ComponentModel.DataAnnotations;

namespace bidding_platform.Models.ViewModels
{
    public class CreateMessageViewModel
    {
        [Required]
        [StringLength(500, ErrorMessage = "Message content cannot be longer than 500 characters.")]
        public string Content { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public int RecipientId { get; set; }
    }
}