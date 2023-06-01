using MediatR;

namespace WebApp.Domain.Command
{
    public class CreateCommand : IRequest<string>
    {
        public string? Name { get; private set; }
        public decimal Price { get; private set; }
    }

    public class DeleteCommand : IRequest<string>
    {
        public int Id { get; set; }
    }

    public class UpdateCommand : IRequest<string>
    {
        public int Id { get; private set; }
        public string? Name { get; private set; }
        public decimal Price { get; private set; }
    }
}
