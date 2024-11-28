using DomainLibrary.Models.Responses;
using System.Net;

namespace DomainLibrary.Models.Errors
{
    /// <summary>
    /// 
    /// </summary>
    public static class SubscriptionResponseErrors
    {
        public static ResponseEnvelope SubscriptionValidationErrorMessage(string customMessage) => new ResponseEnvelope(HttpStatusCode.BadRequest, customMessage);
        public static readonly ResponseEnvelope CreateSubscriptionError = new ResponseEnvelope(HttpStatusCode.InternalServerError, "Ocorreu um erro durante a criação da subscrição.");
        public static readonly ResponseEnvelope CreateSubscriptionProductNotFound = new ResponseEnvelope(HttpStatusCode.InternalServerError, "O produto selecionado não foi encontrado para a criação da subscrição.");
        public static readonly ResponseEnvelope CreateSubscriptionUserNotFound = new ResponseEnvelope(HttpStatusCode.InternalServerError, "O usuário selecionado não foi encontrado para a criação da subscrição.");
        public static readonly ResponseEnvelope SubscriptionNotFound = new ResponseEnvelope(HttpStatusCode.NotFound, "Nenhuma subscrição encontrada. O identificador informado não resultou em dados nesta ação.");
        public static readonly ResponseEnvelope InvalidSubscriptionId = new ResponseEnvelope(HttpStatusCode.BadRequest, "Informe um 'subscriptionId' válido para a requisição.");
        public static readonly ResponseEnvelope InternalServerError = new ResponseEnvelope(HttpStatusCode.InternalServerError, "Ocorreu um erro durante a execução da requisição.");
    }
}
