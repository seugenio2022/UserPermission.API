using MediatR;
using UserPermission.API.Application.Common.Interfaces.RepositoryWrite;
using UserPermission.API.Domain.Entities;
using UserPermission.API.Domain.Events;

namespace UserPermission.API.Application.Commands.RequestPermission
{
    public record RequestPermissionCommand(string Name, string Description, Guid EmployeeId, Guid PermissionTypeId) : IRequest<Guid>;

    public class RequestPermissionCommandHandler : IRequestHandler<RequestPermissionCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        public RequestPermissionCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        public async Task<Guid> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
        {
            var newPermission = new Permission(request.Name, request.Description, request.EmployeeId, request.PermissionTypeId);

            await _unitOfWork.PermissionRepository.Add(newPermission);
            await _unitOfWork.Save();

            await _mediator.Publish(
                new PermissionRequiredEvent(newPermission.Id,
                newPermission.Name, newPermission.Description,
                newPermission.EmployeeId, newPermission.PermissionTypeId));

            return newPermission.Id;
        }
    }
}
