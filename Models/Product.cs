namespace bidding_platform.Models
{
    public class Product
    {
        public int? ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? StartingPrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int? BidIncrement { get; set; }


        public ICollection<Bid>? Bids { get; set; }


        // optional navigation to user
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}