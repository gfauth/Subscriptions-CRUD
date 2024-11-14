using System.Text.RegularExpressions;
using FluentValidation;
using Observer.Domain.Models.Requests;

namespace Observer.Domain.Validators
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
                .NotEmpty()
                .Length(3, 150)
                .WithMessage("Informe um nome válido para o usuário.");

            RuleFor(user => user.LastName)
                .NotNull()
                .NotEmpty()
                .Length(3, 150)
                .WithMessage("Informe um sobrenome válido para o usuário.");

            RuleFor(user => user.Birthdate)
                .LessThan(DateTime.Today.AddYears(-17))
                .GreaterThan(DateTime.Today.AddYears(-100))
                .WithMessage("Informe uma data de nascimento válida para o usuário. Apenas maiores de 18 anos.");

            RuleFor(user => user.Login)
                .NotNull()
                .NotEmpty()
                .Length(3, 30)
                .WithMessage("Login precisa conter ao menos 5 dígitos para o usuário.");

            var regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            RuleFor(user => user.Password)
                .NotNull()
                .NotEmpty()
                .Matches(regex)
                .WithMessage("Password precisa conter ao menos 8 dígitos, conter números e caracteres especiais para o usuário.");
        }
    }
}
