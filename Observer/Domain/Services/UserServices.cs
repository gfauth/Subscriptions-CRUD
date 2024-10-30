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
    /// <summary>
    /// 
    /// </summary>
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly ISingletonLogger<LogModel> _singleLog;
        private readonly MapperConfiguration _mapperConfiguration;

        /// <summary>
        /// Users services constructor.
        /// </summary>
        /// <param name="userRepository">Service class of repository based on IUserRepository.</param>
        /// <param name="singleLog">Service class of log based on ISingleLog.</param>
        public UserServices(IUserRepository userRepository, ISingletonLogger<LogModel> singleLog)
        {
            _userRepository = userRepository;
            _singleLog = singleLog;

            _mapperConfiguration = new MapperConfiguration(config => { config.CreateMap<Users, UsersEnvelope>(); });
        }

        public async Task<IResponse<ResponseEnvelope>> CreateUser(UserRequest user)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.USER_SERVICE_CREATE_USER_PROCESSING, sublog);
            sublog.StartCronometer();

            try
            {
                Users userData = new Users(user);

                var result = await _userRepository.InsertUser(userData);

                if (result is null || !result.IsSuccess)
                    return new ResponseError<ResponseEnvelope>(UserResponseErrors.CreateUserError);

                var mapper = _mapperConfiguration.CreateMapper();
                var envelope = new ResponseEnvelope(HttpStatusCode.Created, $"Usuário {user.Name} criado com sucesso.", mapper.Map<UsersEnvelope>(result.Data));

                return new ResponseOk<ResponseEnvelope>(envelope);
            }
            finally
            {
                sublog.StopCronometer();
            }
        }

        public async Task<IResponse<ResponseEnvelope>> RetrieveUser(int userId)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.USER_SERVICE_RETRIEVE_USER_PROCESSING, sublog);
            sublog.StartCronometer();

            try
            {
                var result = await _userRepository.SelectUser(userId);

                if (result is null || !result.IsSuccess)
                    return new ResponseError<ResponseEnvelope>(UserResponseErrors.UserNotFound);

                var mapper = _mapperConfiguration.CreateMapper();
                var envelope = new ResponseEnvelope(HttpStatusCode.OK, "Usuário recuperado com sucesso.", mapper.Map<UsersEnvelope>(result.Data));

                return new ResponseOk<ResponseEnvelope>(envelope);
            }
            finally
            {
                sublog.StopCronometer();
            }
        }

        public async Task<IResponse<ResponseEnvelope>> DeleteUser(int userId)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.USER_SERVICE_DELETE_USER_PROCESSING, sublog);
            sublog.StartCronometer();

            try
            {
                var result = await _userRepository.DeleteUser(userId);

                if (result is null || !result.IsSuccess)
                    return new ResponseError<ResponseEnvelope>(UserResponseErrors.UserNotFound);

                return new ResponseOk<ResponseEnvelope>(new ResponseEnvelope(HttpStatusCode.OK, $"Usuário {userId} foi deletado com sucesso."));
            }
            finally
            {
                sublog.StopCronometer();
            }
        }

        public async Task<IResponse<ResponseEnvelope>> UpdateUser(int userId, UserRequest user)
        {
            var baseLog = await _singleLog.GetBaseLogAsync();
            var sublog = new SubLog();

            await baseLog.AddStepAsync(LogSteps.USER_SERVICE_UPDATE_USER_PROCESSING, sublog);
            sublog.StartCronometer();

            try
            {
                Users userData = new Users(userId, user);

                var result = await _userRepository.UpdateUser(userData);

                if (result is null || !result.IsSuccess)
                    return new ResponseError<ResponseEnvelope>(UserResponseErrors.UserNotFound);

                var mapper = _mapperConfiguration.CreateMapper();
                var envelope = new ResponseEnvelope(HttpStatusCode.OK, $"Dados do usuário {user.Name} foram alterados com sucesso.", mapper.Map<UsersEnvelope>(userData));

                return new ResponseOk<ResponseEnvelope>(envelope);
            }
            finally
            {
                sublog.StopCronometer();
            }
        }
    }
}
