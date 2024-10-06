using System.Collections.Generic;

namespace bidding_platform.Models.ViewModels
{
    public class SellerDashboardViewModel
    {
        public User Seller { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Message> ReceivedMessages { get; set; }
    }
}