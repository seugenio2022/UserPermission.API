using MediatR;
using UserPermission.API.Application.Common.Interfaces;
using UserPermission.API.Domain.Events;

namespace UserPermission.API.Application.EventHandlers
{
    public class PermissionRequiredEventHandler : INotificationHandler<PermissionRequiredEvent>
    {
        private readonly IProducerService _producerService;
        private readonly string _operation = "request";
        public PermissionRequiredEventHandler(IProducerService producerService)
        {
            _producerService = producerService;
        }
        public async Task Handle(PermissionRequiredEvent notification, CancellationToken cancellationToken)
        {
            await _producerService.PublishAsync(new Common.DTOs.PublishInputDto<PermissionRequiredEvent>(_operation, notification));
        }
    }
}
