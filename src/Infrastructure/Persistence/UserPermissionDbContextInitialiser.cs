using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace UserPermission.API.Infrastructure.Persistence;

public class UserPermissionDbContextInitialiser
{
    private readonly ILogger<UserPermissionDbContextInitialiser> _logger;
    private readonly UserPermissionDbContext _context;

    public UserPermissionDbContextInitialiser(ILogger<UserPermissionDbContextInitialiser> logger, UserPermissionDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (!_context.Employees.Any())
        {
            _context.Employees.Add(new Domain.Entities.Employee
            {
                Name = "Employee",
                Email = new("Employee1@gmail.com")             
            });

            await _context.SaveChangesAsync();
        }
        if (!_context.PermissionTypes.Any())
        {
            _context.PermissionTypes.AddRange(new Domain.Entities.PermissionType
            {
                Name = "Document Access"
            },
            new Domain.Entities.PermissionType
            {
                Name = "Document Editing"
            });

            await _context.SaveChangesAsync();
        }
    }
}
