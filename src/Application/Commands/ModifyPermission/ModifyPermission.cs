using MediatR;
using UserPermission.API.Application.Common.Exceptions;
using UserPermission.API.Application.Common.Interfaces.RepositoryWrite;
using UserPermission.API.Domain.Entities;
using UserPermission.API.Domain.Events;

namespace UserPermission.API.Application.Commands.ModifyPermission
{
    public record ModifyPermissionCommand(Guid Id, string Name, string Description, Guid EmployeeId, Guid PermissionTypeId)
        : IRequest;

    public class ModifyPermissionCommandHandler : IRequestHandler<ModifyPermissionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public ModifyPermissionCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionToUpdate = await _unitOfWork.PermissionRepository.Get(request.Id);

            if (permissionToUpdate == null) throw new NotFoundException(nameof(Permission), request.Id);

            permissionToUpdate.Modify(request.Name, request.Description, request.EmployeeId, request.PermissionTypeId);

            _unitOfWork.PermissionRepository.Update(permissionToUpdate);
            await _unitOfWork.Save();

            await _mediator.Publish(
               new PermissionModifiedEvent(permissionToUpdate.Id,
               permissionToUpdate.Name, permissionToUpdate.Description,
               permissionToUpdate.EmployeeId, permissionToUpdate.PermissionTypeId));
        }
    }
}
