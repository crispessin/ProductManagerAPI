using Domain.Validations;

namespace Domain.Entites
{
    public class Product
    {
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

        public Product(string name, int stock, decimal price)
        {
            Validation(name, stock, price);
        }

        public Product(int id, string name, int stock, decimal price)
        {
            DomainValidationException.When(id < 0, "Id do produto deve ser informado!");
            ProductId = id;
            Validation(name, stock, price);
        }

        private void Validation(string name, int stock, decimal price)
        {
            DomainValidationException.When(string.IsNullOrEmpty(name), "Nome deve ser informado!");
            DomainValidationException.When(stock < 0, "Estoque deve ser informado!");
            DomainValidationException.When(price < 0, "Preço deve ser informado!");

            Name = name;
            Stock = stock;
            Price = price;
        }
    }

}
