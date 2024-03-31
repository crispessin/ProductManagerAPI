using Domain.Entites;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<ICollection<Product?>> SearchByNameAsync(string name, string? orderBy);
        Task<ICollection<Product>> GetProductsAsync(string? orderBy);
        Task<Product> CreateAsync(Product product);
        Task EditAsync(Product product);
        Task DeleteAsync(Product product);
    }
}
