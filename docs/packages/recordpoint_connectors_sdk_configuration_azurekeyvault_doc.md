<a name='assembly'></a>
# RecordPoint.Connectors.SDK.Configuration.AzureKeyVault

## Contents

- [AzureKeyVaultConfigurationBuilderExtensions](#T-RecordPoint-Connectors-SDK-Configuration-AzureKeyVault-AzureKeyVaultConfigurationBuilderExtensions 'RecordPoint.Connectors.SDK.Configuration.AzureKeyVault.AzureKeyVaultConfigurationBuilderExtensions')
  - [UseAzureKeyVaultConfigurationProvider(configuration,rootConfigSectionName)](#M-RecordPoint-Connectors-SDK-Configuration-AzureKeyVault-AzureKeyVaultConfigurationBuilderExtensions-UseAzureKeyVaultConfigurationProvider-Microsoft-Extensions-Configuration-IConfigurationBuilder,System-String- 'RecordPoint.Connectors.SDK.Configuration.AzureKeyVault.AzureKeyVaultConfigurationBuilderExtensions.UseAzureKeyVaultConfigurationProvider(Microsoft.Extensions.Configuration.IConfigurationBuilder,System.String)')
- [AzureKeyVaultOptions](#T-RecordPoint-Connectors-SDK-Configuration-AzureKeyVault-AzureKeyVaultOptions 'RecordPoint.Connectors.SDK.Configuration.AzureKeyVault.AzureKeyVaultOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-Configuration-AzureKeyVault-AzureKeyVaultOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.Configuration.AzureKeyVault.AzureKeyVaultOptions.SECTION_NAME')
  - [KeyVaultName](#P-RecordPoint-Connectors-SDK-Configuration-AzureKeyVault-AzureKeyVaultOptions-KeyVaultName 'RecordPoint.Connectors.SDK.Configuration.AzureKeyVault.AzureKeyVaultOptions.KeyVaultName')
  - [ReloadInterval](#P-RecordPoint-Connectors-SDK-Configuration-AzureKeyVault-AzureKeyVaultOptions-ReloadInterval 'RecordPoint.Connectors.SDK.Configuration.AzureKeyVault.AzureKeyVaultOptions.ReloadInterval')

<a name='T-RecordPoint-Connectors-SDK-Configuration-AzureKeyVault-AzureKeyVaultConfigurationBuilderExtensions'></a>
## AzureKeyVaultConfigurationBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Configuration.AzureKeyVault

##### Summary

Azure key vault builder extensions

<a name='M-RecordPoint-Connectors-SDK-Configuration-AzureKeyVault-AzureKeyVaultConfigurationBuilderExtensions-UseAzureKeyVaultConfigurationProvider-Microsoft-Extensions-Configuration-IConfigurationBuilder,System-String-'></a>
### UseAzureKeyVaultConfigurationProvider(configuration,rootConfigSectionName) `method`

##### Summary

Use Azure Key Vault
The KeyVaultName setting must be populated with the name of the keyvault.
When no other settings are provided, the connection will be attempted using the DefaultAzureCredential.
If the ClientId and ClientSecret settings are populated, the connection will be attempted using the ClientSecretCredential.
If the UseVSCredentials is True, the connection will be attempted using the VisualStudioCredential.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| configuration | [Microsoft.Extensions.Configuration.IConfigurationBuilder](#T-Microsoft-Extensions-Configuration-IConfigurationBuilder 'Microsoft.Extensions.Configuration.IConfigurationBuilder') | Configuration builder to target |
| rootConfigSectionName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The (optional) root section name of the configuration hierarchy to obtain the KeyVault configuration |

<a name='T-RecordPoint-Connectors-SDK-Configuration-AzureKeyVault-AzureKeyVaultOptions'></a>
## AzureKeyVaultOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Configuration.AzureKeyVault

##### Summary

Azure key vault options

<a name='F-RecordPoint-Connectors-SDK-Configuration-AzureKeyVault-AzureKeyVaultOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary

Configuration section name

<a name='P-RecordPoint-Connectors-SDK-Configuration-AzureKeyVault-AzureKeyVaultOptions-KeyVaultName'></a>
### KeyVaultName `property`

##### Summary

Key vault name

<a name='P-RecordPoint-Connectors-SDK-Configuration-AzureKeyVault-AzureKeyVaultOptions-ReloadInterval'></a>
### ReloadInterval `property`

##### Summary

Time in seconds to wait between attempts at polling the Azure Key Vault for changes.

##### Remarks

Reloading cannot be disabled. If a value of 0 is provided, the timeout will default to 5 mins.
