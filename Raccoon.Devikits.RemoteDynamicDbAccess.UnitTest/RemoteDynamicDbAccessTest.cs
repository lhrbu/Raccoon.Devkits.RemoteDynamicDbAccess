using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Raccoon.Devkits.DynamicDbAccess;
using Raccoon.Devkits.RemoteDynamicDbAccess;
using Xunit;

namespace Raccoon.Devikits.RemoteDynamicDbAccess.UnitTest
{
    public class RemoteDynamicDbAccessTest
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly EntityType _entityType = new("EntityTypes/Project.dll","LabCMS.TestReportDomain.EntityAssemblySample","Project");
        public RemoteDynamicDbAccessTest()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDynamicAccessService<UsageRecordDynamicDbAccessService>(
                "Host=localhost;Database=usage_records;",
                item=>new NpgsqlConnection(item)
            );
            services.AddTransient<RemoteDynamicDbAccessService>();
            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public async Task TestRemoteDbAccess()
        {
            RemoteDynamicDbAccessService remoteDynamicDbAccess = _serviceProvider.GetRequiredService<RemoteDynamicDbAccessService>();
            RefinedProject project = new(){no="807c6f68-2ca6-4b7d-bf46-97d42166b386"};
            var updated = await remoteDynamicDbAccess.RefineEntity(project,_entityType);
            Assert.NotNull(updated.Name);
            Assert.NotNull(updated.name_in_fin);
        }
    }
}