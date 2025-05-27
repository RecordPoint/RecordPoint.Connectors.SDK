# RecordPoint.Connectors.SDK.Databases.Cosmos

This package contains the Cosmos database provider for the Connectors SDK.

To use this package, you need to inject the required services using the Host Builder extension method:
```cs
    hostBuilder.UseCosmosDbConnectorDatabase();
```

Configuration must be provided via appsettings.json or KeyVault:
```
"CosmosDbConnectorDatabase": {
    "DatabaseName": "{defaults to 'connector-db'}",
    "ConnectionString": "{connection string for database}"
}
```

The database must contain the following containers:
- 'channels' [Partition Key: '/ConnectorId', Unique Keys: '/ExternalId']
- 'connectors' [Partition Key: '/ConnectorId', Unique Keys: '/ConnectorId']
- 'managedworkstatus' [Partition Key: '/ConnectorId', Unique Keys: '/Id']