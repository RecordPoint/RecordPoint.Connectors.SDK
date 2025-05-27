# RecordPoint.Connectors.SDK

[RecordPoint.Connectors.SDK Api Documentation](./recordpoint_connectors_sdk_doc.md)

The `RecordPoint.Connectors.SDK` Package is the main package for the Connectors SDK.
It contains the logic that makes up a connector as well as `IHostBuilder` extensions for configuring dependency injection for the various connector responsibilities.

## Features

#### Configuration
* [Configuration extensions](../configuration/configuration.md)
  * [RecordPoint.Connectors.SDK.Configuration.AzureKeyVault](./recordpoint_connectors_sdk_configuration_azurekeyvault.md)
* [Feature Toggles](../feature_toggles.md)
  * [RecordPoint.Connectors.SDK.Toggles.LaunchDarkly](./recordpoint_connectors_sdk_toggles_launchdarkly.md)

#### Operations
* [Connector Operations](../operations/operations.md)

#### Observability
* [Connector Health](../observability/health.md)
* [Logging Extensions](../observability/logging.md)
* [Observability Scopes](../observability/scopes.md)
* [Connector Status](../observability/status.md)

#### Managed Work Queue
* [Work Management Services](../managedworkqueue.md)

---

*The interface definitions, models, and enums used within this library are defined within the [RecordPoint.Connectors.SDK.Abstractions](./recordpoint_connectors_sdk_abstractions.md) library.*