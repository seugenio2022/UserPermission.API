using FluentValidation;

namespace UserPermission.API.Application.Commands.ModifyPermission
{
    public class ModifyPermissionValidator : AbstractValidator<ModifyPermissionCommand>
    {
        public ModifyPermissionValidator()
        {
            RuleFor(v => v.Id).NotEmpty().NotNull();
            RuleFor(v => v.EmployeeId).NotEmpty().NotNull(); 
            RuleFor(v => v.PermissionTypeId).NotEmpty().NotNull();
            RuleFor(v => v.Name).NotEmpty().NotNull();
            RuleFor(v => v.Description).NotEmpty().NotNull();
        }
    }
}
