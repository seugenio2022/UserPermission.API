using MediatR;

namespace UserPermission.API.Domain.Common;

public abstract record BaseEvent : INotification
{
}
