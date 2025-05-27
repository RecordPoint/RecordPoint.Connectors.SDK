<a name='assembly'></a>
# RecordPoint.Connectors.SDK.Databases.LocalDb

## Contents

- [DesignTimeDbContextFactory](#T-RecordPoint-Connectors-SDK-Databases-LocalDb-DesignTimeDbContextFactory 'RecordPoint.Connectors.SDK.Databases.LocalDb.DesignTimeDbContextFactory')
  - [CreateDbContext(args)](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-DesignTimeDbContextFactory-CreateDbContext-System-String[]- 'RecordPoint.Connectors.SDK.Databases.LocalDb.DesignTimeDbContextFactory.CreateDbContext(System.String[])')
  - [GetContextOptionsBuilder()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-DesignTimeDbContextFactory-GetContextOptionsBuilder 'RecordPoint.Connectors.SDK.Databases.LocalDb.DesignTimeDbContextFactory.GetContextOptionsBuilder')
- [ILocalDbDatabaseProvider\`1](#T-RecordPoint-Connectors-SDK-Databases-LocalDb-ILocalDbDatabaseProvider`1 'RecordPoint.Connectors.SDK.Databases.LocalDb.ILocalDbDatabaseProvider`1')
  - [GetDatabaseLogFilePath()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-ILocalDbDatabaseProvider`1-GetDatabaseLogFilePath 'RecordPoint.Connectors.SDK.Databases.LocalDb.ILocalDbDatabaseProvider`1.GetDatabaseLogFilePath')
  - [GetDatabasePath()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-ILocalDbDatabaseProvider`1-GetDatabasePath 'RecordPoint.Connectors.SDK.Databases.LocalDb.ILocalDbDatabaseProvider`1.GetDatabasePath')
- [InitialCreate](#T-RecordPoint-Connectors-SDK-Databases-LocalDb-Migrations-InitialCreate 'RecordPoint.Connectors.SDK.Databases.LocalDb.Migrations.InitialCreate')
  - [BuildTargetModel(modelBuilder)](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-Migrations-InitialCreate-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder- 'RecordPoint.Connectors.SDK.Databases.LocalDb.Migrations.InitialCreate.BuildTargetModel(Microsoft.EntityFrameworkCore.ModelBuilder)')
  - [Down(migrationBuilder)](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-Migrations-InitialCreate-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RecordPoint.Connectors.SDK.Databases.LocalDb.Migrations.InitialCreate.Down(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
  - [Up(migrationBuilder)](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-Migrations-InitialCreate-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder- 'RecordPoint.Connectors.SDK.Databases.LocalDb.Migrations.InitialCreate.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)')
- [LocalDbConnectorDatabaseBuilderExtensions](#T-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseBuilderExtensions 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbConnectorDatabaseBuilderExtensions')
  - [UseLocalDbConnectorDatabase(hostBuilder)](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseBuilderExtensions-UseLocalDbConnectorDatabase-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbConnectorDatabaseBuilderExtensions.UseLocalDbConnectorDatabase(Microsoft.Extensions.Hosting.IHostBuilder)')
- [LocalDbConnectorDatabaseOptions](#T-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseOptions 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbConnectorDatabaseOptions')
  - [DEFAULT_DATABASE_NAME](#F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseOptions-DEFAULT_DATABASE_NAME 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbConnectorDatabaseOptions.DEFAULT_DATABASE_NAME')
  - [SECTION_NAME](#F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseOptions-SECTION_NAME 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbConnectorDatabaseOptions.SECTION_NAME')
  - [DatabaseName](#P-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseOptions-DatabaseName 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbConnectorDatabaseOptions.DatabaseName')
- [LocalDbConnectorDatabaseProvider](#T-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseProvider 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbConnectorDatabaseProvider')
  - [#ctor(systemContext,databaseOptions)](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseProvider-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseOptions}- 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbConnectorDatabaseProvider.#ctor(RecordPoint.Connectors.SDK.Context.ISystemContext,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbConnectorDatabaseOptions})')
  - [_databaseOptions](#F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseProvider-_databaseOptions 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbConnectorDatabaseProvider._databaseOptions')
  - [CreateDbContext()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseProvider-CreateDbContext 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbConnectorDatabaseProvider.CreateDbContext')
  - [GetDatabaseName()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseProvider-GetDatabaseName 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbConnectorDatabaseProvider.GetDatabaseName')
- [LocalDbConnectorDbContext](#T-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDbContext 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbConnectorDbContext')
  - [#ctor(options)](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDbContext-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext}- 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbConnectorDbContext.#ctor(Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext})')
- [LocalDbDatabaseProvider\`1](#T-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1')
  - [#ctor(systemContext)](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext- 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.#ctor(RecordPoint.Connectors.SDK.Context.ISystemContext)')
  - [DEFAULT_DATABASE_NAME](#F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-DEFAULT_DATABASE_NAME 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.DEFAULT_DATABASE_NAME')
  - [DEFAULT_LOCALDB_NAME](#F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-DEFAULT_LOCALDB_NAME 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.DEFAULT_LOCALDB_NAME')
  - [LOCALDB_SYSTEM_NAME](#F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-LOCALDB_SYSTEM_NAME 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.LOCALDB_SYSTEM_NAME')
  - [SQL_ATTACH_SCRIPT](#F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-SQL_ATTACH_SCRIPT 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.SQL_ATTACH_SCRIPT')
  - [SQL_CREATE_SCRIPT](#F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-SQL_CREATE_SCRIPT 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.SQL_CREATE_SCRIPT')
  - [SQL_DETACH_SCRIPT](#F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-SQL_DETACH_SCRIPT 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.SQL_DETACH_SCRIPT')
  - [_readySource](#F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-_readySource 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1._readySource')
  - [_systemContext](#F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-_systemContext 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1._systemContext')
  - [AttachAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-AttachAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.AttachAsync(System.Threading.CancellationToken)')
  - [CleanupAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-CleanupAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.CleanupAsync(System.Threading.CancellationToken)')
  - [CreateDbContext()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-CreateDbContext 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.CreateDbContext')
  - [DetachAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-DetachAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.DetachAsync(System.Threading.CancellationToken)')
  - [Exists()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-Exists 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.Exists')
  - [GetConnectionString()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetConnectionString 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.GetConnectionString')
  - [GetContextOptionsBuilder()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetContextOptionsBuilder 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.GetContextOptionsBuilder')
  - [GetDataDirectory()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDataDirectory 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.GetDataDirectory')
  - [GetDataPath()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDataPath 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.GetDataPath')
  - [GetDatabaseFileName()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDatabaseFileName 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.GetDatabaseFileName')
  - [GetDatabaseLogFileName()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDatabaseLogFileName 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.GetDatabaseLogFileName')
  - [GetDatabaseLogFilePath()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDatabaseLogFilePath 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.GetDatabaseLogFilePath')
  - [GetDatabaseName()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDatabaseName 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.GetDatabaseName')
  - [GetDatabasePath()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDatabasePath 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.GetDatabasePath')
  - [GetDatabaseStatusFileName()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDatabaseStatusFileName 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.GetDatabaseStatusFileName')
  - [GetDatabaseStatusFilePath()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDatabaseStatusFilePath 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.GetDatabaseStatusFilePath')
  - [GetExternalSystemName()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetExternalSystemName 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.GetExternalSystemName')
  - [GetSqlDatabaseScript(scriptName,parameters)](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetSqlDatabaseScript-System-String,System-Collections-Generic-Dictionary{System-String,System-String}- 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.GetSqlDatabaseScript(System.String,System.Collections.Generic.Dictionary{System.String,System.String})')
  - [GetSqlServer()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetSqlServer 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.GetSqlServer')
  - [GetSqlServerConnectionString()](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetSqlServerConnectionString 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.GetSqlServerConnectionString')
  - [PrepareAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-PrepareAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.PrepareAsync(System.Threading.CancellationToken)')
  - [ReadyAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-ReadyAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.ReadyAsync(System.Threading.CancellationToken)')
  - [RemoveAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-RemoveAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.RemoveAsync(System.Threading.CancellationToken)')
  - [SetReady(exception)](#M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-SetReady-System-Exception- 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbDatabaseProvider`1.SetReady(System.Exception)')

<a name='T-RecordPoint-Connectors-SDK-Databases-LocalDb-DesignTimeDbContextFactory'></a>
## DesignTimeDbContextFactory `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.LocalDb

##### Summary

The design time db context factory.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-DesignTimeDbContextFactory-CreateDbContext-System-String[]-'></a>
### CreateDbContext(args) `method`

##### Summary

Creates db context.

##### Returns

A ConnectorDbContext

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| args | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') | The args. |

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-DesignTimeDbContextFactory-GetContextOptionsBuilder'></a>
### GetContextOptionsBuilder() `method`

##### Summary

Get context options builder.

##### Returns

DbContextOptionsBuilder<ConnectorDbContext>

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-LocalDb-ILocalDbDatabaseProvider`1'></a>
## ILocalDbDatabaseProvider\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.LocalDb

##### Summary

Defines extensions to the database provider specifically for LocalDb

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TDbContext |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-ILocalDbDatabaseProvider`1-GetDatabaseLogFilePath'></a>
### GetDatabaseLogFilePath() `method`

##### Summary

Get the path to the database log file

##### Returns

File path

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-ILocalDbDatabaseProvider`1-GetDatabasePath'></a>
### GetDatabasePath() `method`

##### Summary

Get the path to the database file

##### Returns

File path

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-LocalDb-Migrations-InitialCreate'></a>
## InitialCreate `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.LocalDb.Migrations

##### Summary

The initial create.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-Migrations-InitialCreate-BuildTargetModel-Microsoft-EntityFrameworkCore-ModelBuilder-'></a>
### BuildTargetModel(modelBuilder) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| modelBuilder | [Microsoft.EntityFrameworkCore.ModelBuilder](#T-Microsoft-EntityFrameworkCore-ModelBuilder 'Microsoft.EntityFrameworkCore.ModelBuilder') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-Migrations-InitialCreate-Down-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Down(migrationBuilder) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| migrationBuilder | [Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder](#T-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder 'Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-Migrations-InitialCreate-Up-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder-'></a>
### Up(migrationBuilder) `method`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| migrationBuilder | [Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder](#T-Microsoft-EntityFrameworkCore-Migrations-MigrationBuilder 'Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder') |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseBuilderExtensions'></a>
## LocalDbConnectorDatabaseBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.LocalDb

##### Summary

LocalDb host builder extensions

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseBuilderExtensions-UseLocalDbConnectorDatabase-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseLocalDbConnectorDatabase(hostBuilder) `method`

##### Summary

Use an Sql Server Local DB as the connector database

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | App host builder |

<a name='T-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseOptions'></a>
## LocalDbConnectorDatabaseOptions `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.LocalDb

##### Summary

The local db connector database options.

<a name='F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseOptions-DEFAULT_DATABASE_NAME'></a>
### DEFAULT_DATABASE_NAME `constants`

##### Summary

Default database name

<a name='F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseOptions-SECTION_NAME'></a>
### SECTION_NAME `constants`

##### Summary

Configuration section name

<a name='P-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseOptions-DatabaseName'></a>
### DatabaseName `property`

##### Summary

Name of the database

<a name='T-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseProvider'></a>
## LocalDbConnectorDatabaseProvider `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.LocalDb

##### Summary

The local db connector database provider.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseProvider-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseOptions}-'></a>
### #ctor(systemContext,databaseOptions) `constructor`

##### Summary

Initializes a new instance of the [LocalDbConnectorDatabaseProvider](#T-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseProvider 'RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbConnectorDatabaseProvider') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| databaseOptions | [Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbConnectorDatabaseOptions}](#T-Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseOptions} 'Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Databases.LocalDb.LocalDbConnectorDatabaseOptions}') | The database options. |

<a name='F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseProvider-_databaseOptions'></a>
### _databaseOptions `constants`

##### Summary

The database options.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseProvider-CreateDbContext'></a>
### CreateDbContext() `method`

##### Summary

Creates db context.

##### Returns

A ConnectorDbContext

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDatabaseProvider-GetDatabaseName'></a>
### GetDatabaseName() `method`

##### Summary

Get database name.

##### Returns

A string

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDbContext'></a>
## LocalDbConnectorDbContext `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.LocalDb

##### Summary

Sqlite Connector Database Context

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbConnectorDbContext-#ctor-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext}-'></a>
### #ctor(options) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| options | [Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext}](#T-Microsoft-EntityFrameworkCore-DbContextOptions{RecordPoint-Connectors-SDK-Databases-ConnectorDbContext} 'Microsoft.EntityFrameworkCore.DbContextOptions{RecordPoint.Connectors.SDK.Databases.ConnectorDbContext}') |  |

<a name='T-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1'></a>
## LocalDbDatabaseProvider\`1 `type`

##### Namespace

RecordPoint.Connectors.SDK.Databases.LocalDb

##### Summary

The local db database provider.

##### Generic Types

| Name | Description |
| ---- | ----------- |
| TDbContext |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext-'></a>
### #ctor(systemContext) `constructor`

##### Summary

Initializes a new instance of the class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |

<a name='F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-DEFAULT_DATABASE_NAME'></a>
### DEFAULT_DATABASE_NAME `constants`

##### Summary

The DEFAULT DATABASE NAME.

<a name='F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-DEFAULT_LOCALDB_NAME'></a>
### DEFAULT_LOCALDB_NAME `constants`

##### Summary

The DEFAULT LOCALDB NAME.

<a name='F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-LOCALDB_SYSTEM_NAME'></a>
### LOCALDB_SYSTEM_NAME `constants`

##### Summary

The LOCALDB SYSTEM NAME.

<a name='F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-SQL_ATTACH_SCRIPT'></a>
### SQL_ATTACH_SCRIPT `constants`

##### Summary

The SQL ATTACH SCRIPT.

<a name='F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-SQL_CREATE_SCRIPT'></a>
### SQL_CREATE_SCRIPT `constants`

##### Summary

The SQL CREATE SCRIPT.

<a name='F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-SQL_DETACH_SCRIPT'></a>
### SQL_DETACH_SCRIPT `constants`

##### Summary

The SQL DETACH SCRIPT.

<a name='F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-_readySource'></a>
### _readySource `constants`

##### Summary

The ready source.

<a name='F-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-_systemContext'></a>
### _systemContext `constants`

##### Summary

The system context.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-AttachAsync-System-Threading-CancellationToken-'></a>
### AttachAsync(cancellationToken) `method`

##### Summary

Attach the database from the local db server

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-CleanupAsync-System-Threading-CancellationToken-'></a>
### CleanupAsync(cancellationToken) `method`

##### Summary

Database cleanup

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-CreateDbContext'></a>
### CreateDbContext() `method`

##### Summary

Create a connector db context

##### Returns

Connector db context

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-DetachAsync-System-Threading-CancellationToken-'></a>
### DetachAsync(cancellationToken) `method`

##### Summary

Detach the database from the local db server

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-Exists'></a>
### Exists() `method`

##### Summary

Does the database exist?

##### Returns

True if the database exists, otherwise false

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetConnectionString'></a>
### GetConnectionString() `method`

##### Summary

Get the connection string for a database

##### Returns

Connection string for the database

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetContextOptionsBuilder'></a>
### GetContextOptionsBuilder() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDataDirectory'></a>
### GetDataDirectory() `method`

##### Summary

Get the data directory

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDataPath'></a>
### GetDataPath() `method`

##### Summary

Data path

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDatabaseFileName'></a>
### GetDatabaseFileName() `method`

##### Summary

Database filename

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDatabaseLogFileName'></a>
### GetDatabaseLogFileName() `method`

##### Summary

Database log file

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDatabaseLogFilePath'></a>
### GetDatabaseLogFilePath() `method`

##### Summary

Full path of database log

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDatabaseName'></a>
### GetDatabaseName() `method`

##### Summary

Required override to get the database name

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDatabasePath'></a>
### GetDatabasePath() `method`

##### Summary

Get the full path to the database database

##### Returns

Path to the selected database

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDatabaseStatusFileName'></a>
### GetDatabaseStatusFileName() `method`

##### Summary

Workqueue status file

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetDatabaseStatusFilePath'></a>
### GetDatabaseStatusFilePath() `method`

##### Summary

Full path of database status file

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetExternalSystemName'></a>
### GetExternalSystemName() `method`

##### Summary

Get the external system name

##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetSqlDatabaseScript-System-String,System-Collections-Generic-Dictionary{System-String,System-String}-'></a>
### GetSqlDatabaseScript(scriptName,parameters) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| scriptName | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| parameters | [System.Collections.Generic.Dictionary{System.String,System.String}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.Dictionary 'System.Collections.Generic.Dictionary{System.String,System.String}') |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [RecordPoint.Connectors.SDK.RequiredValueNullException](#T-RecordPoint-Connectors-SDK-RequiredValueNullException 'RecordPoint.Connectors.SDK.RequiredValueNullException') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetSqlServer'></a>
### GetSqlServer() `method`

##### Summary

Sql server name

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-GetSqlServerConnectionString'></a>
### GetSqlServerConnectionString() `method`

##### Summary

Connection string of the server itself

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-PrepareAsync-System-Threading-CancellationToken-'></a>
### PrepareAsync(cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-ReadyAsync-System-Threading-CancellationToken-'></a>
### ReadyAsync(cancellationToken) `method`

##### Summary

Called by dependant services to determine if the database is ready for operation.

##### Returns

Readiness Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token |

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-RemoveAsync-System-Threading-CancellationToken-'></a>
### RemoveAsync(cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Databases-LocalDb-LocalDbDatabaseProvider`1-SetReady-System-Exception-'></a>
### SetReady(exception) `method`

##### Summary

Set the readiness for this service

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| exception | [System.Exception](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Exception 'System.Exception') | Exception that indicates that the service has critically failed |
