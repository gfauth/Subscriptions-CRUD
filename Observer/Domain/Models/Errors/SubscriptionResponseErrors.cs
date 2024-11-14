﻿using Observer.Domain.Models.Responses;
using System.Net;

namespace Observer.Domain.Models.Errors
{
    /// <summary>
    /// 
    /// </summary>
    public static class SubscriptionResponseErrors
    {
        internal static ResponseEnvelope SubscriptionValidationErrorMessage(string customMessage) => new ResponseEnvelope(HttpStatusCode.BadRequest, customMessage);

        internal static readonly ResponseEnvelope CreateSubscriptionError = new ResponseEnvelope(HttpStatusCode.InternalServerError, "Ocorreu um erro durante a criação da subscrição.");
        internal static readonly ResponseEnvelope CreateSubscriptionProductNotFound = new ResponseEnvelope(HttpStatusCode.InternalServerError, "O produto selecionado não foi encontrado para a criação da subscrição.");
        internal static readonly ResponseEnvelope CreateSubscriptionUserNotFound = new ResponseEnvelope(HttpStatusCode.InternalServerError, "O usuário selecionado não foi encontrado para a criação da subscrição.");
        internal static readonly ResponseEnvelope SubscriptionNotFound = new ResponseEnvelope(HttpStatusCode.InternalServerError, "Nenhuma subscrição encontrada. O identificador informado não resultou em dados nesta ação.");
        internal static readonly ResponseEnvelope InvalidSubscriptionId = new ResponseEnvelope(HttpStatusCode.BadRequest, "Informe um 'subscriptionId' válido para a requisição.");
        internal static readonly ResponseEnvelope InternalServerError = new ResponseEnvelope(HttpStatusCode.InternalServerError, "Ocorreu um erro durante a execução da requisição.");
    }
}
