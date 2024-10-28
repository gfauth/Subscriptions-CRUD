using Microsoft.AspNetCore.Mvc;
using NLog.Config;
using Observer.Constants;
using Observer.Data.Entities;
using Observer.Domain.Interfaces;
using Observer.Domain.Services;
using Observer.Presentation.Errors;
using Observer.Presentation.Logs;
using Observer.Presentation.Models.Requests;
using Observer.Presentation.Models.Responses;
using SingleLog.Enums;
using SingleLog.Interfaces;
using SingleLog.Models;
using System.Net;

namespace Observer.Controllers
{
    /// <summary>
    /// Product Controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;
        private readonly ISingleLog<LogModel> _singleLog;

        /// <summary>
        /// General constructor.
        /// </summary>
        /// <param name="productServices">Service class based on IProductServices.</param>
        /// <param name="singleLog">Service class of log based on ISingleLog.</param>
        public ProductController(IProductServices productServices, ISingleLog<LogModel> singleLog)
        {
            _productServices = productServices ?? throw new ArgumentNullException(nameof(productServices));
            _singleLog = singleLog ?? throw new ArgumentNullException(nameof(singleLog));
        }

        /// <summary>
        /// GET endpoint to retrieve some product data.
        /// </summary>
        /// <param name="productId">Product identification.</param>
        /// <returns>Object ProductResponse</returns>
        [HttpGet]
        [Route("{productId}")]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status201Created)]
        public async Task<ActionResult<ResponseEnvelope>> ProductDetails(int productId)
        {
            var baseLog = await _singleLog.CreateBaseLogAsync();
            baseLog.Request = new { productId };

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.GET_PRODUCT_BY_ID, sublog);
            sublog.StopwatchStart();

            try
            {
                if (productId <= 0)
                {
                    var responseError = ProductResponseErrors.InvalidProductId;
                    baseLog.Response = responseError;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)ProductResponseErrors.InvalidProductId.ResponseCode, ProductResponseErrors.InvalidProductId);
                }

                var response = await _productServices.RetrieveProduct(productId);

                baseLog.Response = response;

                if (response.Data.ResponseCode.Equals(HttpStatusCode.OK))
                {
                    baseLog.Level = LogTypes.INFO;

                    return Ok(response.Data);
                }

                baseLog.Level = LogTypes.WARN;

                return StatusCode((int)response.Data.ResponseCode, response.Data);
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;
                baseLog.Level = LogTypes.ERROR;

                var responseError = ProductResponseErrors.InternalServerError;
                baseLog.Response = responseError;

                return StatusCode((int)responseError.ResponseCode, responseError);
            }
            finally
            {
                sublog.StopwatchStop();

                await _singleLog.WriteLogAsync(baseLog);
            }
        }

        /// <summary>
        /// POST endpoint to create a new user in the sistem.
        /// </summary>
        /// <param name="product">Object ProductRequest with user data.</param>
        /// <returns>Object ProductResponse</returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseEnvelope>> ProductCreate(ProductRequest product)
        {
            var baseLog = await _singleLog.CreateBaseLogAsync();

            baseLog.Request = product;

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.CREATE_NEW_PRODUCT, sublog);

            sublog.StopwatchStart();

            try
            {
                var validation = product.IsValid();

                if (!validation.ResponseCode.Equals(HttpStatusCode.Continue))
                {
                    baseLog.Response = validation;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)validation.ResponseCode, validation);
                }

                var response = await _productServices.CreateProduct(product);

                baseLog.Response = response;

                if (response.Data.ResponseCode.Equals(HttpStatusCode.Created))
                {
                    baseLog.Level = LogTypes.INFO;

                    return Ok(response.Data);
                }

                baseLog.Level = LogTypes.WARN;

                return StatusCode((int)response.Data.ResponseCode, response.Data);
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;
                baseLog.Level = LogTypes.ERROR;

                var responseError = ProductResponseErrors.InternalServerError;
                baseLog.Response = responseError;

                return StatusCode((int)responseError.ResponseCode, responseError);
            }
            finally
            {
                sublog.StopwatchStop();

                await _singleLog.WriteLogAsync(baseLog);
            }
        }

        /// <summary>
        /// PUT endpoint to edit data for a specific user.
        /// </summary>
        /// <param name="productId">Product identification.</param>
        /// <param name="product">Object ProductRequest with user data.</param>
        /// <returns>Object ProductResponse</returns>
        [HttpPut]
        [Route("{productId}")]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseEnvelope>> ProductEdit(int productId, ProductRequest product)
        {
            var baseLog = await _singleLog.CreateBaseLogAsync();
            baseLog.Request = new { productId, product };

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.EDIT_PRODUCT_BY_ID, sublog);

            sublog.StopwatchStart();

            try
            {
                if (productId <= 0)
                {
                    var responseError = ProductResponseErrors.InvalidProductId;
                    baseLog.Response = responseError;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)ProductResponseErrors.InvalidProductId.ResponseCode, ProductResponseErrors.InvalidProductId);
                }

                var validation = product.IsValid();

                if (!validation.ResponseCode.Equals(HttpStatusCode.Continue))
                {
                    baseLog.Response = validation;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)validation.ResponseCode, validation);
                }

                var response = await _productServices.UpdateProduct(productId, product);

                baseLog.Response = response;

                if (response.Data.ResponseCode.Equals(HttpStatusCode.OK))
                {
                    baseLog.Level = LogTypes.INFO;

                    return Ok(response.Data);
                }

                baseLog.Level = LogTypes.WARN;

                return StatusCode((int)response.Data.ResponseCode, response.Data);
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;
                baseLog.Level = LogTypes.ERROR;

                var responseError = ProductResponseErrors.InternalServerError;
                baseLog.Response = responseError;

                return StatusCode((int)responseError.ResponseCode, responseError);
            }
            finally
            {
                sublog.StopwatchStop();

                await _singleLog.WriteLogAsync(baseLog);
            }
        }

        /// <summary>
        /// DELETE endpoint to delete some user into sistem.
        /// </summary>
        /// <param name="productId">Product identification.</param>
        /// <returns>Object ProductResponse</returns>
        [HttpDelete]
        [Route("{productId}")]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseEnvelope>> ProductDelete(int productId)
        {
            var baseLog = await _singleLog.CreateBaseLogAsync();
            baseLog.Request = new { productId };

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.DELETE_PRODUCT_BY_ID, sublog);

            sublog.StopwatchStart();

            try
            {
                if (productId <= 0)
                {
                    var responseError = ProductResponseErrors.InvalidProductId;
                    baseLog.Response = responseError;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)ProductResponseErrors.InvalidProductId.ResponseCode, ProductResponseErrors.InvalidProductId);
                }

                var response = await _productServices.DeleteProduct(productId);

                baseLog.Response = response;

                if (response.Data.ResponseCode.Equals(HttpStatusCode.OK))
                {
                    baseLog.Level = LogTypes.INFO;

                    return Ok(response.Data);
                }

                baseLog.Level = LogTypes.WARN;

                return StatusCode((int)response.Data.ResponseCode, response.Data);
            }
            catch (Exception ex)
            {
                sublog.Exception = ex;
                baseLog.Level = LogTypes.ERROR;

                var responseError = UserResponseErrors.InternalServerError;
                baseLog.Response = responseError;

                return StatusCode((int)responseError.ResponseCode, responseError);
            }
            finally
            {
                sublog.StopwatchStop();

                await _singleLog.WriteLogAsync(baseLog);
            }
        }
    }
}