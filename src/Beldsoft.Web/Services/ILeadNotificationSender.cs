namespace Beldsoft.Web.Services;

internal interface ILeadNotificationSender
{
    string DeliveryMode { get; }

    Task SendAsync(LeadNotification notification, CancellationToken cancellationToken = default);
}
