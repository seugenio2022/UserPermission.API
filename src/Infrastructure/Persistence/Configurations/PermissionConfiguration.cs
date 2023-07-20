using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserPermission.API.Domain.Entities;

namespace UserPermission.API.Infrastructure.Persistence.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasOne(p => p.PermissionType)
                .WithMany(pt => pt.Permissions)
                .HasForeignKey(p => p.PermissionTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
