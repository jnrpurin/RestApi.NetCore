using MediatR;

namespace WebApp.Domain.Notifications
{
    public class CreateNotification : INotification
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateNotification : INotification
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public bool IsOk { get; set; }
    }

    public class DeleteNotification : INotification
    {
        public int Id { get; set; }
        public bool IsOk { get; set; }
    }

    public class ErrorNotification : INotification
    {
        public string? Error { get; set; }
        public string? StackError { get; set; }
    }
}
