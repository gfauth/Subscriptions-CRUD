using DomainLibrary.Models.Responses;
using System.Net;

namespace DomainLibrary.Models.Errors
{
    public static class UserResponseErrors
    {
        public static ResponseEnvelope UserValidationErrorMessage(string customMessage) => new ResponseEnvelope(HttpStatusCode.BadRequest, customMessage);
        public static readonly ResponseEnvelope CreateUserError = new ResponseEnvelope(HttpStatusCode.InternalServerError, "Ocorreu um erro durante a criação do usuário.");
        public static readonly ResponseEnvelope UserNotFound = new ResponseEnvelope(HttpStatusCode.NotFound, "Nenhum usuário encontrado. O identificador informado não resultou em dados nesta ação.");
        public static readonly ResponseEnvelope InternalServerError = new ResponseEnvelope(HttpStatusCode.InternalServerError, "Ocorreu um erro durante a execução da requisição.");
        public static readonly ResponseEnvelope InvalidUserId = new ResponseEnvelope(HttpStatusCode.BadRequest, "Informe um 'userId' válido para a requisição.");
    }
}
