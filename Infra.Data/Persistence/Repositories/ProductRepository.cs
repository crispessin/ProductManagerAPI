using Domain.Entites;
using Domain.Repositories;
using Infra.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Persistence.Repositories
{
    public class ProductRepository(ApplicationDbContext db) : IProductRepository
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<Product> CreateAsync(Product product)
        {
            _db.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task DeleteAsync(Product product)
        {
            _db.Remove(product);
            await _db.SaveChangesAsync();
        }

        public async Task EditAsync(Product product)
        {
            _db.Update(product);
            await _db.SaveChangesAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task<Product?> SearchByNameAsync(string name)
        {
            return await _db.Products.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<ICollection<Product>> GetProductsAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<List<Product>> GetProductsSortedByAsync(string orderBy)
        {
            throw new NotImplementedException();
        }
    }
}
