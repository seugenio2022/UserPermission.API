using FluentAssertions;
using UserPermission.API.Application.Common.DTOs;
using UserPermission.API.Application.Queries.GetPermissions;
using Xunit;

namespace UserPermission.API.Application.IntegrationTests.Queries
{
    [Collection("Sequential")]
    public class GetPermissionsShould : BaseTest
    {

        public GetPermissionsShould(TestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public void Get_Permissions()
        {

            //ARRANGE
            var permissionInserted = new PermissionDto(Guid.NewGuid(), "permission1", "description1", Guid.NewGuid(), Guid.NewGuid());
            InsertToReadRepository(permissionInserted);

            //ACT
            var sut = new GetPermissionsQuery();

            var permissionsResult = SendAsync(sut).GetAwaiter().GetResult();

            //ASSERT

            foreach (var permission in permissionsResult)
            {
                permission.Id.Should().Be(permissionInserted.Id);
                permission.Name.Should().Be(permissionInserted.Name);
                permission.Description.Should().Be(permissionInserted.Description);
                permission.EmployeeId.Should().Be(permissionInserted.EmployeeId);
                permission.PermissionTypeId.Should().Be(permissionInserted.PermissionTypeId);
            }
        }
    }
}
