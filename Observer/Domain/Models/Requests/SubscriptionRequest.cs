using System.Net;
using Observer.Domain.Models.Errors;
using Observer.Domain.Models.Responses;

namespace Observer.Domain.Models.Requests
{
    public record SubscriptionRequest
    {
        public int ProductId { get; private set; }
        public int UserId { get; private set; }

        /// <summary>
        /// Validate subscription dada.
        /// </summary>
        /// <returns>Object ResponseEnvelope.</returns>
        public ResponseEnvelope IsValid()
        {
            if (ProductId >= 0 || UserId <= 0)
                return SubscriptionResponseErrors.SubscriptionValidationErrorMessage("Informe um productId ou userId válido para a subscrição.");

            return new ResponseEnvelope(HttpStatusCode.Continue);
        }
    }
}