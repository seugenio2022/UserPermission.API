using FluentAssertions;
using UserPermission.API.Application.Commands.ModifyPermission;
using UserPermission.API.Domain.Entities;
using UserPermission.API.Domain.ValueObjects;
using Xunit;

namespace UserPermission.API.Application.IntegrationTests.Commands
{

    [Collection("Sequential")]
    public class ModifyPermissionShould : BaseTest
    {
        public ModifyPermissionShould(TestWebAppFactory factory):base(factory) { }

        [Fact]
        public async Task Modify_Permission()
        {
            //ARRANGE
            var employeeCreated = await AddAsync(new Domain.Entities.Employee
            {
                Name = "Employee",
                Email = new Email("Employee1@gmail.com")
            });

            var permissionTypeCreated = await AddAsync(new Domain.Entities.PermissionType
            {
                Name = "Document Access"
            });  
            
            var permissionCreated = await AddPermissionAsync("Name", "Description", employeeCreated.Id, permissionTypeCreated.Id);

            var sut = new ModifyPermissionCommand(permissionCreated.Id, "Name Modified", "Description Modified", employeeCreated.Id, permissionTypeCreated.Id);

            //ACT

            await SendAsync(sut);

            //ASSERT

            var permissionModified = await FindByIdAsync<Permission>(permissionCreated.Id);

            permissionModified.Should().NotBeNull();
            permissionModified.Name.Should().Be(sut.Name);
            permissionModified.Description.Should().Be(sut.Description);
            permissionModified.EmployeeId.Should().Be(sut.EmployeeId);
            permissionModified.PermissionTypeId.Should().Be(sut.PermissionTypeId);

        }
    }
}
