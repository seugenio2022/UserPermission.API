using Confluent.Kafka;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Newtonsoft.Json;
using Respawn;
using UserPermission.API.Application.Common.DTOs;
using UserPermission.API.Application.Common.Interfaces.RepositoryRead;
using UserPermission.API.Application.Queries.SyncQueries;
using Xunit;

namespace UserPermission.API.Application.IntegrationTests;

public class BaseTest : IClassFixture<TestWebAppFactory>
{
    private static WebApplicationFactory<Program> _factory = null!;
    private static IConfiguration _configuration = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static IElasticClient _elasticClient = null!;
    private static Respawner _checkpoint = null!;
    public BaseTest(TestWebAppFactory factory)
    {
        _factory = factory;
        RunBeforeAnyTests();
        ResetState().GetAwaiter().GetResult();
    }
    public void RunBeforeAnyTests()
    {
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();
        _elasticClient = _factory.Services.GetRequiredService<IElasticClient>();

        _checkpoint = Respawner.CreateAsync(_configuration.GetConnectionString("DefaultConnection")!, new RespawnerOptions
        {
            TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
        }).GetAwaiter().GetResult();

    }

    public static async Task<TResponse> SendAsync<TResponse>(MediatR.IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    public static async Task ResetState()
    {
        await _checkpoint.ResetAsync(_configuration.GetConnectionString("DefaultConnection")!);
        DeleteAllDocumentsInIndex();

    }

    public static async Task<TEntity?> FindByIdAsync<TEntity>(Guid id)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.UserPermissionDbContext>();

        return await context.FindAsync<TEntity>(id);
    }

    public static async Task<TEntity> AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<Infrastructure.Persistence.UserPermissionDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();

        return entity;
    }

    public static void InsertToReadRepository(PermissionDto entity)
    {
        using var scope = _scopeFactory.CreateScope();

        var repo = scope.ServiceProvider.GetRequiredService<IRepositoryRead>();

        repo.Insert(entity);
    }

    public static void DeleteAllDocumentsInIndex()
    {

        var indexName = _configuration.GetValue<string>("ElasticSearchDefaultIndex");

        var deleteResponse = _elasticClient.DeleteByQuery<object>(d => d
            .Index(indexName)
            .Query(q => q.MatchAll())
        );
    }

}
