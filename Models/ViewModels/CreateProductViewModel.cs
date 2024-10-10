using System;
using System.ComponentModel.DataAnnotations;

namespace bidding_platform.Models.ViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Starting price must be greater than 0.")]
        public double StartingPrice { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Bid increment must be at least 1.")]
        [Display(Name = "Bid Increment")]
        public int BidIncrement { get; set; }

        [Required]
        [Display(Name = "Seller")]
        public int UserId { get; set; }
    }
}