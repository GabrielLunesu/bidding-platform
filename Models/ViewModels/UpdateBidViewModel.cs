using System.ComponentModel.DataAnnotations;

namespace bidding_platform.Models.ViewModels
{
    public class UpdateBidViewModel
    {
        [Required]
        public int BidId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Bid amount must be greater than 0.")]
        public double Amount { get; set; }
    }
}