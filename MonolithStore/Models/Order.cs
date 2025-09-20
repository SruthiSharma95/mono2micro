namespace MonolithStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public decimal TotalAmount { get; set; } = 0m;
        public List<OrderItem> Items { get; set; } = new();
    }
}
