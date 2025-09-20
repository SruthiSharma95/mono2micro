namespace MonolithStore.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        // Not necessary to populate, but available for convenience
        public List<Product>? Products { get; set; }
    }
}
