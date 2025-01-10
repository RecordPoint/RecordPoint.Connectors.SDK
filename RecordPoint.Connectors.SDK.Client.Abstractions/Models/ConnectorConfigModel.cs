namespace RecordPoint.Connectors.SDK.Client.Models
{
    /// <summary>
    /// The connector config model.
    /// </summary>
    public partial class ConnectorConfigModel
    {
        /// <summary>
        /// The id as guid.
        /// </summary>
        private Lazy<Guid> _idAsGuid;
        /// <summary>
        /// The tenant id as guid.
        /// </summary>
        private Lazy<Guid> _tenantIdAsGuid;
        /// <summary>
        /// The connector type id as guid.
        /// </summary>
        private Lazy<Guid> _connectorTypeIdAsGuid;

        private const string AD_TENANT_ID = "AzureAdTenantId";
        private const string Enabled = "Enabled";
        private const string DisabledTime = "DisabledTime";

        /// <summary>
        /// Customs the init.
        /// </summary>
        partial void CustomInit()
        {
            _idAsGuid = new Lazy<Guid>(() =>
            {
                if (string.IsNullOrEmpty(Id))
                    return Guid.Empty;
                return Guid.Parse(Id);
            });

            _tenantIdAsGuid = new Lazy<Guid>(() =>
            {
                if (string.IsNullOrEmpty(TenantId))
                    return Guid.Empty;
                return Guid.Parse(TenantId);
            });

            _connectorTypeIdAsGuid = new Lazy<Guid>(() =>
            {
                if (string.IsNullOrEmpty(ConnectorTypeId))
                    return Guid.Empty;
                return Guid.Parse(ConnectorTypeId);
            });
        }

        /// <summary>
        /// Gets the ID of this Connector instance as a GUID.
        /// Assumes that the Id field never changes.
        /// </summary>
        public Guid GetIdAsGuid()
        {
            if (_idAsGuid == null) throw new RequiredValueNullException(nameof(_idAsGuid));
            return _idAsGuid.Value;
        }


        /// <summary>
        /// Gets the ID of the Records365 vNext tenant that owns this
        /// connector instance as a GUID.
        /// Assumes that the TenantId field never changes.
        /// </summary>
        public Guid GetTenantIdAsGuid()
        {
            if (_tenantIdAsGuid == null) throw new RequiredValueNullException(nameof(_tenantIdAsGuid));
            return _tenantIdAsGuid.Value;
        }

        /// <summary>
        /// Get AD tenant id.
        /// </summary>
        /// <returns>A string</returns>
        public string GetADTenantId()
        {
            return Properties?.GetValueOrDefault(AD_TENANT_ID) ?? string.Empty;
        }

        /// <summary>
        /// Gets the ID of the ConnectorType for this instance as a GUID.
        /// Assumes that the ConnectorTypeId field never changes.
        /// </summary>
        public Guid? ConnectorTypeIdAsGuid()
        {
            if (_connectorTypeIdAsGuid == null) throw new RequiredValueNullException(nameof(_connectorTypeIdAsGuid));
            return _connectorTypeIdAsGuid.Value;
        }

        /// <summary>
        /// Set the property.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        /// <returns>A ConnectorConfigModel</returns>
        public ConnectorConfigModel SetProperty(string name, string value, string type = nameof(String))
        {
            if (Properties == null)
            {
                Properties = new List<MetaDataModel>();
            }
            Properties.AddOrUpdate(name, type, value);
            return this;
        }

        /// <summary>
        /// Set the property.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>A ConnectorConfigModel</returns>
        public ConnectorConfigModel SetProperty(string name, object value)
        {
            if (Properties == null)
            {
                Properties = new List<MetaDataModel>();
            }
            Properties.AddOrUpdate(name, value?.ToString());
            return this;
        }

        /// <summary>
        /// Get property or default.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A string</returns>
        public string GetPropertyOrDefault(string name)
        {
            return Properties?.GetValueOrDefault(name);
        }

        /// <summary>
        /// Is the Connector Configuration currently enabled?
        /// </summary>
        /// <returns></returns>
        public bool IsEnabled()
        {
            return Status.Equals(Enabled, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Is or has the Connector ever been enabled?
        /// </summary>
        /// <returns>Boolean</returns>
        public bool HasBeenEnabled()
        {
            return Status.Equals(Enabled, StringComparison.InvariantCultureIgnoreCase) || (bool.TryParse(EnabledHistory, out bool isEnabled) && isEnabled);
        }

        /// <summary>
        /// Checks if the Connector is Disabled and if it has been disabled for more than the specified time.
        /// If the Configuration does not have a DisabledTime property, it is considered expired.
        /// If the maxDisabledConnectorAge is less than 0, the Configuration is not considered expired.
        /// </summary>
        /// <param name="maxDisabledConnectorAge">The maximum time in seconds a configuration can be disabled before it should be abandoned</param>
        /// <returns></returns>
        public bool IsDisabledConnectorExpired(int maxDisabledConnectorAge)
        {
            if (IsEnabled() || maxDisabledConnectorAge < 0)
                return false;
            
            return !DateTimeOffset.TryParse(GetPropertyOrDefault(DisabledTime), out var disabledDate) || DateTimeOffset.Now > disabledDate.AddSeconds(maxDisabledConnectorAge);
        }

    }
}
