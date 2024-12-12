using DomainLibrary.Envelopes;
using DomainLibrary.Interfaces.Repositories;
using DomainLibrary.Interfaces.WorkServices;

namespace DomainLibrary.WorkServices
{
    public class NotifyProcess : INotifyProcess
    {
        private readonly INotificationRepository _notificationRepository;
        public NotifyProcess(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
        }
        public async Task<IResponse<bool>> Process()
        {
            try
            {
                var subscriptionsResponse = await _notificationRepository.FindSubscriptions();

                if (subscriptionsResponse is null || !subscriptionsResponse.IsSuccess)
                    return new ResponseOk<bool>(false, "nenhuma subscrição encontrada para ser verificada e processada.");

                foreach (var subscription in subscriptionsResponse.Data)
                {

                }

                return new ResponseOk<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResponseError<bool>(false, ex.Message);
            }
            finally
            {

            }
        }
    }
}
