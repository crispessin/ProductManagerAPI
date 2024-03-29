using Application.DTOs;
using FluentValidation;

namespace Application.Validations
{
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
            RuleFor(x => x.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campo estoque deve ser informado.");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Campo nome deve ser informado.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Campo preço deve ser maior que zero.");
        }
    }
}
