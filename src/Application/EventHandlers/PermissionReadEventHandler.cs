using MediatR;
using UserPermission.API.Application.Common.Interfaces;
using UserPermission.API.Domain.Events;

namespace UserPermission.API.Application.EventHandlers
{
    public class PermissionReadEventHandler : INotificationHandler<PermissionReadEvent>
    {
        private readonly IProducerService _producerService;
        private readonly string _operation = "get";
        public PermissionReadEventHandler(IProducerService producerService)
        {
            _producerService = producerService;
        }
        public async Task Handle(PermissionReadEvent notification, CancellationToken cancellationToken)
        {
            await _producerService.PublishAsync(new Common.DTOs.PublishInputDto<PermissionRequiredEvent>(_operation, null));
        }
    }
}
