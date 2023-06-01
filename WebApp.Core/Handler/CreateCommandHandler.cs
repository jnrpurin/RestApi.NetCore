using MediatR;
using WebApp.Domain.Command;
using WebApp.Domain.Entity;
using WebApp.Domain.Notifications;
using WebApp.Infra.Repository;

namespace WebApp.Core.Handler
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, string>
    {
        private readonly IMediator mediator;
        private readonly IRepositoryMongo<Product> repository;

        public CreateCommandHandler(IMediator mediator, IRepositoryMongo<Product> repository)
        {
            this.mediator = mediator;
            this.repository = repository;
        }
        public async Task<string> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var Product = new Product { Name = request.Name, Price = request.Price };

            try
            {
                await repository.Add(Product);
                await mediator.Publish(new CreateNotification { Id = Product.Id, Name = Product.Name, Price = Product.Price });
                return await Task.FromResult("Product criada com sucesso");
            }
            catch (Exception ex)
            {
                await mediator.Publish(new CreateNotification { Id = Product.Id, Name = Product.Name, Price = Product.Price });
                await mediator.Publish(new ErrorNotification { Error = ex.Message, StackError = ex.StackTrace });
                return await Task.FromResult("Error on Create Command");
            }
        }
    }
}
