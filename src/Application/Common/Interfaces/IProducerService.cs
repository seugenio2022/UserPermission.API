using UserPermission.API.Application.Common.DTOs;

namespace UserPermission.API.Application.Common.Interfaces
{

    public interface IProducerService
    {
        Task PublishAsync<T>(PublishInputDto<T> dto);
    }
}
