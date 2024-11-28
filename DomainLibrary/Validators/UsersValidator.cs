using System.Text.RegularExpressions;
using DomainLibrary.Models.Requests;
using FluentValidation;

namespace DomainLibrary.Validators
{
    /// <summary>
    /// Data validation for UserRequest.
    /// </summary>
    public class UsersValidator : AbstractValidator<UserRequest>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UsersValidator()
        {
            RuleFor(user => user.Name)
                .NotNull()
                .WithMessage("Informe um nome válido para o usuário.")
                .NotEmpty()
                .WithMessage("Informe um nome válido para o usuário.")
                .Length(3, 150)
                .WithMessage("Informe um nome válido para o usuário.");

            RuleFor(user => user.LastName)
                .NotNull()
                .WithMessage("Informe um sobrenome válido para o usuário.")
                .NotEmpty()
                .WithMessage("Informe um sobrenome válido para o usuário.")
                .Length(3, 150)
                .WithMessage("Informe um sobrenome válido para o usuário.");

            RuleFor(user => user.Birthdate)
                .LessThan(DateTime.Today.AddYears(-17))
                .WithMessage("Informe uma data de nascimento válida para o usuário. Apenas maiores de 18 anos.")
                .GreaterThan(DateTime.Today.AddYears(-100))
                .WithMessage("Informe uma data de nascimento válida para o usuário. Apenas maiores de 18 anos.");

            RuleFor(user => user.Login)
                .NotNull()
                .WithMessage("Login precisa conter ao menos 5 dígitos para o usuário.")
                .NotEmpty()
                .WithMessage("Login precisa conter ao menos 5 dígitos para o usuário.")
                .Length(5, 30)
                .WithMessage("Login precisa conter ao menos 5 dígitos para o usuário.");

            var regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            RuleFor(user => user.Password)
                .NotNull()
                .WithMessage("Password precisa conter ao menos 8 dígitos, conter números e caracteres especiais para o usuário.")
                .NotEmpty()
                .WithMessage("Password precisa conter ao menos 8 dígitos, conter números e caracteres especiais para o usuário.")
                .Matches(regex)
                .WithMessage("Password precisa conter ao menos 8 dígitos, conter números e caracteres especiais para o usuário.");
        }
    }
}
