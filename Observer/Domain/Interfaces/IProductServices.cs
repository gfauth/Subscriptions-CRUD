using Observer.Domain.ResponsesEnvelope;
using Observer.Presentation.Models.Requests;
using Observer.Presentation.Models.Responses;

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
