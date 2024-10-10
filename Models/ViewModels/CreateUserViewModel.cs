using System.ComponentModel.DataAnnotations;

namespace bidding_platform.Models.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Email { get; set; }

        // Add other properties as needed, such as password
    }
}