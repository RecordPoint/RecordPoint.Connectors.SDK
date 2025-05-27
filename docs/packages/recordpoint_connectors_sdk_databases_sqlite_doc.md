<a name='assembly'></a>
# RecordPoint.Connectors.SDK.Databases.Sqlite

## Contents

- [DesignTimeDbContextFactory](#T-RecordPoint-Connectors-SDK-Databases-Sqlite-DesignTimeDbContextFactory 'RecordPoint.Connectors.SDK.Databases.Sqlite.DesignTimeDbContextFactory')
  - [CreateDbContext(args)](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-DesignTimeDbContextFactory-CreateDbContext-System-String[]- 'RecordPoint.Connectors.SDK.Databases.Sqlite.DesignTimeDbContextFactory.CreateDbContext(System.String[])')
  - [GetContextOptionsBuilder()](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-DesignTimeDbContextFactory-GetContextOptionsBuilder 'RecordPoint.Connectors.SDK.Databases.Sqlite.DesignTimeDbContextFactory.GetContextOptionsBuilder')
- [ISqliteDatabaseProvider\`1](#T-RecordPoint-Connectors-SDK-Databases-Sqlite-ISqliteDatabaseProvider`1 'RecordPoint.Connectors.SDK.Databases.Sqlite.ISqliteDatabaseProvider`1')
  - [GetDatabasePath()](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-ISqliteDatabaseProvider`1-GetDatabasePath 'RecordPoint.Connectors.SDK.Databases.Sqlite.ISqliteDatabaseProvider`1.GetDatabasePath')
- [InitialCreate](#T-RecordPoint-Connectors-SDK-Databases-Sqlite-Migrations-InitialCreate 'RecordPoint.Connectors.SDK.Databases.Sqlite.Migrations.InitialCreate')
  - [BuildTargetModel(modelBuilder)](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-Migrations-InitialCreate-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder- 'RecordPoint.Connectors.SDK.Databases.Sqlite.Migrations.InitialCreate.BuildTargetModel(Microsoft.EntityFrameworkCore.ModelBuilder)')
  - [Down(migrationBuilder)](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-Migrations-InitialCreate-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RecordPoint.Connectors.SDK.Databases.Sqlite.Migrations.InitialCreate.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
  - [Up(migrationBuilder)](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-Migrations-InitialCreate-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RecordPoint.Connectors.SDK.Databases.Sqlite.Migrations.InitialCreate.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
- [SqliteConnectorDatabaseBuilderExtensions](#T-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseBuilderExtensions 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteConnectorDatabaseBuilderExtensions')
  - [UseSqliteConnectorDatabase(hostBuilder)](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseBuilderExtensions-UseSqliteConnectorDatabase-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteConnectorDatabaseBuilderExtensions.UseSqliteConnectorDatabase(Microsoft.Extensions.Hosting.IHostBuilder)')
- [SqliteConnectorDatabaseOptions](#T-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseOptions 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteConnectorDatabaseOptions')
  - [DEFAULT_DATABASE_NAME](#F-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseOptions-DEFAULT_DATABASE_NAME 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteConnectorDatabaseOptions.DEFAULT_DATABASE_NAME')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteConnectorDatabaseOptions.SECTION_NAME')
  - [DatabaseName](#P-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseOptions-DatabaseName 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteConnectorDatabaseOptions.DatabaseName')
- [SqliteConnectorDatabaseProvider](#T-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseProvider 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteConnectorDatabaseProvider')
  - [#ctor(systemContext,databaseOptions)](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseProvider-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseOptions}- 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteConnectorDatabaseProvider.#ctor(RecordPoint.Connectors.SDK.Context.ISystemContext,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteConnectorDatabaseOptions})')
  - [_databaseOptions](#F-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseProvider-_databaseOptions 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteConnectorDatabaseProvider._databaseOptions')
  - [CreateDbContext()](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseProvider-CreateDbContext 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteConnectorDatabaseProvider.CreateDbContext')
  - [GetDatabaseName()](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseProvider-GetDatabaseName 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteConnectorDatabaseProvider.GetDatabaseName')
- [SqliteConnectorDbContext](#T-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDbContext 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteConnectorDbContext')
  - [#ctor(options)](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDbContext-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext}- 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteConnectorDbContext.#ctor(Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext})')
- [SqliteDatabaseProvider\`1](#T-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1')
  - [#ctor(systemContext)](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext- 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1.#ctor(RecordPoint.Connectors.SDK.Context.ISystemContext)')
  - [SQLITE_EXTENSION](#F-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-SQLITE_EXTENSION 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1.SQLITE_EXTENSION')
  - [SQLITE_SYSTEM_NAME](#F-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-SQLITE_SYSTEM_NAME 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1.SQLITE_SYSTEM_NAME')
  - [_readySource](#F-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-_readySource 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1._readySource')
  - [_systemContext](#F-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-_systemContext 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1._systemContext')
  - [CleanupAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-CleanupAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1.CleanupAsync(System.Threading.CancellationToken)')
  - [CreateDbContext()](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-CreateDbContext 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1.CreateDbContext')
  - [Exists()](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-Exists 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1.Exists')
  - [GetConnectionString()](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-GetConnectionString 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1.GetConnectionString')
  - [GetContextOptionsBuilder()](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-GetContextOptionsBuilder 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1.GetContextOptionsBuilder')
  - [GetDataDirectory()](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-GetDataDirectory 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1.GetDataDirectory')
  - [GetDatabaseName()](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-GetDatabaseName 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1.GetDatabaseName')
  - [GetDatabasePath()](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-GetDatabasePath 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1.GetDatabasePath')
  - [GetExternalSystemName()](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-GetExternalSystemName 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1.GetExternalSystemName')
  - [PrepareAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-PrepareAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1.PrepareAsync(System.Threading.CancellationToken)')
  - [ReadyAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-ReadyAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1.ReadyAsync(System.Threading.CancellationToken)')
  - [RemoveAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-RemoveAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1.RemoveAsync(System.Threading.CancellationToken)')
  - [SetReady(exception)](#M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-SetReady-System-Exception- 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteDatabaseProvider`1.SetReady(System.Exception)')

<a name='T-RecordPoint-Connectors-SDK-Databases-Sqlite-DesignTimeDbContextFactory'></a>
## DesignTimeDbContextFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Sqlite

##### Summary

The design time db context factory.

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-DesignTimeDbContextFactory-CreateDbContext-System-String[]-'></a>
### CreateDbContext(args) `method`

##### Summary

Creates db context.

##### Returns

A ConnectorDbContext

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| args | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | The args. |

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-DesignTimeDbContextFactory-GetContextOptionsBuilder'></a>
### GetContextOptionsBuilder() `method`

##### Summary

Get context options builder.

##### Returns

DbContextOptionsBuilder<ConnectorDbContext>

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-Sqlite-ISqliteDatabaseProvider`1'></a>
## ISqliteDatabaseProvider\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Sqlite

##### Summary

Defines extensions to the database provider specifically for sqllite

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TDbContext |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-ISqliteDatabaseProvider`1-GetDatabasePath'></a>
### GetDatabasePath() `method`

##### Summary

Get the path to the sqlite database

##### Returns

File path

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-Sqlite-Migrations-InitialCreate'></a>
## InitialCreate `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Sqlite.Migrations

##### Summary

The initial create.

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-Migrations-InitialCreate-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### BuildTargetModel(modelBuilder) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| modelBuilder | [Microsoft.EntityFrameworkCore.ModelBuilder](#T-Microsoft-EntityFrameworkCore-ModelBuilder 'Microsoft.EntityFrameworkCore.ModelBuilder') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-Migrations-InitialCreate-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Down(migrationBuilder) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| migrationBuilder | [Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder](#T-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder 'Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-Migrations-InitialCreate-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Up(migrationBuilder) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| migrationBuilder | [Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder](#T-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder 'Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder') |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseBuilderExtensions'></a>
## SqliteConnectorDatabaseBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Sqlite

##### Summary

Sqllite host builder extensions

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseBuilderExtensions-UseSqliteConnectorDatabase-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseSqliteConnectorDatabase(hostBuilder) `method`

##### Summary

Use Sqlite as the connector database

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | App host builder |

<a name='T-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseOptions'></a>
## SqliteConnectorDatabaseOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Sqlite

##### Summary

The sqlite connector database options.

<a name='F-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseOptions-DEFAULT_DATABASE_NAME'></a>
### DEFAULT_DATABASE_NAME `constants`

##### Summary

Default database name

<a name='F-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary

Configuration section name

<a name='P-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseOptions-DatabaseName'></a>
### DatabaseName `property`

##### Summary

Name of the database

<a name='T-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseProvider'></a>
## SqliteConnectorDatabaseProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Sqlite

##### Summary

The sqlite connector database provider.

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseProvider-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseOptions}-'></a>
### #ctor(systemContext,databaseOptions) `constructor`

##### Summary

Initializes a new instance of the [SqliteConnectorDatabaseProvider](#T-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseProvider 'RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteConnectorDatabaseProvider') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| databaseOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteConnectorDatabaseOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.Sqlite.SqliteConnectorDatabaseOptions}') | The database options. |

<a name='F-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseProvider-_databaseOptions'></a>
### _databaseOptions `constants`

##### Summary

The database options.

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseProvider-CreateDbContext'></a>
### CreateDbContext() `method`

##### Summary

Creates db context.

##### Returns

A ConnectorDbContext

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDatabaseProvider-GetDatabaseName'></a>
### GetDatabaseName() `method`

##### Summary

Get database name.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDbContext'></a>
## SqliteConnectorDbContext `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Sqlite

##### Summary

Sqlite Connector Database Context

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteConnectorDbContext-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext}-'></a>
### #ctor(options) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| options | [Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext}](#T-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext} 'Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext}') |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1'></a>
## SqliteDatabaseProvider\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.Sqlite

##### Summary

The sqlite database provider.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TDbContext |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext-'></a>
### #ctor(systemContext) `constructor`

##### Summary

Initializes a new instance of the class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |

<a name='F-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-SQLITE_EXTENSION'></a>
### SQLITE_EXTENSION `constants`

##### Summary

The SQLITE EXTENSION.

<a name='F-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-SQLITE_SYSTEM_NAME'></a>
### SQLITE_SYSTEM_NAME `constants`

##### Summary

The SQLITE SYSTEM NAME.

<a name='F-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-_readySource'></a>
### _readySource `constants`

##### Summary

The ready source.

<a name='F-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-_systemContext'></a>
### _systemContext `constants`

##### Summary

The system context.

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-CleanupAsync-System-Threading-CancellationToken-'></a>
### CleanupAsync(cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-CreateDbContext'></a>
### CreateDbContext() `method`

##### Summary

Create a connector db context

##### Returns

Connector db context

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-Exists'></a>
### Exists() `method`

##### Summary

Does the database exist?

##### Returns

True if the database exists, otherwise false

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-GetConnectionString'></a>
### GetConnectionString() `method`

##### Summary

Get the connection string for a database

##### Returns

Connection string for the database

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-GetContextOptionsBuilder'></a>
### GetContextOptionsBuilder() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-GetDataDirectory'></a>
### GetDataDirectory() `method`

##### Summary

Get the data directory

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-GetDatabaseName'></a>
### GetDatabaseName() `method`

##### Summary

Required override to get the database name

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-GetDatabasePath'></a>
### GetDatabasePath() `method`

##### Summary

Get the full path to an sqllite database

##### Returns

Path to the selected database

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-GetExternalSystemName'></a>
### GetExternalSystemName() `method`

##### Summary

Get the external system name

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-PrepareAsync-System-Threading-CancellationToken-'></a>
### PrepareAsync(cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-ReadyAsync-System-Threading-CancellationToken-'></a>
### ReadyAsync(cancellationToken) `method`

##### Summary

Called by dependant services to determine if the database is ready for operation.

##### Returns

Readiness Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-RemoveAsync-System-Threading-CancellationToken-'></a>
### RemoveAsync(cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-Sqlite-SqliteDatabaseProvider`1-SetReady-System-Exception-'></a>
### SetReady(exception) `method`

##### Summary

Set the readiness for this service

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Exception that indicates that the service has critically failed |
