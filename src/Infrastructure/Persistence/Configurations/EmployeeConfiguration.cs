using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserPermission.API.Domain.Entities;

namespace UserPermission.API.Infrastructure.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasMany(e => e.Permissions)
                .WithOne(p => p.Employee)
                .HasForeignKey(p => p.EmployeeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
