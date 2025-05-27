# RecordPoint.Connectors.SDK.Client

[RecordPoint.Connectors.SDK.Client Api Documentation](./recordpoint_connectors_sdk_client_doc.md)

The Connectors SDK communicates with Records 365 via the Connector Api.
The SDK includes a wrapper for the Connector Api within the "Client" packages, which consists of some auto-generated code.
The Abstractions package includes the underlying models, abstract classes and interfaces used by the core Client package.

This package is used internally by the SDK for item submission and should not need to be used directly by any Connector code.

## Generating the Connector Api Client

The Connector Api Client code will need to be updated when the Connector Api itself has been modified, to ensure all contracts are adhered to.

#### Pre-requisites
Exact versions of the following:
- node version 12.18.4
- autorest version 2.0.4413 -> [AutoRest on GitHub](https://github.com/Azure/autorest)

It is recommended to install the correct version of node by using a node version manager (nvm).

To install autorest:
```
npm install -g autorest@2.0.4413
```

#### AutoRest.ps1
To generate the Connector Api Client code, there is a powershell script that can be run.
It is set to retrieve the swagger payload from a locally running instance of the Connector Api (`https://localhost:44366/connector/swagger/v1/swagger.json`).
You can change the URL within the script to point to any other running instance of the Connector Api.

_Note: You can pass an argument (`-useLocal`) to the script to prevent it from downloading the swagger payload and instead use a local copy located in the same location as the script._

#### Running the Script
1. Ensure you have the latest Connectors SDK code locally.
2. From a terminal, navigate to the `Client` folder within the `RecordPoint.Connectors.SDK.Client` project.
3. Run `AutoRest.ps1` or `AutoRest.ps1 -useLocal`
4. Move the client models to the Abstractions project - see note below.

The new code should now have overwritten the `ApiClient.cs` file located under the `AutoRestGenerated` folder.

_Important: AutoRest generates both models and client logic across numerous classes, but places all this within a single file.
You MUST split the models out from the main client code in the `ApiClient.cs` file and move it to the `ClientModels.cs` file within the `RecordPoint.Connectors.SDK.Client.Abstractions` project._

## Troubleshoot

Using different versions of node and autorest often result in varying errors. Therefore, ensure the versions mentioned above are installed correctely. The following will show both the node version and the autorest version being used.

```
autorest --version
```

If errors are still occuring on the autorest side then try to reset it:

```
autorest --reset
```
