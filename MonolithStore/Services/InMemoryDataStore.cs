using MonolithStore.Models;

namespace MonolithStore.Services
{
    // Simple singleton in-memory store with hardcoded seed data.
    public class InMemoryDataStore : IDataStore
    {
        private readonly List<Category> _categories = new();
        private readonly List<Product> _products = new();
        private readonly List<Order> _orders = new();
        private int _nextOrderId = 1;
        private int _nextOrderItemId = 1;

        public InMemoryDataStore()
        {
            Seed();
        }

        private void Seed()
        {
            // Hardcoded categories
            _categories.AddRange(new[]
            {
                new Category { CategoryId = 1, Name = "Electronics" },
                new Category { CategoryId = 2, Name = "Books" },
                new Category { CategoryId = 3, Name = "Home" }
            });

            // Hardcoded products
            _products.AddRange(new[]
            {
                new Product { ProductId = 1, Name = "Wireless Mouse", Description = "Ergonomic wireless mouse", Price = 25.99m, CategoryId = 1, Stock = 100 },
                new Product { ProductId = 2, Name = "Mechanical Keyboard", Description = "RGB mechanical keyboard", Price = 99.50m, CategoryId = 1, Stock = 50 },
                new Product { ProductId = 3, Name = "Noise Cancelling Headphones", Description = "Over-ear, Bluetooth", Price = 149.99m, CategoryId = 1, Stock = 30 },
                new Product { ProductId = 4, Name = "C# in Depth", Description = "Programming book", Price = 39.99m, CategoryId = 2, Stock = 200 },
                new Product { ProductId = 5, Name = "Cooking 101", Description = "Beginner cookbook", Price = 19.99m, CategoryId = 3, Stock = 80 }
            });

            // attach products list to categories for convenience
            foreach (var c in _categories)
            {
                c.Products = _products.Where(p => p.CategoryId == c.CategoryId).ToList();
            }

            // No orders initially
        }

        public IReadOnlyList<Category> Categories => _categories;
        public IReadOnlyList<Product> Products => _products;
        public IReadOnlyList<Order> Orders => _orders;

        public Product? GetProduct(int id) => _products.FirstOrDefault(p => p.ProductId == id);
        public Category? GetCategory(int id) => _categories.FirstOrDefault(c => c.CategoryId == id);
        public IEnumerable<Product> GetProductsByCategory(int categoryId) => _products.Where(p => p.CategoryId == categoryId);

        public Order CreateOrder(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            order.OrderId = _nextOrderId++;
            foreach (var item in order.Items)
            {
                item.OrderItemId = _nextOrderItemId++;
            }
            // compute total
            order.TotalAmount = order.Items.Sum(i => i.UnitPrice * i.Quantity);
            _orders.Add(order);
            return order;
        }
    }
}
