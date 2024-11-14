using FluentValidation;
using Observer.Domain.Models.Requests;

namespace Observer.Domain.Validators
{
    /// <summary>
    /// Data validation for UserRequest.
    /// </summary>
    public class ProductsValidator : AbstractValidator<ProductRequest>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductsValidator()
        {
            RuleFor(prod => prod.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Informe um nome válido para o produto.");

            RuleFor(prod => prod.Category)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("Informe uma categoria válida para o produto.");

            RuleFor(prod => prod.Description)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("Description precisa conter ao menos 5 dígitos.");

            RuleFor(prod => prod.Stock)
                .LessThan(1)
                .GreaterThan(9999)
                .WithMessage("Não é possível cadastrar um produto com estoque inferior a 1 ou superior a 9999.");
        }
    }
}
