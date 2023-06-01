using MediatR;
using WebApp.Domain.Notifications;

namespace WebApp.Core.EventsHandlers
{
    public class LogEventHandler :
                                INotificationHandler<CreateNotification>,
                                INotificationHandler<UpdateNotification>,
                                INotificationHandler<DeleteNotification>,
                                INotificationHandler<ErrorNotification>
    {
        public Task Handle(CreateNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Create: '{notification.Id} " +
                    $"- {notification.Name} - {notification.Price}'");
            });
        }
        public Task Handle(UpdateNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Update: '{notification.Id} - {notification.Name} " +
                    $"- {notification.Price} - {notification.IsOk}'");
            });
        }
        public Task Handle(DeleteNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Delete: '{notification.Id} " +
                    $"- {notification.IsOk}'");
            });
        }
        public Task Handle(ErrorNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ERROR: '{notification.Error} \n {notification.StackError}'");
            });
        }
    }
}
