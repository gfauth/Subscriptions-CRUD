using DomainLibrary.Models.Responses;
using System.Net;

namespace DomainLibrary.Models.Errors
{
    /// <summary>
    /// 
    /// </summary>
    public static class ProductResponseErrors
    {
        public static ResponseEnvelope ProductValidationErrorMessage(string customMessage) => new ResponseEnvelope(HttpStatusCode.BadRequest, customMessage);
        public static readonly ResponseEnvelope CreateProductError = new ResponseEnvelope(HttpStatusCode.InternalServerError, "Ocorreu um erro durante a criação do produto.");
        public static readonly ResponseEnvelope ProductNotFound = new ResponseEnvelope(HttpStatusCode.NotFound, "Nenhum produto encontrado. O identificador informado não resultou em dados nesta ação.");
        public static readonly ResponseEnvelope InternalServerError = new ResponseEnvelope(HttpStatusCode.InternalServerError, "Ocorreu um erro durante a execução da requisição.");
        public static readonly ResponseEnvelope InvalidProductId = new ResponseEnvelope(HttpStatusCode.BadRequest, "Informe um 'productId' válido para a requisição.");
    }
}
