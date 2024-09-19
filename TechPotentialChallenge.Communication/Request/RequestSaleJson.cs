namespace TechPotentialChallenge.Communication.Request
{
    public class RequestSaleJson
    {
        public string NameSaler { get; set; } = string.Empty;
        public string CPFSaler { get; set; } = string.Empty;
        public string EmailSaler { get; set; } = string.Empty;
        public string TelephoneSeler { get; set; } = string.Empty;
        public List<OrderItemRequest> OrderItem { get; set; } = [];
    }

    public class OrderItemRequest
    {
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
