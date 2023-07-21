using FluentAssertions;
using Moq;
using UserPermission.API.Domain.Entities;
using UserPermission.API.Domain.Exceptions;
using UserPermission.API.Domain.Interfaces;
using Xunit;

namespace UserPermission.API.Domain.UnitTests.Entities
{
    public class PermissionShould
    {
        [Fact]
        public async Task Return_Correct_Permission()
        {
            //ARRANGE

            string name = "Name1";
            string description = "Des1";
            Guid employeeId = Guid.NewGuid();
            Guid permissionTypeId = Guid.NewGuid();

            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            employeeRepositoryMock.Setup(repo => repo.GetAsync(employeeId)).ReturnsAsync(new Employee());

            var permissionTypeRepositoryMock = new Mock<IPermissionTypeRepository>();
            permissionTypeRepositoryMock.Setup(repo => repo.GetAsync(permissionTypeId)).ReturnsAsync(new PermissionType());

            //ACT
            var permissionCreated = await Permission.CreateAsync(
                name,
                description,
                employeeId,
                permissionTypeId,
                employeeRepositoryMock.Object,
                permissionTypeRepositoryMock.Object);

            //ASSERT
            permissionCreated?.Id.Should().NotBeEmpty();
            permissionCreated?.Name.Should().Be(name);
            permissionCreated?.Description.Should().Be(description);
            permissionCreated?.EmployeeId.Should().Be(employeeId);
            permissionCreated?.PermissionTypeId.Should().Be(permissionTypeId);
        }

        [Fact]
        public async Task Throw_NotFound_EmployeeId_Exception()
        {
            //ARRANGE

            string name = "Name1";
            string description = "Des1";
            var invalidEmployeeId = Guid.NewGuid();
            Guid permissionTypeId = Guid.NewGuid();


            var employeeRepositoryMock = new Mock<IEmployeeRepository>();

            var permissionTypeRepositoryMock = new Mock<IPermissionTypeRepository>();
            permissionTypeRepositoryMock.Setup(repo => repo.GetAsync(permissionTypeId)).ReturnsAsync(new PermissionType());

            //ACT AND ASSERT

            await FluentActions.Invoking(() => Permission.CreateAsync(
                name,
                description,
                invalidEmployeeId,
                permissionTypeId,
                employeeRepositoryMock.Object,
                permissionTypeRepositoryMock.Object)).Should().ThrowAsync<EmployeeNotFoundException>();
        }
    }
}
