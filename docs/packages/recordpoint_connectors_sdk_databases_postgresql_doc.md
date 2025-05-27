<a name='assembly'></a>
# RecordPoint.Connectors.SDK.Databases.PostgreSql

## Contents

- [DesignTimeDbContextFactory](#T-RecordPoint-Connectors-SDK-Databases-PostgreSql-DesignTimeDbContextFactory 'RecordPoint.Connectors.SDK.Databases.PostgreSql.DesignTimeDbContextFactory')
  - [CreateDbContext(args)](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-DesignTimeDbContextFactory-CreateDbContext-System-String[]- 'RecordPoint.Connectors.SDK.Databases.PostgreSql.DesignTimeDbContextFactory.CreateDbContext(System.String[])')
  - [GetContextOptionsBuilder()](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-DesignTimeDbContextFactory-GetContextOptionsBuilder 'RecordPoint.Connectors.SDK.Databases.PostgreSql.DesignTimeDbContextFactory.GetContextOptionsBuilder')
- [IPostgreSqlConnectionFactory](#T-RecordPoint-Connectors-SDK-Databases-PostgreSql-IPostgreSqlConnectionFactory 'RecordPoint.Connectors.SDK.Databases.PostgreSql.IPostgreSqlConnectionFactory')
  - [GetConnection(connectionString)](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-IPostgreSqlConnectionFactory-GetConnection-System-String- 'RecordPoint.Connectors.SDK.Databases.PostgreSql.IPostgreSqlConnectionFactory.GetConnection(System.String)')
- [IPostgreSqlDbProvider\`1](#T-RecordPoint-Connectors-SDK-Databases-PostgreSql-IPostgreSqlDbProvider`1 'RecordPoint.Connectors.SDK.Databases.PostgreSql.IPostgreSqlDbProvider`1')
- [InitialCreate](#T-RecordPoint-Connectors-SDK-Databases-PostgreSql-Migrations-InitialCreate 'RecordPoint.Connectors.SDK.Databases.PostgreSql.Migrations.InitialCreate')
  - [BuildTargetModel(modelBuilder)](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-Migrations-InitialCreate-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder- 'RecordPoint.Connectors.SDK.Databases.PostgreSql.Migrations.InitialCreate.BuildTargetModel(Microsoft.EntityFrameworkCore.ModelBuilder)')
  - [Down(migrationBuilder)](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-Migrations-InitialCreate-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RecordPoint.Connectors.SDK.Databases.PostgreSql.Migrations.InitialCreate.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
  - [Up(migrationBuilder)](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-Migrations-InitialCreate-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RecordPoint.Connectors.SDK.Databases.PostgreSql.Migrations.InitialCreate.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
- [PostgreSqlConnectionFactory](#T-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectionFactory 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectionFactory')
  - [GetConnection(connectionString)](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectionFactory-GetConnection-System-String- 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectionFactory.GetConnection(System.String)')
- [PostgreSqlConnectorDbBuilderExtensions](#T-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbBuilderExtensions 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbBuilderExtensions')
  - [UsePostgreSqlConnectorDatabase(hostBuilder)](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbBuilderExtensions-UsePostgreSqlConnectorDatabase-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbBuilderExtensions.UsePostgreSqlConnectorDatabase(Microsoft.Extensions.Hosting.IHostBuilder)')
- [PostgreSqlConnectorDbContext](#T-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbContext 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbContext')
  - [#ctor(options,schemaName)](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbContext-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext},System-String- 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbContext.#ctor(Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext},System.String)')
  - [DEFAULT_DB_SCHEMA_NAME](#F-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbContext-DEFAULT_DB_SCHEMA_NAME 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME')
  - [GetSchema()](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbContext-GetSchema 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbContext.GetSchema')
- [PostgreSqlConnectorDbOptions](#T-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbOptions 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbOptions')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbOptions.SECTION_NAME')
  - [AdminPassword](#P-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbOptions-AdminPassword 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbOptions.AdminPassword')
  - [AdminUsername](#P-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbOptions-AdminUsername 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbOptions.AdminUsername')
  - [ConnectionString](#P-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbOptions-ConnectionString 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbOptions.ConnectionString')
- [PostgreSqlConnectorDbProvider](#T-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbProvider')
  - [#ctor(systemContext,databaseOptions,telemetryTracker,connectionFactory)](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbOptions},RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Databases-PostgreSql-IPostgreSqlConnectionFactory- 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbProvider.#ctor(RecordPoint.Connectors.SDK.Context.ISystemContext,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbOptions},RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Databases.PostgreSql.IPostgreSqlConnectionFactory)')
  - [_adminConnectionString](#F-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-_adminConnectionString 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbProvider._adminConnectionString')
  - [_databaseName](#F-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-_databaseName 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbProvider._databaseName')
  - [_databaseOptions](#F-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-_databaseOptions 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbProvider._databaseOptions')
  - [CreateDbAdminContext()](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-CreateDbAdminContext 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbProvider.CreateDbAdminContext')
  - [CreateDbContext()](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-CreateDbContext 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbProvider.CreateDbContext')
  - [GetAdminConnectionString()](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-GetAdminConnectionString 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbProvider.GetAdminConnectionString')
  - [GetConnectionString()](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-GetConnectionString 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbProvider.GetConnectionString')
  - [GetDatabaseName()](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-GetDatabaseName 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbProvider.GetDatabaseName')
- [PostgreSqlDbProvider\`1](#T-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlDbProvider`1')
  - [#ctor(systemContext,telemetryTracker,connectionFactory,options)](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Databases-PostgreSql-IPostgreSqlConnectionFactory,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbOptions}- 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlDbProvider`1.#ctor(RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Databases.PostgreSql.IPostgreSqlConnectionFactory,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbOptions})')
  - [SQL_CREATE_SCHEMA_SCRIPT](#F-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-SQL_CREATE_SCHEMA_SCRIPT 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlDbProvider`1.SQL_CREATE_SCHEMA_SCRIPT')
  - [_connectionFactory](#F-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-_connectionFactory 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlDbProvider`1._connectionFactory')
  - [CheckDatabaseExists(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-CheckDatabaseExists-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlDbProvider`1.CheckDatabaseExists(System.Threading.CancellationToken)')
  - [CreateSchema(schemaName,connection)](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-CreateSchema-System-String,System-Data-SqlClient-SqlConnection- 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlDbProvider`1.CreateSchema(System.String,System.Data.SqlClient.SqlConnection)')
  - [GetAdminContextOptionsBuilder()](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-GetAdminContextOptionsBuilder 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlDbProvider`1.GetAdminContextOptionsBuilder')
  - [GetContextOptionsBuilder()](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-GetContextOptionsBuilder 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlDbProvider`1.GetContextOptionsBuilder')
  - [GetSqlDatabaseScript(scriptName,parameters)](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-GetSqlDatabaseScript-System-String,System-Collections-Generic-Dictionary{System-String,System-String}- 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlDbProvider`1.GetSqlDatabaseScript(System.String,System.Collections.Generic.Dictionary{System.String,System.String})')
  - [RunScript(scriptName,paramName,paramValue,connection)](#M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-RunScript-System-String,System-String,System-String,System-Data-SqlClient-SqlConnection- 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlDbProvider`1.RunScript(System.String,System.String,System.String,System.Data.SqlClient.SqlConnection)')

<a name='T-RecordPoint-Connectors-SDK-Databases-PostgreSql-DesignTimeDbContextFactory'></a>
## DesignTimeDbContextFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.PostgreSql

##### Summary

The design time db context factory.

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-DesignTimeDbContextFactory-CreateDbContext-System-String[]-'></a>
### CreateDbContext(args) `method`

##### Summary

Creates db context.

##### Returns

A ConnectorDbContext

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| args | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | The args. |

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-DesignTimeDbContextFactory-GetContextOptionsBuilder'></a>
### GetContextOptionsBuilder() `method`

##### Summary

Get context options builder.

##### Returns

DbContextOptionsBuilder<ConnectorDbContext>

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-PostgreSql-IPostgreSqlConnectionFactory'></a>
## IPostgreSqlConnectionFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.PostgreSql

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-IPostgreSqlConnectionFactory-GetConnection-System-String-'></a>
### GetConnection(connectionString) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectionString | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-PostgreSql-IPostgreSqlDbProvider`1'></a>
## IPostgreSqlDbProvider\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.PostgreSql

##### Summary

Defines extensions to the database provider specifically for PostgreSql

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TDbContext |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-PostgreSql-Migrations-InitialCreate'></a>
## InitialCreate `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.PostgreSql.Migrations

##### Summary

The initial create.

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-Migrations-InitialCreate-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### BuildTargetModel(modelBuilder) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| modelBuilder | [Microsoft.EntityFrameworkCore.ModelBuilder](#T-Microsoft-EntityFrameworkCore-ModelBuilder 'Microsoft.EntityFrameworkCore.ModelBuilder') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-Migrations-InitialCreate-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Down(migrationBuilder) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| migrationBuilder | [Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder](#T-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder 'Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-Migrations-InitialCreate-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Up(migrationBuilder) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| migrationBuilder | [Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder](#T-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder 'Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder') |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectionFactory'></a>
## PostgreSqlConnectionFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.PostgreSql

##### Summary

The postgre sql connection factory.

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectionFactory-GetConnection-System-String-'></a>
### GetConnection(connectionString) `method`

##### Summary

Get the connection.

##### Returns

A SqlConnection

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectionString | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The connection string. |

<a name='T-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbBuilderExtensions'></a>
## PostgreSqlConnectorDbBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.PostgreSql

##### Summary

PostgreSql host builder extensions

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbBuilderExtensions-UsePostgreSqlConnectorDatabase-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UsePostgreSqlConnectorDatabase(hostBuilder) `method`

##### Summary

Use an PostgreSql Server as the connector database

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | App host builder |

<a name='T-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbContext'></a>
## PostgreSqlConnectorDbContext `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.PostgreSql

##### Summary

PostgreSql Connector Database Context

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbContext-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext},System-String-'></a>
### #ctor(options,schemaName) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| options | [Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext}](#T-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext} 'Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext}') |  |
| schemaName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='F-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbContext-DEFAULT_DB_SCHEMA_NAME'></a>
### DEFAULT_DB_SCHEMA_NAME `constants`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbContext-GetSchema'></a>
### GetSchema() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbOptions'></a>
## PostgreSqlConnectorDbOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.PostgreSql

##### Summary

The postgre sql connector db options.

<a name='F-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary

Configuration section name

<a name='P-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbOptions-AdminPassword'></a>
### AdminPassword `property`

##### Summary

Gets or sets the admin password.

<a name='P-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbOptions-AdminUsername'></a>
### AdminUsername `property`

##### Summary

Gets or sets the admin username.

<a name='P-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbOptions-ConnectionString'></a>
### ConnectionString `property`

##### Summary

Connection string for the database to be used

<a name='T-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider'></a>
## PostgreSqlConnectorDbProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.PostgreSql

##### Summary

The postgre sql connector db provider.

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbOptions},RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Databases-PostgreSql-IPostgreSqlConnectionFactory-'></a>
### #ctor(systemContext,databaseOptions,telemetryTracker,connectionFactory) `constructor`

##### Summary

Initializes a new instance of the [PostgreSqlConnectorDbProvider](#T-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider 'RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbProvider') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| databaseOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbOptions}') | The database options. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| connectionFactory | [RecordPoint.Connectors.SDK.Databases.PostgreSql.IPostgreSqlConnectionFactory](#T-RecordPoint-Connectors-SDK-Databases-PostgreSql-IPostgreSqlConnectionFactory 'RecordPoint.Connectors.SDK.Databases.PostgreSql.IPostgreSqlConnectionFactory') | The connection factory. |

<a name='F-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-_adminConnectionString'></a>
### _adminConnectionString `constants`

##### Summary

The admin connection string.

<a name='F-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-_databaseName'></a>
### _databaseName `constants`

##### Summary

The database name.

<a name='F-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-_databaseOptions'></a>
### _databaseOptions `constants`

##### Summary

The database options.

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-CreateDbAdminContext'></a>
### CreateDbAdminContext() `method`

##### Summary

Creates db admin context.

##### Returns

A ConnectorDbContext

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-CreateDbContext'></a>
### CreateDbContext() `method`

##### Summary

Creates db context.

##### Returns

A ConnectorDbContext

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-GetAdminConnectionString'></a>
### GetAdminConnectionString() `method`

##### Summary

Get admin connection string.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-GetConnectionString'></a>
### GetConnectionString() `method`

##### Summary

Get connection string.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbProvider-GetDatabaseName'></a>
### GetDatabaseName() `method`

##### Summary

Get database name.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1'></a>
## PostgreSqlDbProvider\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.PostgreSql

##### Summary

The postgre sql db provider.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TDbContext |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Databases-PostgreSql-IPostgreSqlConnectionFactory,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbOptions}-'></a>
### #ctor(systemContext,telemetryTracker,connectionFactory,options) `constructor`

##### Summary

Initializes a new instance of the class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| connectionFactory | [RecordPoint.Connectors.SDK.Databases.PostgreSql.IPostgreSqlConnectionFactory](#T-RecordPoint-Connectors-SDK-Databases-PostgreSql-IPostgreSqlConnectionFactory 'RecordPoint.Connectors.SDK.Databases.PostgreSql.IPostgreSqlConnectionFactory') | The connection factory. |
| options | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlConnectorDbOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.PostgreSql.PostgreSqlConnectorDbOptions}') | The options. |

<a name='F-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-SQL_CREATE_SCHEMA_SCRIPT'></a>
### SQL_CREATE_SCHEMA_SCRIPT `constants`

##### Summary

The SQL CREATE SCHEMA SCRIPT.

<a name='F-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-_connectionFactory'></a>
### _connectionFactory `constants`

##### Summary

The connection factory.

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-CheckDatabaseExists-System-Threading-CancellationToken-'></a>
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
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-CreateSchema-System-String,System-Data-SqlClient-SqlConnection-'></a>
### CreateSchema(schemaName,connection) `method`

##### Summary

Creates the schema.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| schemaName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The schema name. |
| connection | [System.Data.SqlClient.SqlConnection](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Data.SqlClient.SqlConnection 'System.Data.SqlClient.SqlConnection') | The connection. |

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-GetAdminContextOptionsBuilder'></a>
### GetAdminContextOptionsBuilder() `method`

##### Summary

Get admin context options builder.

##### Returns

DbContextOptionsBuilder<TDbContext>

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-GetContextOptionsBuilder'></a>
### GetContextOptionsBuilder() `method`

##### Summary

Get context options builder.

##### Returns

DbContextOptionsBuilder<TDbContext>

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-GetSqlDatabaseScript-System-String,System-Collections-Generic-Dictionary{System-String,System-String}-'></a>
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

<a name='M-RecordPoint-Connectors-SDK-Databases-PostgreSql-PostgreSqlDbProvider`1-RunScript-System-String,System-String,System-String,System-Data-SqlClient-SqlConnection-'></a>
### RunScript(scriptName,paramName,paramValue,connection) `method`

##### Summary

Run the script.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| scriptName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The script name. |
| paramName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The param name. |
| paramValue | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The param value. |
| connection | [System.Data.SqlClient.SqlConnection](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Data.SqlClient.SqlConnection 'System.Data.SqlClient.SqlConnection') | The connection. |
