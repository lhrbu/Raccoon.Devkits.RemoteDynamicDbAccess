using Raccoon.Devkits.DynamicDbAccess;
using Raccoon.Devkits.DynamicDbAccess.ConnectionPool;

namespace Raccoon.Devikits.RemoteDynamicDbAccess.UnitTest
{
    public class UsageRecordDynamicDbAccessService:DynamicDbAccessService
    {
        public UsageRecordDynamicDbAccessService(DbConnectionPool<UsageRecordDynamicDbAccessService> pool)
            :base(pool){}
    }
}