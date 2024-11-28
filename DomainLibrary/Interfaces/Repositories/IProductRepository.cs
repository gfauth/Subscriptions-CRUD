using DomainLibrary.Entities;
using DomainLibrary.Envelopes;

namespace DomainLibrary.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IResponse<Products>> InsertProduct(Products productData);
        Task<IResponse<Products>> SelectProduct(int productId);
        Task<IResponse<bool>> UpdateProduct(Products productData);
        Task<IResponse<bool>> DeleteProduct(int productId);
    }
}
