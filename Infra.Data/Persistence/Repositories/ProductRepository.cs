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

        public async Task<ICollection<Product>> SearchByNameAsync(string name, string? orderBy)
        {
            if (orderBy != null)
            {
                return await _db.Products.Where(p => p.Name.Contains(name)).OrderBy(GetOrder(orderBy)).ToListAsync();
            }

            return await _db.Products.Where(p => p.Name.Contains(name)).ToListAsync();
        }

        public async Task<ICollection<Product>> GetProductsAsync(string? orderBy)
        {
            if (orderBy != null)
            {
                return await _db.Products.OrderBy(GetOrder(orderBy)).ToListAsync();
            }

            return await _db.Products.ToListAsync();
        }

        private System.Linq.Expressions.Expression<Func<Product, object>> GetOrder(string orderBy)
        {
            switch (orderBy.ToLower())
            {
                case "id": return p => p.ProductId;
                case "stock": return p => p.Stock;
                case "name": return p => p.Name;                
                case "price": return p => p.Price;                
                default: throw new Exception("Parâmetro de ordenação não permitido, opções válidas: [id, stock, name, price]");
            }
        }
    }
}
