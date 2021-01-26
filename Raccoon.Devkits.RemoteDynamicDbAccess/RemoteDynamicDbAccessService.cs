using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Raccoon.Devkits.DynamicDbAccess;

namespace Raccoon.Devkits.RemoteDynamicDbAccess
{
    public class RemoteDynamicDbAccessService
    {
        private readonly IServiceProvider _serviceProvider;
        public RemoteDynamicDbAccessService(IServiceProvider serviceProvider)=>_serviceProvider = serviceProvider;
        
        public async ValueTask<TEntity> RefineEntity<TEntity>(TEntity entity,EntityType remoteEntityType)
        {
            PropertyInfo[] propertyInfos = typeof(TEntity).GetProperties();
            var remotePropertyGroups = propertyInfos
                .Where(property=>property.GetCustomAttribute<RemotePropertyAttribute>() is not null)
                .GroupBy(property=>property.GetCustomAttribute<RemotePropertyAttribute>()!.DynamicDbAccessServiceType);
            
            var remoteKeyPropertyInfos = propertyInfos
                .Where(property=>property.GetCustomAttribute<RemoteKeyAttribute>() is not null);

            foreach(var remotePropertyGroup in remotePropertyGroups)
            {
                DynamicDbAccessService? dbAccessService = (_serviceProvider.GetRequiredService(remotePropertyGroup.Key) as DynamicDbAccessService);
                if(dbAccessService is null){
                    throw new ArgumentException($"{remotePropertyGroup.Key} in {typeof(TEntity)} is not resolved.",nameof(entity));
                }
                PropertyInfo? keyPropertyInfo = remoteKeyPropertyInfos.FirstOrDefault(item=>
                    item.GetCustomAttribute<RemoteKeyAttribute>()!.DynamicDbAccessServiceType == remotePropertyGroup.Key);
                if(keyPropertyInfo is null){
                    throw new ArgumentException($"Remote key for {remotePropertyGroup.Key} is not defined in {typeof(TEntity)}.",nameof(entity));
                }
                dynamic? id = keyPropertyInfo.GetValue(entity);
                object remoteEntity = await dbAccessService.GetByIdAsync(id,remoteEntityType);
                Type remoteType = remoteEntity.GetType();

                foreach(PropertyInfo remoteProperty in remotePropertyGroup)
                {
                    string remotePropertyName = remoteProperty.GetCustomAttribute<RemotePropertyAttribute>()?.RemotePropertyName ??
                        remoteProperty.Name;
                    remoteProperty.SetValue(entity,remoteType.GetProperty(remotePropertyName)!.GetValue(remoteEntity));
                }
            }
            return entity;
            
        } 
    }
}