using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nest;
using UserPermission.API.Application.Common.DTOs;
using UserPermission.API.Application.Common.Interfaces.RepositoryRead;

namespace UserPermission.API.Infrastructure.Persistence.RepositoryRead
{
    public class RepositoryRead : IRepositoryRead
    {
        private readonly IElasticClient _client;
        private readonly ILogger<RepositoryRead> _logger;
        private readonly string _indexName;

        public RepositoryRead(IElasticClient client, IConfiguration configuration, ILogger<RepositoryRead> logger)
        {
            _logger = logger;
            _indexName = configuration.GetValue<string>("ElasticSearchDefaultIndex");
            _client = client;

            if (!_client.Indices.Exists(_indexName).Exists)
            {
                var response = _client.Indices.Create(_indexName,
                ci => ci
                    .Index(_indexName)
                    .Map<PermissionDto>(m => m
                        .Properties(p => p
                            .Text(x => x.Name(n => n.Id))
                            .Text(x => x.Name(n => n.Name))
                            .Text(x => x.Name(n => n.Description))
                            .Text(x => x.Name(n => n.EmployeeId))
                            .Text(x => x.Name(n => n.PermissionTypeId))
                        )
                    )
                );
                if (!response.IsValid) throw new Exception($"Error create Index: {response.ServerError?.Error?.Reason}");
                _logger.LogInformation($"Index {_indexName} created");
            }
        }
        public async Task<List<PermissionDto>> GetAll()
        {
            var result = await _client.SearchAsync<PermissionDto>(s => s.Query(q => q.MatchAll()));
            return result.Documents.ToList();
        }

        public async Task Insert(PermissionDto permission)
        {
            await _client.CreateAsync(permission, q => q.Index(_indexName));
        }

        public async Task Update(PermissionDto permission)
        {
            await _client.UpdateAsync<PermissionDto>(permission.Id, a => a.Index(_indexName).Doc(permission));
        }
    }
}
