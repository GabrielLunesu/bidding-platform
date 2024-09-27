namespace bidding_platform.Models
{
    public class Bid 
    {
        public int? BidId { get; set; }
        public double? Amount { get; set; }
        public DateTime? BidDate { get; set; }


        // navigation to user to know which user placed which bid
        public int? UserId { get; set; }
        public User? User { get; set; }

        // navigation to product this bid is for
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}