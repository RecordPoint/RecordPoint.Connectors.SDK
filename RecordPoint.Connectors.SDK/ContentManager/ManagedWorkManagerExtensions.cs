using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Work;
using System;
using System.Collections.Generic;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Content job extensions
    /// </summary>
    public static class ManagedWorkManagerExtensions
    {
        /// <summary>
        /// Create a new Content Manager Operation
        /// </summary>
        /// <param name="managedWorkFactory"></param>
        /// <returns>Newly created Content Manager Operation</returns>
        public static IManagedWorkManager CreateContentManagerOperation(this IManagedWorkFactory managedWorkFactory)
        {
            var workStatusId = Guid.NewGuid().ToString();
            var workType = ContentManagerOperation.WORK_TYPE;
            var configuration = new ContentManagerConfiguration();

            return managedWorkFactory.CreateWork(workStatusId, workType, string.Empty, ContentManagerConfiguration.ConfigurationType, configuration.Serialize());
        }

        /// <summary>
        /// Create a new Channel Discovery Operation
        /// </summary>
        /// <param name="managedWorkFactory"></param>
        /// <param name="connectorConfigModel">Connector configuration</param>
        /// <returns>Newly created Channel Discovery Operation</returns>
        public static IManagedWorkManager CreateChannelDiscoveryOperation(this IManagedWorkFactory managedWorkFactory, ConnectorConfigModel connectorConfigModel)
        {
            var workStatusId = Guid.NewGuid().ToString();
            var workType = ChannelDiscoveryOperation.WORK_TYPE;
            var configuration = new ChannelDiscoveryConfiguration
            {
                ConnectorConfigurationId = connectorConfigModel.Id,
                TenantId = connectorConfigModel.TenantId,
                TenantDomainName = connectorConfigModel.TenantDomainName
            };

            return managedWorkFactory.CreateWork(workStatusId, workType, configuration.ConnectorConfigurationId, ChannelDiscoveryConfiguration.ConfigurationType, configuration.Serialize());
        }

        /// <summary>
        /// Create a new Content Registration Operation
        /// </summary>
        /// <param name="managedWorkFactory"></param>
        /// <param name="connectorConfigModel">Connector configuration</param>
        /// <param name="channel">Channel on which to perform the content synchronisation</param>
        /// <param name="context">Custom Context for the Content Registration</param>
        /// <returns>Newly created Content Registration Operation</returns>
        public static IManagedWorkManager CreateContentRegistrationOperation(this IManagedWorkFactory managedWorkFactory, ConnectorConfigModel connectorConfigModel, Channel channel, Dictionary<string, string> context = null)
        {
            var workStatusId = Guid.NewGuid().ToString();
            var workType = ContentRegistrationOperation.WORK_TYPE;
            var configuration = new ContentRegistrationConfiguration
            {
                ConnectorConfigurationId = connectorConfigModel.Id,
                TenantId = connectorConfigModel.TenantId,
                TenantDomainName = connectorConfigModel.TenantDomainName,
                ChannelExternalId = channel.ExternalId,
                ChannelTitle = channel.Title,
                Context = context ?? new Dictionary<string, string>()
            };

            return managedWorkFactory.CreateWork(workStatusId, workType, configuration.ConnectorConfigurationId, ContentRegistrationConfiguration.ConfigurationType, configuration.Serialize());
        }

        /// <summary>
        /// Create a new Content Synchronisation Operation
        /// </summary>
        /// <param name="managedWorkFactory"></param>
        /// <param name="connectorConfigModel">Connector configuration</param>
        /// <param name="channel">Channel on which to perform the content synchronisation</param>
        /// <returns>Newly created Content Synchronisation Operation</returns>
        public static IManagedWorkManager CreateContentSynchronisationOperation(this IManagedWorkFactory managedWorkFactory, ConnectorConfigModel connectorConfigModel, Channel channel)
        {
            var workStatusId = Guid.NewGuid().ToString();
            var workType = ContentSynchronisationOperation.WORK_TYPE;
            var configuration = new ContentSynchronisationConfiguration
            {
                ConnectorConfigurationId = connectorConfigModel.Id,
                TenantId = connectorConfigModel.TenantId,
                TenantDomainName = connectorConfigModel.TenantDomainName,
                ChannelExternalId = channel.ExternalId,
                ChannelTitle = channel.Title
            };

            return managedWorkFactory.CreateWork(workStatusId, workType, configuration.ConnectorConfigurationId, ContentSynchronisationConfiguration.ConfigurationType, configuration.Serialize());
        }


        /// <summary>
        /// Desrialises the Content Manager Configuration from the Managed Work Status
        /// </summary>
        /// <param name="workStatus">Status of the Work Operation</param>
        /// <returns>Configuration for the Channel Discovery</returns>
        public static ContentManagerConfiguration DeserialiseContentManagerConfiguration(this ManagedWorkStatusModel workStatus)
        {
            return ContentManagerConfiguration.Deserialize(workStatus.ConfigurationType, workStatus.Configuration);
        }

        /// <summary>
        /// Desrialises the Channel Discovery Configuration from the Managed Work Status
        /// </summary>
        /// <param name="workStatus">Status of the Work Operation</param>
        /// <returns>Configuration for the Channel Discovery</returns>
        public static ChannelDiscoveryConfiguration DeserialiseChannelDiscoveryConfiguration(this ManagedWorkStatusModel workStatus)
        {
            return ChannelDiscoveryConfiguration.Deserialize(workStatus.ConfigurationType, workStatus.Configuration);
        }

        /// <summary>
        /// Desrialises the Content Synchronisation Configuration from the Managed Work Status
        /// </summary>
        /// <param name="workStatus">Status of the Work Operation</param>
        /// <returns>Configuration for the Content Synchronisation</returns>
        public static ContentSynchronisationConfiguration DeserialiseContentSynchronisationConfiguration(this ManagedWorkStatusModel workStatus)
        {
            return ContentSynchronisationConfiguration.Deserialize(workStatus.ConfigurationType, workStatus.Configuration);
        }

        /// <summary>
        /// Desrialises the Content Registration Configuration from the Managed Work Status
        /// </summary>
        /// <param name="workStatus">Status of the Work Operation</param>
        /// <returns>Configuration for the Content Registration</returns>
        public static ContentRegistrationConfiguration DeserialiseContentRegistrationConfiguration(this ManagedWorkStatusModel workStatus)
        {
            return ContentRegistrationConfiguration.Deserialize(workStatus.ConfigurationType, workStatus.Configuration);
        }
    }
}
