using System;
using Raccoon.Devkits.RemoteDynamicDbAccess;

namespace Raccoon.Devikits.RemoteDynamicDbAccess.UnitTest
{
    public class RefinedProject
    {
        public Guid Id {get;set;} = Guid.NewGuid();
        public string Payload {get;set;} = "Hello Remote Db Accesss";

        [RemoteKey(typeof(UsageRecordDynamicDbAccessService))]
        public string no {get;set;}=null!;

        [RemoteProperty(typeof(UsageRecordDynamicDbAccessService),"name")]
        public string Name{get;set;} = null!;
        [RemoteProperty(typeof(UsageRecordDynamicDbAccessService))]
        public string name_in_fin {get;set;} = null!;
    }
}