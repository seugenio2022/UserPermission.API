using MediatR;
using UserPermission.API.Application.Common.DTOs;
using UserPermission.API.Application.Common.Interfaces.RepositoryRead;
using UserPermission.API.Domain.Events;

namespace UserPermission.API.Application.Queries.GetPermissions
{
    public record GetPermissionsQuery() : IRequest<List<PermissionDto>>;

    public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, List<PermissionDto>>
    {
        private readonly IRepositoryRead _repositoryRead;
        private readonly IMediator _mediator;
        public GetPermissionsQueryHandler(IRepositoryRead repositoryRead, IMediator mediator)
        {
            _repositoryRead = repositoryRead;
            _mediator = mediator;
        }
        public async Task<List<PermissionDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _repositoryRead.GetAll();
            await _mediator.Publish(new PermissionReadEvent());
            return permissions;
        }
    }

}
