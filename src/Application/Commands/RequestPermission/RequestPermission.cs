using MediatR;
using UserPermission.API.Application.Common.Interfaces.RepositoryWrite;
using UserPermission.API.Domain.Entities;
using UserPermission.API.Domain.Events;
using UserPermission.API.Domain.Interfaces;

namespace UserPermission.API.Application.Commands.RequestPermission
{
    public record RequestPermissionCommand(string Name, string Description, Guid EmployeeId, Guid PermissionTypeId) : IRequest<Guid>;

    public class RequestPermissionCommandHandler : IRequestHandler<RequestPermissionCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPermissionTypeRepository _permissionTypeRepository;
        public RequestPermissionCommandHandler(
            IUnitOfWork unitOfWork,
            IMediator mediator,
            IEmployeeRepository employeeRepository,
            IPermissionTypeRepository permissionTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _employeeRepository = employeeRepository;
            _permissionTypeRepository = permissionTypeRepository;
        }
        public async Task<Guid> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
        {
            var newPermission = await Permission.CreateAsync(
                request.Name,
                request.Description,
                request.EmployeeId,
                request.PermissionTypeId,
                _employeeRepository,
                _permissionTypeRepository);

            await _unitOfWork.PermissionRepository.AddAsync(newPermission);
            await _unitOfWork.SaveAsync();

            await _mediator.Publish(
                new PermissionRequiredEvent(newPermission.Id,
                newPermission.Name, newPermission.Description,
                newPermission.EmployeeId, newPermission.PermissionTypeId));

            return newPermission.Id;
        }
    }
}
