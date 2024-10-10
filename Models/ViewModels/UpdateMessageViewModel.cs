using System.ComponentModel.DataAnnotations;

namespace bidding_platform.Models.ViewModels
{
    public class UpdateMessageViewModel
    {
        [Required]
        public int MessageId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Message content cannot be longer than 500 characters.")]
        public string Content { get; set; }
    }
}