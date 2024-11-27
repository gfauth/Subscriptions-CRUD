using System.Net;
using Observer.Domain.Models.Errors;
using Observer.Domain.Models.Responses;

namespace Observer.Domain.Models.Requests
{
    /// <summary>
    /// Request notification subscription request model.
    /// </summary>
    public record SubscriptionRequest
    {
        public SubscriptionRequest(int productId, int userId)
        {
            ProductId = productId;
            UserId = userId;
        }

        /// <summary>
        /// Related Product identification.
        /// </summary>
        /// <example>2</example>
        public int ProductId { get; private set; }

        /// <summary>
        /// Related User identification.
        /// </summary>
        /// <example>2</example>
        public int UserId { get; private set; }

        /// <summary>
        /// Validate subscription dada.
        /// </summary>
        /// <returns>Object ResponseEnvelope.</returns>
        public ResponseEnvelope IsValid()
        {
            if (ProductId <= 0 || UserId <= 0)
                return SubscriptionResponseErrors.SubscriptionValidationErrorMessage("Informe um productId ou userId válido para a subscrição.");

            return new ResponseEnvelope(HttpStatusCode.Continue);
        }
    }
}