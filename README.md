# Records365 vNext Connectors SDK 
Get started quickly with writing new connectors for Records365 vNext using this .NET library.

This library uses [Semantic Versioning](https://semver.org/).

# Quickstart Guide

If you're a developer looking to quickly get up and running with the basics of Records365 vNext Connector development, this is the guide for you. 
You must have access to a Records365 vNext tenant before you begin.

## Overview 

In this guide you will create a simple connector that

*  Submits items to the Records365 vNext Connector API
*  Polls for notifications from Records365 vNext (via the Connector API)

### Authentication 

Records365 vNext Connectors use Azure Active Directory to authenticate to the Records365 vNext Connector API. 
Connectors use the [OAuth 2.0 client credentials grant flow](https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-protocols-oauth-service-to-service) 
to obtain a bearer token from Azure Active Directory. The bearer token is then used in each call to the Connector API.

# Let's Get Started! 

## Create an App in Azure Active Directory 

In the [Azure Portal](https://portal.azure.com), create a new App Registration in the Azure Active Directory that is linked to your Records365 vNext tenant. 
For example, if you log in to your Records365 vNext tenant as `john.doe@mytenant.onmicrosoft.com`, then create 
a new App Registration in the `mytenant.onmicrosoft.com` directory. Provide the following details when creating the App Registration:

*  **Name**: Anything you like
*  **Application type**: Web app/API
*  **Sign on URL**: Any valid URL - it doesn't really matter, this value isn't used anywhere

Once the App Registration is created, take note of the value for `Application ID` and then navigate to `Settings`.

Finally, under `Keys`, create a new Key and take note of its value. 

## Register a Connector Type in Records365 vNext

In Records365 vNext, log in as an Application Administrator. Click the settings cog, then click "Connectors" on the left navigation menu. 
Click "Add Connector", then click the ellipsis menu in the top right and select "New Connector Type". Provide the following details:

*  **Name**: Name of your connector type.
*  **Short Name**: Short name of your connector type.
*  **Content Source**: Name of the content source that your connector adapts to. 
*  **Publisher**: Name of the organization that publishes your connector type.
*  **Client ID**: The `Application ID` from the Azure Active Directory App Registration created above.
*  **Allow Client ID Override**: No.
*  **Notification Method**: Pull.
*  **Notification Types**: Check "Item Destroyed", and leave all others unchecked.
*  **Logo**: Upload a 360px by 160px image.
*  **Icon**: Upload a 70px by 70px image.

Click Save. Your custom connector type will now appear in the Connectors Gallery.

## Create a Connector 

Click the Add button on your new custom connector type. A new instance of the connector will be created. 

In the flyout pane on the right, click the "Download Settings" button. Take note of the json file that is downloaded.

## Submit Some Records

Any interaction with the Records365 vNext Connector API via this SDK requires two objects - an `AuthenticationHelperSettings` and an `ApiClientFactorySettings`. 

Create an `AuthenticationHelperSettings` object:

    var authenticationHelperSettings = new AuthenticationHelperSettings
    {
        AuthenticationResource = <audience from the connector settings json>,
        ClientId = <clientId from the connector settings json>,
        ClientSecret = <Key value Azure AD App Registration>,
        TenantDomainName = <tenantDomainName from the connector settings json>
    };

Create an `ApiClientFactorySettings` object:

    var apiClientFactorySettings = new ApiClientFactorySettings
    {
        ConnectorApiUrl = <connectorApiUrl from the connector settings json>,
        ServerCertificateValidation = true
    };

To submit your first record to Records365 vNext, use the `HttpSubmitItemPipelineElement` class...

    var submitPipeline = new HttpSubmitItemPipelineElement(null);
    submitPipeline.ApiClientFactory = new ApiClientFactory();

... and submit a record like so:

    var submitContext = new SubmitContext
    {
        ConnectorConfigId = Guid.Parse("<connectorId from connector settings json"),
        TenantId = Guid.Parse("<tenantId from connector settings json>"),
        ApiClientFactorySettings = apiClientFactorySettings,
        AuthenticationHelperSettings = authenticationHelperSettings,
        CoreMetaData = new List<SubmissionMetaDataModel>()
    };

    submitContext.CoreMetaData.Add(new SubmissionMetaDataModel(Fields.ExternalId, value: "12345"));
    submitContext.CoreMetaData.Add(new SubmissionMetaDataModel(Fields.Title, value: "Record title"));
    submitContext.CoreMetaData.Add(new SubmissionMetaDataModel(Fields.Author, value: "Record author"));
    submitContext.CoreMetaData.Add(new SubmissionMetaDataModel(Fields.SourceCreatedDate, value: DateTime.UtcNow.ToString("O")));
    submitContext.CoreMetaData.Add(new SubmissionMetaDataModel(Fields.SourceLastModifiedDate, value: DateTime.UtcNow.ToString("O")));
    submitContext.CoreMetaData.Add(new SubmissionMetaDataModel(Fields.SourceCreatedBy, value: "Me"));
    submitContext.CoreMetaData.Add(new SubmissionMetaDataModel(Fields.SourceLastModifiedBy, value: "Me"));
    submitContext.CoreMetaData.Add(new SubmissionMetaDataModel(Fields.Location, value: "C:\\record.txt"));
    submitContext.CoreMetaData.Add(new SubmissionMetaDataModel(Fields.MediaType, value: "Electronic"));
    submitContext.CoreMetaData.Add(new SubmissionMetaDataModel(Fields.MimeType, value: "text/plain"));
    submitContext.CoreMetaData.Add(new SubmissionMetaDataModel(Fields.ParentExternalId, value: "12345-12345"));
    submitContext.CoreMetaData.Add(new SubmissionMetaDataModel(Fields.ContentVersion, value: "1.0"));

    await submitPipeline.Submit(submitContext);

The result of the submission can be checked on the `SubmitResult` field on the `submitContext`.


