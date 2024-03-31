using Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Persistence.DbContexts
{

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product(1, "Teclado", 10, 1.10M),
                new Product(2, "Monitor", 20, 20),
                new Product(3, "Mouse", 30, 30),
                new Product(4, "Fone", 40, 30),
                new Product(5, "Carregador", 50, 30)
            );
        }
    }
}
