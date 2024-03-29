namespace Application.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
