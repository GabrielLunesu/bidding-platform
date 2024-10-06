namespace bidding_platform.Models
{
    public class Message
    {
        public int? MessageId { get; set; }
        public string? Content { get; set; }
        public DateTime? SentDate { get; set; }

        public int? SenderId { get; set; }
        public User? Sender { get; set; }

        public int? RecipientId { get; set; }
        public User? Recipient { get; set; }
    }
}