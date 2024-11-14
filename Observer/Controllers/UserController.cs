using Microsoft.AspNetCore.Mvc;
using Observer.Constants;
using Observer.Domain.Interfaces;
using Observer.Domain.Models.Errors;
using Observer.Domain.Models.LogModels;
using Observer.Domain.Models.Requests;
using Observer.Domain.Models.Responses;
using SingleLog.Enums;
using SingleLog.Interfaces;
using SingleLog.Models;
using System.Net;

namespace Observer.Controllers
{
    /// <summary>
    /// User Controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly ISingletonLogger<LogModel> _singleLog;

        /// <summary>
        /// General constructor.
        /// </summary>
        /// <param name="userServices">Service class based on IUserServices.</param>
        /// <param name="singleLog">Service class of log based on ISingleLog.</param>
        public UserController(IUserServices userServices, ISingletonLogger<LogModel> singleLog)
        {
            _userServices = userServices ?? throw new ArgumentNullException(nameof(userServices));
            _singleLog = singleLog ?? throw new ArgumentNullException(nameof(singleLog));
        }

        /// <summary>
        /// GET endpoint to retrieve some user data.
        /// </summary>
        /// <param name="userId">User identification.</param>
        /// <returns>Object ResponseEnvelope</returns>
        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status201Created)]
        public async Task<ActionResult<ResponseEnvelope>> UserDetails(int userId)
        {
            var baseLog = await _singleLog.CreateBaseLogAsync();
            baseLog.Request = new { userId };

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.GET_USER_BY_ID, sublog);

            sublog.StartCronometer();

            try
            {
                if (userId <= 0)
                {
                    var responseError = UserResponseErrors.InvalidUserId;
                    baseLog.Response = responseError;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)UserResponseErrors.InvalidUserId.ResponseCode, UserResponseErrors.InvalidUserId);
                }

                var response = await _userServices.RetrieveUser(userId);

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

        /// <summary>
        /// POST endpoint to create a new user in the sistem.
        /// </summary>
        /// <param name="user">Object UserRequest with user data.</param>
        /// <returns>Object ResponseEnvelope</returns>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseEnvelope>> UserCreate(UserRequest user)
        {
            var baseLog = await _singleLog.CreateBaseLogAsync();

            baseLog.Request = user;

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.CREATE_NEW_USER, sublog);

            sublog.StartCronometer();

            try
            {
                var validation = user.IsValid();

                if (!validation.ResponseCode.Equals(HttpStatusCode.Continue))
                {
                    baseLog.Response = validation;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)validation.ResponseCode, validation);
                }

                var response = await _userServices.CreateUser(user);

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

        /// <summary>
        /// PUT endpoint to edit data for a specific user.
        /// </summary>
        /// <param name="userId">User identification.</param>
        /// <param name="user">Object UserRequest with user data.</param>
        /// <returns>Object ResponseEnvelope</returns>
        [HttpPut]
        [Route("{userId}")]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseEnvelope>> UserEdit(int userId, UserRequest user)
        {
            var baseLog = await _singleLog.CreateBaseLogAsync();
            baseLog.Request = new { userId, user };

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.EDIT_USER_BY_ID, sublog);

            sublog.StartCronometer();

            try
            {
                if (userId <= 0)
                {
                    var responseError = UserResponseErrors.InvalidUserId;
                    baseLog.Response = responseError;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)UserResponseErrors.InvalidUserId.ResponseCode, UserResponseErrors.InvalidUserId);
                }

                var validation = user.IsValid();

                if (!validation.ResponseCode.Equals(HttpStatusCode.Continue))
                {
                    baseLog.Response = validation;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)validation.ResponseCode, validation);
                }

                var response = await _userServices.UpdateUser(userId, user);

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

        /// <summary>
        /// DELETE endpoint to delete some user into sistem.
        /// </summary>
        /// <param name="userId">User identification.</param>
        /// <returns>Object ResponseEnvelope</returns>
        [HttpDelete]
        [Route("{userId}")]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ResponseEnvelope), StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseEnvelope>> UserDelete(int userId)
        {
            var baseLog = await _singleLog.CreateBaseLogAsync();
            baseLog.Request = new { userId };

            var sublog = new SubLog();
            await baseLog.AddStepAsync(LogSteps.DELETE_USER_BY_ID, sublog);

            sublog.StartCronometer();

            try
            {
                if (userId <= 0)
                {
                    var responseError = UserResponseErrors.InvalidUserId;
                    baseLog.Response = responseError;
                    baseLog.Level = LogTypes.WARN;

                    return StatusCode((int)UserResponseErrors.InvalidUserId.ResponseCode, UserResponseErrors.InvalidUserId);
                }

                var response = await _userServices.DeleteUser(userId);

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