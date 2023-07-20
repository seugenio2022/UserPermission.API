using MediatR;
using UserPermission.API.Application.Common.Interfaces;
using UserPermission.API.Domain.Events;

namespace UserPermission.API.Application.EventHandlers
{
    public class PermissionModifiedEventHandler : INotificationHandler<PermissionModifiedEvent>
    {
        private readonly IProducerService _producerService;
        private readonly string _operation = "modify";
        public PermissionModifiedEventHandler(IProducerService producerService)
        {
            _producerService = producerService;
        }
        public async Task Handle(PermissionModifiedEvent notification, CancellationToken cancellationToken)
        {
            await _producerService.PublishAsync(new Common.DTOs.PublishInputDto<PermissionModifiedEvent>(_operation, notification));
        }
    }
}
