# Quickstart Guide

If you're a developer looking to quickly get up and running with the basics of Records365 Connector development, this is the guide for you. 
You must have access to a Records365 tenant before you begin.

If you want a more detailed explanation of the Connectors SDK, start [here](../README.md).

The Reference Connector Project ([link](https://github.com/RecordPoint/RecordPoint.Connectors.Reference)) can be used as a guide for developing a connector.


### Authentication 
Connectors use Azure Active Directory to authenticate to the Records365 Connector API. 
Connectors use the [OAuth 2.0 client credentials grant flow](https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-protocols-oauth-service-to-service) 
to obtain a bearer token from Azure Active Directory. The bearer token is then used in each call to the Connector API.
Authentication is entirely handled within the Connectors SDK, you only need to provide the appropriate settings.


# Let's Get Started! 
In this guide, we will create a simple connector that performs the responsibilities listed below within a single project.

If you want to split your connector up across multiple projects that handle each responsbility individually,
you will need to duplicate the majority of the dependency injection code across each project but only configure the relevant responsibilities within each project.

The responsibilities contained within our simple connector:
* web hook notifications for receiving connector configuration changes
* content manager operation for spawning new channel discovery operations
* channel discovery operation for spawning new content synchronisation operations
* record submission operation for submitting records to Records365
* binary submission operation for submitting binaries to Record365

- [ ] You will need to create a console application and setup the connector dependencies to ensure the connector features are enabled.
- [ ] You will need to implement logic for various connector actions.
- [ ] You will need to provide appsettings for various connection strings and authentication settings.
- [ ] You will need to create an App Registration for your connector within an Azure Active Directory.
- [ ] You will need to register your Connector Type within Records365.
- [ ] You will need to create a Connector Configuration with Records365.


## Create your Connector
Create a new dotnet 8 Console Application.
Add the following packages to the project:
* RecordPoint.Connectors.SDK
* RecordPoint.Connectors.SDK.Databases.LocalDb
* RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus
* RecordPoint.Connectors.SDK.Notifications
* RecordPoint.Connectors.SDK.WebHost


### Setup Connector Dependencies
Add the following code to the Program.cs class:
```cs
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Databases.LocalDb;
using RecordPoint.Connectors.SDK.Notifications;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Observability.Null;
using RecordPoint.Connectors.SDK.Time;
using RecordPoint.Connectors.SDK.Toggles.Null;
using RecordPoint.Connectors.SDK.WebHost;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus;

namespace ReferenceConnector.Connector
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateConnectorHostBuilder(args)
                .Build()
                .Run();
        }

        private static IHostBuilder CreateConnectorHostBuilder(string[] args)
        {
            //Setup Configuration
            var configuration = ConnectorConfigurationBuilder
                .CreateConfigurationBuilder(args, Assembly.GetExecutingAssembly())
                .Build();

            //Create Host Builder
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .UseConfiguration(configuration)
                .UseSystemContext("RecordPoint", "Reference Connector", "RefConn")
                .UseSystemTime()
                .ConfigureServices(services =>
                {
                    services.AddConsoleLogging();
                });

            //Setup R365 Configuration (AppSettings)
            hostBuilder.UseR365AppSettingsConfiguration();

            //Setup Connector Database
            hostBuilder
                .UseLocalDbConnectorDatabase()
                .UseDatabaseConnectorConfigurationManager()
                .UseDatabaseChannelManager();

            //Setup Telemetry - we're not going to be persisting telemetry for our sample so just use the null provider
            hostBuilder.UseNullTelemetryTracking();

            //Setup Feature Toggles - we're not going to be using a real feature toggle provider for our sample so just use the null provider
            hostBuilder.UseNullToggleProvider();

            //Setup Work Queue
            hostBuilder
                .UseWorkManager()
                .UseWorkStateManager<DatabaseManagedWorkStatusManager>()
                .UseASBWorkQueue();

            //Setup the Connector Responsibilities
            hostBuilder
                .UseR365Integration()                                               //Configures the Records365 Connector Api client
                .UseWebHost(builder.Configuration)                                  //Configures the connector web api host
                .UseWebhookNotifications()                                          //Configures connector Web Hook notifications
                .UseContentManagerService()                                         //Configures the Content Manager operation
                .UseChannelDiscoveryOperation<ChannelDiscoveryAction>()             //Configures the Channel Discovery operation
                .UseContentSynchronisationOperation<ContentSynchronisationAction>() //Configures the Content Synchronisation operation
                .UseRecordSubmissionOperation()                                     //Configures the Record Submission operation
                .UseBinarySubmissionOperation<BinaryRetrievalAction>();             //Configures the Binary Submission operation

            return hostBuilder;
        }
    }
}
```


### Create Connector Actions
Our sample connector implements the minimum responsibilities of a connector in order to successfully submit records to Records365.
These connector responsibilities require logic (actions) to be implemented specific to your connector to identify records that need to be submitted.

For our sample connector, we need to implement the actions listed below.
The reference connector actions can be copied in order to implement a simple "file system" connector.

* IChannelDiscoveryAction - [see the Reference Connector Channel Discovery Action](../ReferenceConnector/ReferenceConnector.ChannelDiscovery/ChannelDiscoveryAction.cs)
* IContentSynchronisationAction - [see the Reference Connector Content Synchronisation Action](../ReferenceConnector/ReferenceConnector.ContentSynchronisation/ContentSynchronisationAction.cs)
* IBinaryRetrievalAction - [see the Reference Connector Binary Retrieval Action](../ReferenceConnector/ReferenceConnector.BinarySubmission/BinaryRetrievalAction.cs)


### Connector Settings
Your sample connector requires settings so it knows the address of the Records365 instance and how to authenticate, as well as connection strings to service bus and the connector database.

The code in the "Connector Dependencies" above injects the "AppSettings" provider for connector settings. This provider utilises an appsettings.json file for retrieving the required settings.

It is likely you would use the RecordPoint.Connectors.SDK.Configuration.AzureKeyVault provider in a production deployment.

```
{
  "Configuration": {
    "ClientId": "{Active Directory App Registration Application/Client Id}",
    "ClientSecret": "{Value of the client secret generated within the Azure AD App Registration}",
    "Audience": "{The audience used by the Records365 Connector Api for authentication}",
    "ConnectorApiUrl": "{Url to the Records365 Connector Api - https://localhost:44366/}"
  },
  "System": {
    "DataPathRoot": "{a disk location used to store data such as database files etc. used by LocalDb, Sqlite, Serilog}"
  },
  "ContentManager": {
    "DelaySeconds": 30,
    "ChannelDiscovery": {
      "DelaySeconds": 30,
      "BackOffEnabled": true,
      "MaxDelaySeconds": 300
    },
    "ContentSynchronisation": {
      "DelaySeconds": 30,
      "BackOffEnabled": true,
      "MaxDelaySeconds": 300
    }
  },
  "AzureServiceBusSettings": {
    "ServiceBusConnectionString": "{Connection string to the Azure Service Bus resource used by the Work Queue System}"
  }
}
```


## Run your Connector
Now that you have what should be a working Connector, you can run the code to test it.

There is still configuration that must be done within Azure Active Directory and your Records365 instance before you can submit any records.


## Create an App in Azure Active Directory 
In the [Azure Portal](https://portal.azure.com), create a new App Registration in the Azure Active Directory that is linked to your Records365 tenant. 
For example, if you log in to your Records365 tenant as `john.doe@mytenant.onmicrosoft.com`, then create a new App Registration in the `mytenant.onmicrosoft.com` directory.
Provide the following details when creating the App Registration:

*  **Name**: Anything you like
*  **Application type**: Web app/API
*  **Sign on URL**: Any valid URL - it doesn't really matter, this value isn't used anywhere

Once the App Registration is created, take note of the value for `Application (client) ID` and then navigate to `Certificates & secrets`.

Finally, under `Client secrets`, create a new client secret and take note of its value. 


## Register a Connector Type in Records365
In Records365, log in as an Application Administrator. Click the settings cog, then click "Connectors" on the left navigation menu. 
Click "Add Connector", then click the ellipsis menu in the top right and select "New Connector Type". Provide the following details:

*  **Name**: Name of your connector type.
*  **Short Name**: Short name of your connector type.
*  **Content Source**: Name of the content source that your connector adapts to. 
*  **Publisher**: Name of the organization that publishes your connector type.
*  **Client ID**: The `Application ID` from the Azure Active Directory App Registration created above.
*  **Allow Client ID Override**: No.
*  **Notification Method**: Push.
*  **Notification Authentication resource**: The `Application ID URI` from the App Registration
*  **Notification URL**: The url that your Connector exposes for webhooks.  When debugging locally, this will be https://localhost:44342/api
*  **Notification Types**: Check all items.
*  **Logo**: Upload a 360px by 160px image.
*  **Icon**: Upload a 70px by 70px image.

Click Save. Your custom connector type will now appear in the Connectors Gallery.


## Create a Connector Configuration
Click the Add button on your new custom connector type. A new instance of the connector will be created. 

Records365 will push a notification via the connectors web hook, telling the connector that a new configuration has been created.
Assuming your code is setup correctly, the Connectors SDK will handle this notification and store the configuration in your connector database.


## Submit Some Records
After you've confirmed the connector configuration has been added to the database, you should also see a record for Channel Discovery in the ManagedWorkStatuses table.

This indicates that a channel discovery operation is now running for your connector configuration.

You should now be able to add channels and records that will be identified by the various connector operations/actions, and see them submit to Records365.