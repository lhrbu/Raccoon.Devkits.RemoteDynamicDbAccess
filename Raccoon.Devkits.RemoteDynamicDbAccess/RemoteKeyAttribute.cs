using System;

namespace Raccoon.Devkits.RemoteDynamicDbAccess
{
     [AttributeUsage(AttributeTargets.Property)]
    public class RemoteKeyAttribute:Attribute
    {
        public Type DynamicDbAccessServiceType {get;}

        public RemoteKeyAttribute(Type dynamicDbAccessServiceType)
        {
            DynamicDbAccessServiceType = dynamicDbAccessServiceType;
        }
    }
}