using Microsoft.AspNetCore.Mvc;
using Observer.Constants;
using Observer.Domain.Interfaces;
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
    /// Subscription Controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionServices _subscriptionServices;
        private readonly ISingletonLogger<LogModel> _singleLog;

        /// <summary>
        /// General constructor.
        /// </summary>
        /// <param name="subscriptionServices">Service class based on ISubscriptionServices.</param>
        /// <param name="singleLog">Service class of log based on ISingleLog.</param>
        public SubscriptionController(ISubscriptionServices subscriptionServices, ISingletonLogger<LogModel> singleLog)
        {
            _subscriptionServices = subscriptionServices ?? throw new ArgumentNullException(nameof(subscriptionServices));
            _singleLog = singleLog ?? throw new ArgumentNullException(nameof(singleLog));
        }

        /// <summary>
        /// GET endpoint to retrieve some subscription data.
        /// </summary>
        /// <param name="subscriptionId">Subscription identification.</param>
        /// <returns>Object SubscriptionResponse</returns>
        [HttpGet]
        [Route("{subscriptionId}")]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status201Created)]
        public async Task<ActionResult<ResponseEnvelope>> SubscriptionDetails(int subscriptionId)
        {
            var baseLog = await _singleLog.CreateBaseLogAsync();
            baseLog.Request = new { subscriptionId };

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.GET_SUBSCRIPTION_BY_ID, sublog);
            sublog.StartCronometer();

            try
            {
                if (subscriptionId <= 0)
                {
                    var responseError = SubscriptionResponseErrors.InvalidSubscriptionId;
                    baseLog.Response = responseError;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)SubscriptionResponseErrors.InvalidSubscriptionId.ResponseCode, SubscriptionResponseErrors.InvalidSubscriptionId);
                }

                var response = await _subscriptionServices.RetrieveSubscription(subscriptionId);

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

                var responseError = SubscriptionResponseErrors.InternalServerError;
                baseLog.Response = responseError;

                return StatusCode((int)responseError.ResponseCode, responseError);
            }
            finally
            {
                sublog.StopCronometer();

                await _singleLog.WriteLogAsync(baseLog);
            }
        }

        /// <summary>
        /// POST endpoint to create a new user in the sistem.
        /// </summary>
        /// <param name="subscription">Object SubscriptionRequest with user data.</param>
        /// <returns>Object SubscriptionResponse</returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseEnvelope>> SubscriptionCreate(SubscriptionRequest subscription)
        {
            var baseLog = await _singleLog.CreateBaseLogAsync();

            baseLog.Request = subscription;

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.CREATE_NEW_SUBSCRIPTION, sublog);

            sublog.StartCronometer();

            try
            {
                var validation = subscription.IsValid();

                if (!validation.ResponseCode.Equals(HttpStatusCode.Continue))
                {
                    baseLog.Response = validation;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)validation.ResponseCode, validation);
                }

                var response = await _subscriptionServices.CreateSubscription(subscription);

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

                var responseError = SubscriptionResponseErrors.InternalServerError;
                baseLog.Response = responseError;

                return StatusCode((int)responseError.ResponseCode, responseError);
            }
            finally
            {
                sublog.StopCronometer();

                await _singleLog.WriteLogAsync(baseLog);
            }
        }

        /// <summary>
        /// DELETE endpoint to delete some user into sistem.
        /// </summary>
        /// <param name="subscriptionId">Subscription identification.</param>
        /// <returns>Object SubscriptionResponse</returns>
        [HttpDelete]
        [Route("{subscriptionId}")]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseEnvelope>> SubscriptionDelete(int subscriptionId)
        {
            var baseLog = await _singleLog.CreateBaseLogAsync();
            baseLog.Request = new { subscriptionId };

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.DELETE_SUBSCRIPTION_BY_ID, sublog);

            sublog.StartCronometer();

            try
            {
                if (subscriptionId <= 0)
                {
                    var responseError = SubscriptionResponseErrors.InvalidSubscriptionId;
                    baseLog.Response = responseError;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)SubscriptionResponseErrors.InvalidSubscriptionId.ResponseCode, SubscriptionResponseErrors.InvalidSubscriptionId);
                }

                var response = await _subscriptionServices.DeleteSubscription(subscriptionId);

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
                sublog.StopCronometer();

                await _singleLog.WriteLogAsync(baseLog);
            }
        }
    }
}