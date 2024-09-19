using TechPotentialChallenge.Domain.Enuns;

namespace TechPotentialChallenge.Domain.Model
{
    public class Order
    {
        public Guid Id { get; set; }
        public Status Status { get; set; } = Status.AwaitingPayment;
        public string NameSaler { get; set; } = string.Empty;
        public string CPFSaler { get; set; } = string.Empty;
        public string EmailSaler { get; set; } = string.Empty;
        public string TelephoneSeler { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<OrderItem> OrderItem { get; set; } = [];
    }
}
