using FluentValidation;

namespace UserPermission.API.Application.Commands.RequestPermission
{
    public class RequestPermissionValidator : AbstractValidator<RequestPermissionCommand>
    {
        public RequestPermissionValidator()
        {
            RuleFor(v => v.EmployeeId).NotEmpty().NotNull();
            RuleFor(v => v.PermissionTypeId).NotEmpty().NotNull();
            RuleFor(v => v.Name).NotEmpty().NotNull();
            RuleFor(v => v.Description).NotEmpty().NotNull();
        }
    }
}
