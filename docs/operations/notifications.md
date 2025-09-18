# Notifications

The operation is entirely managed by the SDK so only requires dependency injection configuration to setup the operation.

## Swagger 
Connectors that use Push (webhook) notifications expose API endpoints.
These APIs can be inspected using Swagger.

To access the Swagger UI:
- Go to the connector type YAML file and copy the value of `NotificationAuthenticationResource`
- Add '/swagger' to the end
- *In local environments*: 
  - Start the Notification service.
  - Open URL in browser
- *In prod-like environments*: 
  - Open the jumpbox for the environment. 
  - Open URL in browser in the jumpbox.
