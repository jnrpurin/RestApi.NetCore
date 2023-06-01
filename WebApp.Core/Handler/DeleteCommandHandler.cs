using MediatR;
using WebApp.Domain.Command;
using WebApp.Domain.Entity;
using WebApp.Domain.Notifications;
using WebApp.Infra.Service;

namespace WebApp.Core.Handler
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryMongo<Product> _repository;
        public DeleteCommandHandler(IMediator mediator, IRepositoryMongo<Product> repository)
        {
            this._mediator = mediator;
            this._repository = repository;
        }
        public async Task<string> Handle(DeleteCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Delete(request.Id);
                await _mediator.Publish(new DeleteNotification
                { Id = request.Id, IsOk = true });
                return await Task.FromResult("Product deleted");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new DeleteNotification
                {
                    Id = request.Id,
                    IsOk = false
                });
                await _mediator.Publish(new ErrorNotification
                {
                    Error = ex.Message,
                    StackError = ex.StackTrace
                });
                return await Task.FromResult("Error on delete");
            }
        }
    }
}
