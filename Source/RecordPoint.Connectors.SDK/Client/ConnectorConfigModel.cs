using System;
using System.Collections.Generic;

namespace RecordPoint.Connectors.SDK.Client.Models
{
    public partial class ConnectorConfigModel
    {
        private Lazy<Guid> _idAsGuid;
        private Lazy<Guid> _tenantIdAsGuid;
        private Lazy<Guid> _connectorTypeIdAsGuid;

        partial void CustomInit()
        {
            _idAsGuid = new Lazy<Guid>(() =>
            {
                if (String.IsNullOrEmpty(Id))
                    return Guid.Empty;
                return Guid.Parse(Id);
            });

            _tenantIdAsGuid = new Lazy<Guid>(() =>
            {
                if (String.IsNullOrEmpty(TenantId))
                    return Guid.Empty;
                return Guid.Parse(TenantId);
            });

            _connectorTypeIdAsGuid = new Lazy<Guid>(() =>
            {
                if (String.IsNullOrEmpty(ConnectorTypeId))
                    return Guid.Empty;
                return Guid.Parse(ConnectorTypeId);
            });
        }

        /// <summary>
        /// Gets the ID of this Connector instance as a GUID.
        /// Assumes that the Id field never changes.
        /// </summary>
        public Guid IdAsGuid
        {
            get
            {
                return _idAsGuid.Value;
            }
        }

        /// <summary>
        /// Gets the ID of the Records365 vNext tenant that owns this
        /// connector instance as a GUID.
        /// Assumes that the TenantId field never changes.
        /// </summary>
        public Guid TenantIdAsGuid
        {
            get
            {
                return _tenantIdAsGuid.Value;
            }
        }

        /// <summary>
        /// Gets the ID of the ConnectorType for this instance as a GUID.
        /// Assumes that the ConnectorTypeId field never changes.
        /// </summary>
        public Guid ConnectorTypeIdAsGuid
        {
            get
            {
                return _connectorTypeIdAsGuid.Value;
            }
        }
        
        public ConnectorConfigModel SetProperty(string name, string value, string type = nameof(String))
        {
            if (Properties == null)
            {
                Properties = new List<MetaDataModel>();
            }
            Properties.AddOrUpdate(name, type, value);
            return this;
        }

        public ConnectorConfigModel SetProperty(string name, object value)
        {
            if (Properties == null)
            {
                Properties = new List<MetaDataModel>();
            }
            Properties.AddOrUpdate(name, value?.ToString());
            return this;
        }

        public string GetPropertyOrDefault(string name)
        {
            return Properties?.GetValueOrDefault(name);
        }
    }
}
