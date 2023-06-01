using MediatR;
using WebApp.Domain.Command;
using WebApp.Domain.Entity;
using WebApp.Domain.Notifications;
using WebApp.Infra.Repository;

namespace WebApp.Core.Handler
{
    public class UpdateCommandHandler
    {
        private readonly IMediator mediator;
        private readonly IRepositoryMongo<Product> repository;

        public UpdateCommandHandler(IMediator mediator, IRepositoryMongo<Product> repository)
        {
            this.mediator = mediator;
            this.repository = repository;
        }

        public async Task<string> Handle(UpdateCommand request,
            CancellationToken cancellationToken)
        {
            var Product = new Product
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price
            };
            try
            {
                await repository.Edit(Product);
                await mediator.Publish(new UpdateNotification
                { Id = Product.Id, Name = Product.Name, Price = Product.Price });
                return await Task.FromResult("Product updated");
            }
            catch (Exception ex)
            {
                await mediator.Publish(new UpdateNotification
                {
                    Id = Product.Id,
                    Name = Product.Name,
                    Price = Product.Price
                });

                await mediator.Publish(new ErrorNotification
                {
                    Error = ex.Message,
                    StackError = ex.StackTrace
                });
                return await Task.FromResult("Error on Update");
            }
        }
    }
}