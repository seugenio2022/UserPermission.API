using MediatR;
using UserPermission.API.Application.Common.Exceptions;
using UserPermission.API.Application.Common.Interfaces.RepositoryWrite;
using UserPermission.API.Domain.Entities;
using UserPermission.API.Domain.Events;
using UserPermission.API.Domain.Interfaces;

namespace UserPermission.API.Application.Commands.ModifyPermission
{
    public record ModifyPermissionCommand(Guid Id, string Name, string Description, Guid EmployeeId, Guid PermissionTypeId)
        : IRequest;

    public class ModifyPermissionCommandHandler : IRequestHandler<ModifyPermissionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPermissionTypeRepository _permissionTypeRepository;

        public ModifyPermissionCommandHandler(IUnitOfWork unitOfWork, IMediator mediator, 
            IEmployeeRepository employeeRepository,
            IPermissionTypeRepository permissionTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _employeeRepository = employeeRepository;
            _permissionTypeRepository = permissionTypeRepository;
        }

        public async Task Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionToUpdate = await _unitOfWork.PermissionRepository.GetAsync(request.Id);

            if (permissionToUpdate == null) throw new NotFoundException(nameof(Permission), request.Id);

            await permissionToUpdate.ModifyAsync(
                request.Name,
                request.Description,
                request.EmployeeId,
                request.PermissionTypeId,
                _employeeRepository,
                _permissionTypeRepository);

            _unitOfWork.PermissionRepository.Update(permissionToUpdate);
            await _unitOfWork.SaveAsync();

            await _mediator.Publish(
               new PermissionModifiedEvent(permissionToUpdate.Id,
               permissionToUpdate.Name, permissionToUpdate.Description,
               permissionToUpdate.EmployeeId, permissionToUpdate.PermissionTypeId));
        }
    }
}
