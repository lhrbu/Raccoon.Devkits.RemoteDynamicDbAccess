using System;

namespace Raccoon.Devkits.RemoteDynamicDbAccess
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RemotePropertyAttribute:Attribute
    {
        public Type DynamicDbAccessServiceType {get;}
        public string? RemotePropertyName {get;}
        public RemotePropertyAttribute(Type dynamicDbAccessServiceType,string? remotePropertyName=null)
        {
            DynamicDbAccessServiceType = dynamicDbAccessServiceType;
            RemotePropertyName = remotePropertyName;
        }
    }
}