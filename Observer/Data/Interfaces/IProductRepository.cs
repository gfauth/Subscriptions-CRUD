using Observer.Data.Entities;
using Observer.Domain.ResponsesEnvelope;

namespace Observer.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<IResponse<Products>> InsertProduct(Products productData);
        Task<IResponse<Products>> SelectProduct(int productId);
        Task<IResponse<bool>> UpdateProduct(Products productData);
        Task<IResponse<bool>> DeleteProduct(int productId);
    }
}
