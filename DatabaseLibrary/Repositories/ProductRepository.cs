using System.Diagnostics.CodeAnalysis;
using DatabaseLibrary.Constants;
using DomainLibrary.Constants;
using DomainLibrary.Entities;
using DomainLibrary.Envelopes;
using DomainLibrary.Interfaces.Repositories;
using DomainLibrary.Models.LogModels;
using SingleLog.Interfaces;

namespace DatabaseLibrary.Repositories
{
    /// <summary>
    /// Repository for entity Products.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ProductRepository : IProductRepository
    {
        private readonly ISqlServerContext _sqlServerContext;
        private readonly ISingletonLogger<LogModel> _singleLog;

        /// <summary>
        /// Repository constructor.
        /// </summary>
        /// <param name="sqlServerContext">Object ISqlServerContext.</param>
        /// <param name="singleLog">Object ISingleLog.</param>
        public ProductRepository(ISqlServerContext sqlServerContext, ISingletonLogger<LogModel> singleLog)
        {
            _sqlServerContext = sqlServerContext;
            _singleLog = singleLog;
        }

        /// <summary>
        /// Insert new data into Products table.
        /// </summary>
        /// <param name="productData">Object Products</param>
        /// <returns></returns>
        public async Task<IResponse<Products>> InsertProduct(Products productData) =>
            await QueryRunner<Products>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.PRODUCT_DATABASE_CREATE_DATA,
            _sqlServerContext, Queries.InsertProducts, productData);

        /// <summary>
        /// Select an existent into Products table.
        /// </summary>
        /// <param name="productId">Products identification</param>
        /// <returns></returns>
        public async Task<IResponse<Products>> SelectProduct(int productId) =>
            await QueryRunner<Products>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.PRODUCT_DATABASE_RETRIEVE_DATA,
            _sqlServerContext, Queries.SelectOneProducts, new { productId });

        /// <summary>
        /// Update data in existent into Products table.
        /// </summary>
        /// <param name="productData">Object Products</param>
        /// <returns></returns>
        public async Task<IResponse<bool>> UpdateProduct(Products productData) =>
            await QueryRunner<bool>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.PRODUCT_DATABASE_UPDATE_DATA,
            _sqlServerContext, Queries.UpdateProducts, productData);

        /// <summary>
        /// Delete data existent into Products table.
        /// </summary>
        /// <param name="productId">Products identification</param>
        /// <returns></returns>
        public async Task<IResponse<bool>> DeleteProduct(int productId) =>
            await QueryRunner<bool>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.PRODUCT_DATABASE_DELETE_DATA,
            _sqlServerContext, Queries.DeleteProducts, new { productId });
    }
}