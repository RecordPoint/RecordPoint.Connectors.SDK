<a name='assembly'></a>
# RecordPoint.Connectors.SDK.Databases.AzureSql

## Contents

- [AzureSqlConnectionFactory](#T-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectionFactory 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectionFactory')
  - [GetConnection(connectionString)](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectionFactory-GetConnection-System-String- 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectionFactory.GetConnection(System.String)')
- [AzureSqlConnectorDbBuilderExtensions](#T-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbBuilderExtensions 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbBuilderExtensions')
  - [UseAzureSqlConnectorDatabase(hostBuilder)](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbBuilderExtensions-UseAzureSqlConnectorDatabase-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbBuilderExtensions.UseAzureSqlConnectorDatabase(Microsoft.Extensions.Hosting.IHostBuilder)')
- [AzureSqlConnectorDbContext](#T-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbContext 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbContext')
  - [#ctor(options,schemaName)](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbContext-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext},System-String- 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbContext.#ctor(Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext},System.String)')
  - [DEFAULT_DB_SCHEMA_NAME](#F-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbContext-DEFAULT_DB_SCHEMA_NAME 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME')
  - [_schemaName](#F-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbContext-_schemaName 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbContext._schemaName')
  - [GetSchema()](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbContext-GetSchema 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbContext.GetSchema')
- [AzureSqlConnectorDbOptions](#T-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbOptions 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbOptions.SECTION_NAME')
  - [AdminPassword](#P-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbOptions-AdminPassword 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbOptions.AdminPassword')
  - [AdminUsername](#P-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbOptions-AdminUsername 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbOptions.AdminUsername')
  - [ConnectionString](#P-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbOptions-ConnectionString 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbOptions.ConnectionString')
- [AzureSqlConnectorDbProvider](#T-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbProvider')
  - [#ctor(systemContext,databaseOptions,telemetryTracker,connectionFactory)](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbOptions},RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Databases-AzureSql-IAzureSqlConnectionFactory- 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbProvider.#ctor(RecordPoint.Connectors.SDK.Context.ISystemContext,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbOptions},RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Databases.AzureSql.IAzureSqlConnectionFactory)')
  - [_adminConnectionString](#F-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-_adminConnectionString 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbProvider._adminConnectionString')
  - [_databaseName](#F-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-_databaseName 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbProvider._databaseName')
  - [_databaseOptions](#F-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-_databaseOptions 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbProvider._databaseOptions')
  - [CreateDbAdminContext()](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-CreateDbAdminContext 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbProvider.CreateDbAdminContext')
  - [CreateDbContext()](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-CreateDbContext 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbProvider.CreateDbContext')
  - [GetAdminConnectionString()](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-GetAdminConnectionString 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbProvider.GetAdminConnectionString')
  - [GetConnectionString()](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-GetConnectionString 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbProvider.GetConnectionString')
  - [GetDatabaseName()](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-GetDatabaseName 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbProvider.GetDatabaseName')
- [AzureSqlDbProvider\`1](#T-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlDbProvider`1')
  - [#ctor(systemContext,telemetryTracker,connectionFactory,options)](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Databases-AzureSql-IAzureSqlConnectionFactory,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbOptions}- 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlDbProvider`1.#ctor(RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Databases.AzureSql.IAzureSqlConnectionFactory,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbOptions})')
  - [SQL_CREATE_SCHEMA_SCRIPT](#F-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-SQL_CREATE_SCHEMA_SCRIPT 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlDbProvider`1.SQL_CREATE_SCHEMA_SCRIPT')
  - [_connectionFactory](#F-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-_connectionFactory 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlDbProvider`1._connectionFactory')
  - [CheckDatabaseExists(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-CheckDatabaseExists-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlDbProvider`1.CheckDatabaseExists(System.Threading.CancellationToken)')
  - [CreateSchema(schemaName,connection)](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-CreateSchema-System-String,Microsoft-Data-SqlClient-SqlConnection- 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlDbProvider`1.CreateSchema(System.String,Microsoft.Data.SqlClient.SqlConnection)')
  - [GetAdminContextOptionsBuilder()](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-GetAdminContextOptionsBuilder 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlDbProvider`1.GetAdminContextOptionsBuilder')
  - [GetContextOptionsBuilder()](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-GetContextOptionsBuilder 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlDbProvider`1.GetContextOptionsBuilder')
  - [GetSqlDatabaseScript(scriptName,parameters)](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-GetSqlDatabaseScript-System-String,System-Collections-Generic-Dictionary{System-String,System-String}- 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlDbProvider`1.GetSqlDatabaseScript(System.String,System.Collections.Generic.Dictionary{System.String,System.String})')
  - [RunScript(scriptName,paramName,paramValue,connection)](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-RunScript-System-String,System-String,System-String,Microsoft-Data-SqlClient-SqlConnection- 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlDbProvider`1.RunScript(System.String,System.String,System.String,Microsoft.Data.SqlClient.SqlConnection)')
- [DesignTimeDbContextFactory](#T-RecordPoint-Connectors-SDK-Databases-AzureSql-DesignTimeDbContextFactory 'RecordPoint.Connectors.SDK.Databases.AzureSql.DesignTimeDbContextFactory')
  - [CreateDbContext(args)](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-DesignTimeDbContextFactory-CreateDbContext-System-String[]- 'RecordPoint.Connectors.SDK.Databases.AzureSql.DesignTimeDbContextFactory.CreateDbContext(System.String[])')
  - [GetContextOptionsBuilder()](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-DesignTimeDbContextFactory-GetContextOptionsBuilder 'RecordPoint.Connectors.SDK.Databases.AzureSql.DesignTimeDbContextFactory.GetContextOptionsBuilder')
- [IAzureSqlConnectionFactory](#T-RecordPoint-Connectors-SDK-Databases-AzureSql-IAzureSqlConnectionFactory 'RecordPoint.Connectors.SDK.Databases.AzureSql.IAzureSqlConnectionFactory')
  - [GetConnection(connectionString)](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-IAzureSqlConnectionFactory-GetConnection-System-String- 'RecordPoint.Connectors.SDK.Databases.AzureSql.IAzureSqlConnectionFactory.GetConnection(System.String)')
- [IAzureSqlDbProvider\`1](#T-RecordPoint-Connectors-SDK-Databases-AzureSql-IAzureSqlDbProvider`1 'RecordPoint.Connectors.SDK.Databases.AzureSql.IAzureSqlDbProvider`1')
- [InitialCreate](#T-RecordPoint-Connectors-SDK-Databases-AzureSql-Migrations-InitialCreate 'RecordPoint.Connectors.SDK.Databases.AzureSql.Migrations.InitialCreate')
  - [BuildTargetModel(modelBuilder)](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-Migrations-InitialCreate-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder- 'RecordPoint.Connectors.SDK.Databases.AzureSql.Migrations.InitialCreate.BuildTargetModel(Microsoft.EntityFrameworkCore.ModelBuilder)')
  - [Down(migrationBuilder)](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-Migrations-InitialCreate-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RecordPoint.Connectors.SDK.Databases.AzureSql.Migrations.InitialCreate.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
  - [Up(migrationBuilder)](#M-RecordPoint-Connectors-SDK-Databases-AzureSql-Migrations-InitialCreate-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RecordPoint.Connectors.SDK.Databases.AzureSql.Migrations.InitialCreate.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')

<a name='T-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectionFactory'></a>
## AzureSqlConnectionFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.AzureSql

##### Summary

The azure sql connection factory.

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectionFactory-GetConnection-System-String-'></a>
### GetConnection(connectionString) `method`

##### Summary

Get the connection.

##### Returns

A SqlConnection

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectionString | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The connection string. |

<a name='T-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbBuilderExtensions'></a>
## AzureSqlConnectorDbBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.AzureSql

##### Summary

AzureSql host builder extensions

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbBuilderExtensions-UseAzureSqlConnectorDatabase-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseAzureSqlConnectorDatabase(hostBuilder) `method`

##### Summary

Use an AzureSql Server as the connector database

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | App host builder |

<a name='T-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbContext'></a>
## AzureSqlConnectorDbContext `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.AzureSql

##### Summary

The azure sql connector db context.

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbContext-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext},System-String-'></a>
### #ctor(options,schemaName) `constructor`

##### Summary

Initializes a new instance of the [AzureSqlConnectorDbContext](#T-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbContext 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbContext') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| options | [Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext}](#T-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext} 'Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext}') | The options. |
| schemaName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The schema name. |

<a name='F-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbContext-DEFAULT_DB_SCHEMA_NAME'></a>
### DEFAULT_DB_SCHEMA_NAME `constants`

##### Summary

The DEFAULT DB SCHEMA NAME.

<a name='F-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbContext-_schemaName'></a>
### _schemaName `constants`

##### Summary

The schema name.

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbContext-GetSchema'></a>
### GetSchema() `method`

##### Summary

Get the schema.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbOptions'></a>
## AzureSqlConnectorDbOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.AzureSql

##### Summary

The azure sql connector db options.

<a name='F-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary

Configuration section name

<a name='P-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbOptions-AdminPassword'></a>
### AdminPassword `property`

##### Summary

Gets or sets the admin password.

<a name='P-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbOptions-AdminUsername'></a>
### AdminUsername `property`

##### Summary

Gets or sets the admin username.

<a name='P-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbOptions-ConnectionString'></a>
### ConnectionString `property`

##### Summary

Connection string for the database to be used

<a name='T-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider'></a>
## AzureSqlConnectorDbProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.AzureSql

##### Summary

The azure sql connector db provider.

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbOptions},RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Databases-AzureSql-IAzureSqlConnectionFactory-'></a>
### #ctor(systemContext,databaseOptions,telemetryTracker,connectionFactory) `constructor`

##### Summary

Initializes a new instance of the [AzureSqlConnectorDbProvider](#T-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider 'RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbProvider') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| databaseOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbOptions}') | The database options. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| connectionFactory | [RecordPoint.Connectors.SDK.Databases.AzureSql.IAzureSqlConnectionFactory](#T-RecordPoint-Connectors-SDK-Databases-AzureSql-IAzureSqlConnectionFactory 'RecordPoint.Connectors.SDK.Databases.AzureSql.IAzureSqlConnectionFactory') | The connection factory. |

<a name='F-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-_adminConnectionString'></a>
### _adminConnectionString `constants`

##### Summary

The admin connection string.

<a name='F-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-_databaseName'></a>
### _databaseName `constants`

##### Summary

The database name.

<a name='F-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-_databaseOptions'></a>
### _databaseOptions `constants`

##### Summary

The database options.

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-CreateDbAdminContext'></a>
### CreateDbAdminContext() `method`

##### Summary

Creates db admin context.

##### Returns

A ConnectorDbContext

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-CreateDbContext'></a>
### CreateDbContext() `method`

##### Summary

Creates db context.

##### Returns

A ConnectorDbContext

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-GetAdminConnectionString'></a>
### GetAdminConnectionString() `method`

##### Summary

Get admin connection string.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-GetConnectionString'></a>
### GetConnectionString() `method`

##### Summary

Get connection string.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbProvider-GetDatabaseName'></a>
### GetDatabaseName() `method`

##### Summary

Get database name.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1'></a>
## AzureSqlDbProvider\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.AzureSql

##### Summary

The azure sql db provider.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TDbContext |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Databases-AzureSql-IAzureSqlConnectionFactory,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbOptions}-'></a>
### #ctor(systemContext,telemetryTracker,connectionFactory,options) `constructor`

##### Summary

Initializes a new instance of the class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| connectionFactory | [RecordPoint.Connectors.SDK.Databases.AzureSql.IAzureSqlConnectionFactory](#T-RecordPoint-Connectors-SDK-Databases-AzureSql-IAzureSqlConnectionFactory 'RecordPoint.Connectors.SDK.Databases.AzureSql.IAzureSqlConnectionFactory') | The connection factory. |
| options | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlConnectorDbOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.AzureSql.AzureSqlConnectorDbOptions}') | The options. |

<a name='F-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-SQL_CREATE_SCHEMA_SCRIPT'></a>
### SQL_CREATE_SCHEMA_SCRIPT `constants`

##### Summary

The SQL CREATE SCHEMA SCRIPT.

<a name='F-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-_connectionFactory'></a>
### _connectionFactory `constants`

##### Summary

The connection factory.

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-CheckDatabaseExists-System-Threading-CancellationToken-'></a>
### CheckDatabaseExists(cancellationToken) `method`

##### Summary

Check database exists.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [RecordPoint.Connectors.SDK.Databases.ConnectorDatabaseException](#T-RecordPoint-Connectors-SDK-Databases-ConnectorDatabaseException 'RecordPoint.Connectors.SDK.Databases.ConnectorDatabaseException') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-CreateSchema-System-String,Microsoft-Data-SqlClient-SqlConnection-'></a>
### CreateSchema(schemaName,connection) `method`

##### Summary

Creates the schema.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| schemaName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The schema name. |
| connection | [Microsoft.Data.SqlClient.SqlConnection](#T-Microsoft-Data-SqlClient-SqlConnection 'Microsoft.Data.SqlClient.SqlConnection') | The connection. |

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-GetAdminContextOptionsBuilder'></a>
### GetAdminContextOptionsBuilder() `method`

##### Summary

Get admin context options builder.

##### Returns

DbContextOptionsBuilder<TDbContext>

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-GetContextOptionsBuilder'></a>
### GetContextOptionsBuilder() `method`

##### Summary

Get context options builder.

##### Returns

DbContextOptionsBuilder<TDbContext>

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-GetSqlDatabaseScript-System-String,System-Collections-Generic-Dictionary{System-String,System-String}-'></a>
### GetSqlDatabaseScript(scriptName,parameters) `method`

##### Summary

Get sql database script.

##### Returns

A string

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| scriptName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The script name. |
| parameters | [System.Collections.Generic.Dictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.String}') | The parameters. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-AzureSqlDbProvider`1-RunScript-System-String,System-String,System-String,Microsoft-Data-SqlClient-SqlConnection-'></a>
### RunScript(scriptName,paramName,paramValue,connection) `method`

##### Summary

Run the script.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| scriptName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The script name. |
| paramName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The param name. |
| paramValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The param value. |
| connection | [Microsoft.Data.SqlClient.SqlConnection](#T-Microsoft-Data-SqlClient-SqlConnection 'Microsoft.Data.SqlClient.SqlConnection') | The connection. |

<a name='T-RecordPoint-Connectors-SDK-Databases-AzureSql-DesignTimeDbContextFactory'></a>
## DesignTimeDbContextFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.AzureSql

##### Summary

The design time db context factory.

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-DesignTimeDbContextFactory-CreateDbContext-System-String[]-'></a>
### CreateDbContext(args) `method`

##### Summary

Creates db context.

##### Returns

A ConnectorDbContext

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| args | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | The args. |

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-DesignTimeDbContextFactory-GetContextOptionsBuilder'></a>
### GetContextOptionsBuilder() `method`

##### Summary

Get context options builder.

##### Returns

DbContextOptionsBuilder<ConnectorDbContext>

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-AzureSql-IAzureSqlConnectionFactory'></a>
## IAzureSqlConnectionFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.AzureSql

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-IAzureSqlConnectionFactory-GetConnection-System-String-'></a>
### GetConnection(connectionString) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectionString | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-AzureSql-IAzureSqlDbProvider`1'></a>
## IAzureSqlDbProvider\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.AzureSql

##### Summary

Defines extensions to the database provider specifically for AzureSql

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TDbContext |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-AzureSql-Migrations-InitialCreate'></a>
## InitialCreate `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.AzureSql.Migrations

##### Summary

The initial create.

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-Migrations-InitialCreate-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### BuildTargetModel(modelBuilder) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| modelBuilder | [Microsoft.EntityFrameworkCore.ModelBuilder](#T-Microsoft-EntityFrameworkCore-ModelBuilder 'Microsoft.EntityFrameworkCore.ModelBuilder') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-Migrations-InitialCreate-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Down(migrationBuilder) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| migrationBuilder | [Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder](#T-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder 'Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-AzureSql-Migrations-InitialCreate-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Up(migrationBuilder) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| migrationBuilder | [Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder](#T-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder 'Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder') |  |
