using MediatR;
using UserPermission.API.Application.Common.DTOs;
using UserPermission.API.Application.Common.Interfaces.RepositoryRead;

namespace UserPermission.API.Application.Queries.SyncQueries
{
    public record class InsertPermissionCommand(PermissionDto permissionDto)
        : IRequest;
    public class InsertPermissionCommandHandler : IRequestHandler<InsertPermissionCommand>
    {
        private readonly IRepositoryRead _repositoryRead;
        public InsertPermissionCommandHandler(IRepositoryRead repositoryRead)
        {
            _repositoryRead = repositoryRead;
        }
        public async Task Handle(InsertPermissionCommand request, CancellationToken cancellationToken)
        {
            await _repositoryRead.Insert(request.permissionDto);
        }
    }
}
