# RecordPoint.Connectors.SDK.WebHost

[RecordPoint.Connectors.SDK.WebHost Api Documentation](./recordpoint_connectors_sdk_webhost_doc.md)

The Connectors SDK includes a web hosting package for enabling a RestFul Web Api.
The Api includes an endpoint for receiving webhook notifications from Records365
as well as an endpoint for obtaining the overall status of the running service instance.

## Configuration

### Hosting Urls
The base url used to host the Web Api can be configured via appsettings.
Multiple hosting urls can be configured using the "Urls" setting.

The default hosting url is `https://localhost:44342`

```json
  "WebHost": {
    "Urls": [ 
        "https://localhost:44342",
        "https://referenceconnector.recordpoint.com"
    ],
  }
```



### Authentication
The Api can be secured via OAuth using Microsoft Azure Active Directory.

If no `Authentication` setting is present, the Api will not require a bearer token and all unauthenticated requests will be accepted.

*Note: Authentication should always be tested after deployment to ensure the Api is appropriately secured.*

*Note: The `AllowWebApiToBeAuthorizedByACL` setting must be set to `true` as Records365 does not include scope or role claims
within the token and the authentication middleware will reject any tokens missing these claims unless this setting is enabled.*

```json
  "WebHost": {
    "Authentication": {
      "Instance": "https://login.microsoftonline.com/",
      "ClientId": "[... app registration client id ...]",
      "Audience": "[... app registration audience ...]",
      "TenantId": "common",
      "AllowWebApiToBeAuthorizedByACL": true
    }
  }
```

## Built-In Endpoints

### Notifications

See [Record.Connectors.SDK.Notifications](./recordpoint_connectors_sdk_notifications.md)

#### GET: /Notifications
When a `GET` request is sent to the `/Notifications` endpoint, it is treated as a "ping". A 200 (OK) response is returned to the caller.

#### POST: /Notifications
When a `POST` request is sent to the `/Notifications` endpoint, it is treated as a Records365 notification.

Records365 has several connector notification types:
- Connector Configuration Created
- Connector Configuration Deleted
- Connector Configuration Updated
- Item Destroyed