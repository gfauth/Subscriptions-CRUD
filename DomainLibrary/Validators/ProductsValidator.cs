using DomainLibrary.Models.Requests;
using FluentValidation;

namespace DomainLibrary.Validators
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
                .NotNull().WithMessage("Informe um nome válido para o produto.")
                .NotEmpty().WithMessage("Informe um nome válido para o produto.")
                .Length(3, 150).WithMessage("Informe um nome válido para o produto.");

            RuleFor(prod => prod.Category)
                .NotNull().WithMessage("Informe uma categoria válida para o produto.")
                .NotEmpty().WithMessage("Informe uma categoria válida para o produto.")
                .Length(2, 150).WithMessage("Informe uma categoria válida para o produto.");

            RuleFor(prod => prod.Description)
                .NotNull().WithMessage("Description precisa conter ao menos 3 dígitos.")
                .NotEmpty().WithMessage("Description precisa conter ao menos 3 dígitos.")
                .Length(3, 500).WithMessage("Description precisa conter ao menos 3 dígitos.");

            RuleFor(prod => prod.Stock)
                .LessThan(9999).WithMessage("Não é possível cadastrar um produto com estoque inferior a 1 ou superior a 9999.")
                .GreaterThan(1).WithMessage("Não é possível cadastrar um produto com estoque inferior a 1 ou superior a 9999.");
        }
    }
}
