using Observer.Presentation.Models.Responses;
using Observer.Constants;
using Observer.Data.Entities;
using Observer.Data.Interfaces;
using Observer.Domain.Interfaces;
using Observer.Presentation.Logs;
using Observer.Presentation.Models.Requests;
using SingleLog.Interfaces;
using SingleLog.Models;
using System.Net;
using Observer.Presentation.Errors;
using Observer.Domain.ResponsesEnvelope;
using AutoMapper;

namespace Observer.Domain.Services
{
    public class ProductServices : IProductServices
    {
        private readonly ISingletonLogger<LogModel> _singleLog;
        private readonly IProductRepository _productRepository;
        private readonly MapperConfiguration _mapperConfiguration;

        /// <summary>
        /// Users services constructor.
        /// </summary>
        /// <param name="productRepository">Service class of repository based on IProductRepository.</param>
        /// <param name="singleLog">Service class of log based on ISingleLog.</param>
        public ProductServices(IProductRepository productRepository, ISingletonLogger<LogModel> singleLog)
        {
            _productRepository = productRepository;
            _singleLog = singleLog;

            _mapperConfiguration = new MapperConfiguration(config => { config.CreateMap<Products, ProductsEnvelope>(); });
        }

        public async Task<IResponse<ResponseEnvelope>> CreateProduct(ProductRequest product)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.PRODUCT_SERVICE_CREATE_PRODUCT_PROCESSING, sublog);
            sublog.StartCronometer();

            try
            {
                Products productData = new Products(product);

                var result = await _productRepository.InsertProduct(productData);

                if (result is null || !result.IsSuccess)
                    return new ResponseError<ResponseEnvelope>(ProductResponseErrors.CreateProductError);

                var mapper = _mapperConfiguration.CreateMapper();
                var envelope = new ResponseEnvelope(HttpStatusCode.Created, $"Produto {product.Name} criado com sucesso.", mapper.Map<ProductsEnvelope>(result.Data));

                return new ResponseOk<ResponseEnvelope>(envelope);
            }
            finally
            {
                sublog.StopCronometer();
            }
        }

        public async Task<IResponse<ResponseEnvelope>> RetrieveProduct(int productId)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.PRODUCT_SERVICE_RETRIEVE_PRODUCT_PROCESSING, sublog);
            sublog.StartCronometer();

            try
            {
                var result = await _productRepository.SelectProduct(productId);

                if (result is null || !result.IsSuccess)
                    return new ResponseError<ResponseEnvelope>(ProductResponseErrors.ProductNotFound);

                var mapper = _mapperConfiguration.CreateMapper();
                var envelope = new ResponseEnvelope(HttpStatusCode.OK, "Produto recuperado com sucesso.", mapper.Map<ProductsEnvelope>(result.Data));

                return new ResponseOk<ResponseEnvelope>(envelope);
            }
            finally
            {
                sublog.StopCronometer();
            }
        }

        public async Task<IResponse<ResponseEnvelope>> DeleteProduct(int productId)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.PRODUCT_SERVICE_DELETE_PRODUCT_PROCESSING, sublog);
            sublog.StartCronometer();

            try
            {
                var result = await _productRepository.DeleteProduct(productId);

                if (result is null || !result.IsSuccess)
                    return new ResponseError<ResponseEnvelope>(ProductResponseErrors.ProductNotFound);

                return new ResponseOk<ResponseEnvelope>(new ResponseEnvelope(HttpStatusCode.OK, $"Produto {productId} foi deletado com sucesso."));
            }
            finally
            {
                sublog.StopCronometer();
            }
        }

        public async Task<IResponse<ResponseEnvelope>> UpdateProduct(int productId, ProductRequest product)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.PRODUCT_SERVICE_UPDATE_PRODUCT_PROCESSING, sublog);
            sublog.StartCronometer();

            try
            {
                Products productData = new Products(productId, product);

                var result = await _productRepository.UpdateProduct(productData);

                if (result is null || !result.IsSuccess)
                    return new ResponseError<ResponseEnvelope>(ProductResponseErrors.ProductNotFound);

                var mapper = _mapperConfiguration.CreateMapper();
                var envelope = new ResponseEnvelope(HttpStatusCode.OK, $"Dados do produto {product.Name} foram alterados com sucesso.", mapper.Map<ProductsEnvelope>(productData));

                return new ResponseOk<ResponseEnvelope>(envelope);
            }
            finally
            {
                sublog.StopCronometer();
            }
        }
    }
}