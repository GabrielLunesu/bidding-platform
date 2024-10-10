using System.ComponentModel.DataAnnotations;

namespace bidding_platform.Models.ViewModels
{
    public class CreateBidViewModel
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Bid amount must be greater than 0.")]
        public double Amount { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}