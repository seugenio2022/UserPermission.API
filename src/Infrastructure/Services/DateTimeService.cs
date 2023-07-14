using UserPermission.API.Application.Common.Interfaces;

namespace UserPermission.API.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
