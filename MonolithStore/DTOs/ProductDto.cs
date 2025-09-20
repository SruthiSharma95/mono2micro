namespace MonolithStore.DTOs
{
    public record ProductDto(int ProductId, string Name, string? Description, decimal Price, int CategoryId, int Stock);
}
