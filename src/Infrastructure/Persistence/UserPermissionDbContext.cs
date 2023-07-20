using System.Reflection;
using UserPermission.API.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserPermission.API.Infrastructure.Common;

namespace UserPermission.API.Infrastructure.Persistence;

public class UserPermissionDbContext :DbContext
{
    private readonly IMediator _mediator;

    public UserPermissionDbContext(
        DbContextOptions<UserPermissionDbContext> options,
        IMediator mediator) 
        : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<PermissionType> PermissionTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = await base.SaveChangesAsync(cancellationToken);
        
        await _mediator.DispatchDomainEvents(this);

        return entries;
    }
}
