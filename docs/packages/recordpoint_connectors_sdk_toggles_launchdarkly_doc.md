<a name='assembly'></a>
# RecordPoint.Connectors.SDK.Toggles.LaunchDarkly

## Contents

- [FeatureToggle](#T-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-FeatureToggle 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.FeatureToggle')
  - [#ctor(toggleKey)](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-FeatureToggle-#ctor-System-String- 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.FeatureToggle.#ctor(System.String)')
  - [ToString()](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-FeatureToggle-ToString 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.FeatureToggle.ToString')
- [LaunchDarklyBuilderExtensions](#T-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyBuilderExtensions 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyBuilderExtensions')
  - [UseLaunchDarklyToggles(hostBuilder)](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyBuilderExtensions-UseLaunchDarklyToggles-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyBuilderExtensions.UseLaunchDarklyToggles(Microsoft.Extensions.Hosting.IHostBuilder)')
- [LaunchDarklyToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyToggleProvider')
  - [#ctor(launchDarklyFeatureToggleProvider)](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-#ctor-RecordPoint-Services-Common-FeatureToggles-IFeatureToggleProvider- 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyToggleProvider.#ctor(RecordPoint.Services.Common.FeatureToggles.IFeatureToggleProvider)')
  - [_launchDarklyFeatureToggleProvider](#F-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-_launchDarklyFeatureToggleProvider 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyToggleProvider._launchDarklyFeatureToggleProvider')
  - [GetToggleBool(toggle,default)](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleBool-System-String,System-Boolean- 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyToggleProvider.GetToggleBool(System.String,System.Boolean)')
  - [GetToggleBool(toggle,userKey,default)](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleBool-System-String,System-String,System-Boolean- 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyToggleProvider.GetToggleBool(System.String,System.String,System.Boolean)')
  - [GetToggleNumber()](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleNumber-System-String,System-String,System-Int32- 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyToggleProvider.GetToggleNumber(System.String,System.String,System.Int32)')
  - [GetToggleNumber()](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleNumber-System-String,System-Int32- 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyToggleProvider.GetToggleNumber(System.String,System.Int32)')
  - [GetToggleString()](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleString-System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyToggleProvider.GetToggleString(System.String,System.String,System.String)')
  - [GetToggleString()](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleString-System-String,System-String- 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyToggleProvider.GetToggleString(System.String,System.String)')

<a name='T-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-FeatureToggle'></a>
## FeatureToggle `type`

##### Namespace

RecordPoint.Connectors.SDK.Toggles.LaunchDarkly

##### Summary

Launch darkly feature toggle class for use with the connector sdk

<a name='M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-FeatureToggle-#ctor-System-String-'></a>
### #ctor(toggleKey) `constructor`

##### Summary

sets toggle key

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggleKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-FeatureToggle-ToString'></a>
### ToString() `method`

##### Summary

Overrides to string method for feature toggle class

##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyBuilderExtensions'></a>
## LaunchDarklyBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Toggles.LaunchDarkly

##### Summary

Launch darkly host builder extensions

<a name='M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyBuilderExtensions-UseLaunchDarklyToggles-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseLaunchDarklyToggles(hostBuilder) `method`

##### Summary

Configure the host to use the launch darkly toggle provider

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to update |

<a name='T-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider'></a>
## LaunchDarklyToggleProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Toggles.LaunchDarkly

##### Summary

The launch darkly toggle provider.

<a name='M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-#ctor-RecordPoint-Services-Common-FeatureToggles-IFeatureToggleProvider-'></a>
### #ctor(launchDarklyFeatureToggleProvider) `constructor`

##### Summary

Launch darkly feature toggle provider class for use with the connector sdk

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| launchDarklyFeatureToggleProvider | [RecordPoint.Services.Common.FeatureToggles.IFeatureToggleProvider](#T-RecordPoint-Services-Common-FeatureToggles-IFeatureToggleProvider 'RecordPoint.Services.Common.FeatureToggles.IFeatureToggleProvider') |  |

<a name='F-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-_launchDarklyFeatureToggleProvider'></a>
### _launchDarklyFeatureToggleProvider `constants`

##### Summary

Launch darkly feature toggle provider.

<a name='M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleBool-System-String,System-Boolean-'></a>
### GetToggleBool(toggle,default) `method`

##### Summary

Get toggle value for given toggle (non tenanted)

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| default | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |

<a name='M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleBool-System-String,System-String,System-Boolean-'></a>
### GetToggleBool(toggle,userKey,default) `method`

##### Summary

Get a tenanted toggle value for given toggle

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| toggle | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| userKey | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| default | [System.Boolean](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Boolean 'System.Boolean') |  |

<a name='M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleNumber-System-String,System-String,System-Int32-'></a>
### GetToggleNumber() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleNumber-System-String,System-Int32-'></a>
### GetToggleNumber() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleString-System-String,System-String,System-String-'></a>
### GetToggleString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleString-System-String,System-String-'></a>
### GetToggleString() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.
