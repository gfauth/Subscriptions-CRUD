using Observer.Presentation.Models.Responses;
using System.Net;

namespace Observer.Presentation.Errors
{
    public static class UserResponseErrors
    {
        internal static ResponseEnvelope UserValidationErrorMessage(string customMessage) => new ResponseEnvelope(HttpStatusCode.BadRequest, customMessage);

        internal static readonly ResponseEnvelope CreateUserError = new ResponseEnvelope(HttpStatusCode.InternalServerError, "Ocorreu um erro durante a criação do usuário.");
        internal static readonly ResponseEnvelope UserNotFound = new ResponseEnvelope(HttpStatusCode.NotFound, "Nenhum usuário encontrado. O identificador informado não resultou em dados nesta ação.");
        internal static readonly ResponseEnvelope InternalServerError = new ResponseEnvelope(HttpStatusCode.InternalServerError, "Ocorreu um erro durante a execução da requisição.");
        internal static readonly ResponseEnvelope InvalidUserId = new ResponseEnvelope(HttpStatusCode.BadRequest, "Informe um 'userId' válido para a requisição.");
    }
}
