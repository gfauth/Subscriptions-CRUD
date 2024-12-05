using DomainLibrary.Entities;
using DomainLibrary.Envelopes;
using DomainLibrary.Models.Notifications;

namespace DomainLibrary.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<IResponse<List<Subscriptions>>> FindSubscriptions();
        Task<IResponse<List<Notifications>>> FindNotifications();
    }
}