<a name='assembly'></a>
# RecordPoint.Connectors.SDK.Toggles.LaunchDarkly

## Contents

- [LaunchDarklyBuilderExtensions](#T-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyBuilderExtensions 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyBuilderExtensions')
  - [UseLaunchDarklyToggles(hostBuilder)](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyBuilderExtensions-UseLaunchDarklyToggles-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyBuilderExtensions.UseLaunchDarklyToggles(Microsoft.Extensions.Hosting.IHostBuilder)')
- [LaunchDarklyOptions](#T-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyOptions 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyOptions.SECTION_NAME')
  - [DefaultUserKey](#P-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyOptions-DefaultUserKey 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyOptions.DefaultUserKey')
  - [SdkKey](#P-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyOptions-SdkKey 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyOptions.SdkKey')
- [LaunchDarklyToggleProvider](#T-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyToggleProvider')
  - [#ctor(options)](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-#ctor-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyOptions}- 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyToggleProvider.#ctor(Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyOptions})')
  - [GetToggleBool(toggle,default)](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleBool-System-String,System-Boolean- 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyToggleProvider.GetToggleBool(System.String,System.Boolean)')
  - [GetToggleBool(toggle,userKey,default)](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleBool-System-String,System-String,System-Boolean- 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyToggleProvider.GetToggleBool(System.String,System.String,System.Boolean)')
  - [GetToggleNumber()](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleNumber-System-String,System-String,System-Int32- 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyToggleProvider.GetToggleNumber(System.String,System.String,System.Int32)')
  - [GetToggleNumber()](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleNumber-System-String,System-Int32- 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyToggleProvider.GetToggleNumber(System.String,System.Int32)')
  - [GetToggleString()](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleString-System-String,System-String,System-String- 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyToggleProvider.GetToggleString(System.String,System.String,System.String)')
  - [GetToggleString()](#M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-GetToggleString-System-String,System-String- 'RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyToggleProvider.GetToggleString(System.String,System.String)')

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

<a name='T-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyOptions'></a>
## LaunchDarklyOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Toggles.LaunchDarkly

##### Summary

Configuration Options for Launch Darkly Feature Toggle Provider

<a name='F-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary

Configuration Section for Launch Darkly Configuration Options

<a name='P-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyOptions-DefaultUserKey'></a>
### DefaultUserKey `property`

##### Summary

Default user key used for Feature Toggle State filtering

##### Remarks

Set to a personal value to change settings without impacting other users

<a name='P-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyOptions-SdkKey'></a>
### SdkKey `property`

##### Summary

Sdk key used to access launch darkly

<a name='T-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider'></a>
## LaunchDarklyToggleProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Toggles.LaunchDarkly

##### Summary

The launch darkly toggle provider.

<a name='M-RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyToggleProvider-#ctor-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyOptions}-'></a>
### #ctor(options) `constructor`

##### Summary

Launch darkly feature toggle provider class for use with the connector sdk

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| options | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Toggles-LaunchDarkly-LaunchDarklyOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.LaunchDarklyOptions}') |  |

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
