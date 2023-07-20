using MediatR;
using UserPermission.API.Application.Common.DTOs;
using UserPermission.API.Application.Common.Interfaces.RepositoryRead;

namespace UserPermission.API.Application.Queries.SyncQueries
{
    public record UpdatePermissionCommand(PermissionDto permissionDto)
        : IRequest;
    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand>
    {
        private readonly IRepositoryRead _repositoryRead;
        public UpdatePermissionCommandHandler(IRepositoryRead repositoryRead)
        {
            _repositoryRead = repositoryRead;
        }
        public async Task Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            await _repositoryRead.Update(request.permissionDto);
        }
    }
}
