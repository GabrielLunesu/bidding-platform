namespace bidding_platform.Models
{
    public class User
    {
        public int? UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        // one user can send many messages
        public ICollection<Message>? Messages { get; set; }

        // one user can send many bids
        public ICollection<Bid>? Bids { get; set; }

        // one user can send many products
        public ICollection<Product>? Products { get; set; }

        // password and role i skip for now 
    }
}