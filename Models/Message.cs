namespace bidding_platform.Models
{
    public class Message
    {
       public int? MessageId { get; set; }
       public string? Content { get; set; }
       public DateTime? SentDate { get; set; }

       // optional navigation to user from message
       public int? UserId { get; set; }
       public User? User { get; set; }
       
    }
}