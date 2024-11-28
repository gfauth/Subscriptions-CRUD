using DomainLibrary.Envelopes;
using DomainLibrary.Models.Requests;
using DomainLibrary.Models.Responses;

namespace DomainLibrary.Interfaces.Services
{
    public interface IProductServices
    {
        public Task<IResponse<ResponseEnvelope>> RetrieveProduct(int productId);
        public Task<IResponse<ResponseEnvelope>> CreateProduct(ProductRequest product);
        public Task<IResponse<ResponseEnvelope>> UpdateProduct(int productId, ProductRequest product);
        public Task<IResponse<ResponseEnvelope>> DeleteProduct(int productId);

        //public Task<ResponseEnvelope> ListProduct();
    }
}
