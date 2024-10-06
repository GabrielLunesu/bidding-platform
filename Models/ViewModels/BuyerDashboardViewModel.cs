using System.Collections.Generic;

namespace bidding_platform.Models.ViewModels
{
    public class BuyerDashboardViewModel
    {
        public User Buyer { get; set; }
        public IEnumerable<Product> BiddedProducts { get; set; }
        public IEnumerable<Message> ReceivedMessages { get; set; }
    }
}