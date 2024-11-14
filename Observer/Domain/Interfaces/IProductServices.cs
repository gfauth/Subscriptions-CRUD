using Observer.Domain.Models.Requests;
using Observer.Domain.Models.Responses;
using Observer.Domain.ResponsesEnvelope;

namespace Observer.Domain.Interfaces
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
