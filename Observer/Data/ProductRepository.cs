using Observer.Constants;
using Observer.Data.Entities;
using Observer.Data.Interfaces;
using Observer.Domain.ResponsesEnvelope;
using Observer.Presentation.Logs;
using SingleLog.Interfaces;

namespace Observer.Data
{
    /// <summary>
    /// Repository for entity Products.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ISqlServerContext _sqlServerContext;
        private readonly ISingleLog<LogModel> _singleLog;

        /// <summary>
        /// Repository constructor.
        /// </summary>
        /// <param name="sqlServerContext">Object ISqlServerContext.</param>
        /// <param name="singleLog">Object ISingleLog.</param>
        public ProductRepository(ISqlServerContext sqlServerContext, ISingleLog<LogModel> singleLog)
        {
            _sqlServerContext = sqlServerContext;
            _singleLog = singleLog;
        }

        public async Task<IResponse<Products>> InsertProduct(Products productData) =>
            await QueryExecuter<Products>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.PRODUCT_DATABASE_CREATE_DATA, 
            _sqlServerContext, QueryData.InsertProducts, productData);

        public async Task<IResponse<Products>> SelectProduct(int productId) =>
            await QueryExecuter<Products>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.PRODUCT_DATABASE_RETRIEVE_DATA, 
            _sqlServerContext, QueryData.InsertProducts, productId);

        public async Task<IResponse<bool>> UpdateProduct(Products productData) =>
            await QueryExecuter<bool>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.PRODUCT_DATABASE_UPDATE_DATA,
            _sqlServerContext, QueryData.InsertProducts, productData);

        public async Task<IResponse<bool>> DeleteProduct(int productId) =>
            await QueryExecuter<bool>.ExecuteQuerySingleTAsync(_singleLog, LogSteps.PRODUCT_DATABASE_DELETE_DATA, 
            _sqlServerContext, QueryData.SelectOneProducts, productId);
    }
}
