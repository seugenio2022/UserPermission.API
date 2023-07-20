using FluentAssertions;
using UserPermission.API.Application.Commands.RequestPermission;
using UserPermission.API.Domain.Entities;
using Xunit;

namespace UserPermission.API.Application.IntegrationTests.Commands
{

    [Collection("Sequential")]
    public class RequestPermissionShould : BaseTest
    {
        public RequestPermissionShould(TestWebAppFactory factory):base(factory) { }

        [Fact]
        public void Create_Permission()
        {
            //ARRANGE
            var employeeCreated = AddAsync(new Domain.Entities.Employee
            {
                Name = "Employee",
                Email = new("Employee1@gmail.com")
            }).Result;

            var permissionTypeCreated = AddAsync(new Domain.Entities.PermissionType
            {
                Name = "Document Access"
            }).Result;

            var sut = new RequestPermissionCommand("Name", "Description", employeeCreated.Id, permissionTypeCreated.Id);

            //ACT

            var permissionCreatedId = SendAsync(sut).Result;

            //ASSERT
            var permissionCreated = FindByIdAsync<Permission>(permissionCreatedId).Result;

            permissionCreated.Should().NotBeNull();
            permissionCreated.Name.Should().Be(sut.Name);
            permissionCreated.Description.Should().Be(sut.Description);
            permissionCreated.EmployeeId.Should().Be(sut.EmployeeId);
            permissionCreated.PermissionTypeId.Should().Be(sut.PermissionTypeId);
        }
    }
}
