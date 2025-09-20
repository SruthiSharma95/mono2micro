using MonolithStore.Models;

namespace MonolithStore.Services
{
    public interface IDataStore
    {
        IReadOnlyList<Category> Categories { get; }
        IReadOnlyList<Product> Products { get; }
        IReadOnlyList<Order> Orders { get; }

        Product? GetProduct(int id);
        Category? GetCategory(int id);
        IEnumerable<Product> GetProductsByCategory(int categoryId);
        Order CreateOrder(Order order); // persists in-memory store
    }
}
