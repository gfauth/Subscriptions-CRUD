using Observer.Domain.Models.Errors;
using Observer.Domain.Models.Responses;
using Observer.Domain.Validators;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.RegularExpressions;

namespace Observer.Domain.Models.Requests
{
    /// <summary>
    /// Request record for use on UserController.
    /// </summary>
    public record UserRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="LastName"></param>
        /// <param name="Birthdate"></param>
        /// <param name="Document"></param>
        /// <param name="Login"></param>
        /// <param name="Password"></param>
        public UserRequest([Required] string Name, [Required] string LastName, [Required] DateTime Birthdate, string Document, [Required] string Login, [Required] string Password)
        {
            this.Name = Name;
            this.LastName = LastName;
            this.Birthdate = Birthdate;
            this.Document = Document;
            this.Login = Login;
            this.Password = Password;
        }

        /// <summary>
        /// User first name.
        /// </summary>
        /// <example>Jhon</example>
        public string Name { get; }

        /// <summary>
        /// User last name.
        /// </summary>
        /// <example>Blevers</example>
        public string LastName { get; }

        /// <summary>
        /// User birthdate.
        /// </summary>
        /// <example>2000-05-12</example>
        public DateTime Birthdate { get; }

        /// <summary>
        /// User identification document.
        /// </summary>
        /// <example>12345678912</example>
        public string Document { get; }

        /// <summary>
        /// User login.
        /// </summary>
        /// <example>mycustomlogin</example>
        public string Login { get; }

        /// <summary>
        /// User password.
        /// </summary>
        /// <example>123!@Best</example>
        public string Password { get; }

        /// <summary>
        /// Validate User data and return a object record for return to requester.
        /// </summary>
        /// <returns>Record UserResponse.</returns>
        public ResponseEnvelope IsValid()
        {
            UsersValidator validationRules = new UsersValidator();
            var result = validationRules.Validate(this);

            return result.IsValid ? new ResponseEnvelope(HttpStatusCode.Continue) :
                UserResponseErrors.UserValidationErrorMessage(result.Errors.FirstOrDefault()!.ErrorMessage);
        }
    }
}