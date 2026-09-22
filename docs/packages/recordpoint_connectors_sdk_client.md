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
- node version 16.20.2
- AutoRest CLI 2.0.4413 (invoked via `npx`, no global install required) -> [AutoRest on GitHub](https://github.com/Azure/autorest)
- AutoRest csharp extension 2.3.79
- AutoRest modeler extension 2.3.55

It is recommended to install the correct version of node by using a node version manager (nvm).

#### AutoRest.ps1
To generate the Connector Api Client code, there is a powershell script that can be run.
It is set to retrieve the swagger payload from a locally running instance of the Connector Api (`https://localhost:44366/connector/swagger/v1/swagger.json`).
You can change the URL within the script to point to any other running instance of the Connector Api.

_Note: You can pass an argument (`-useLocal`) to the script to prevent it from downloading the swagger payload and instead use a local copy located in the same location as the script._

#### Running the Script
1. Ensure you have the latest Connectors SDK code locally.
2. From a terminal, navigate to the `Client` folder within the `RecordPoint.Connectors.SDK.Client` project.
3. Run `AutoRest.ps1` or `AutoRest.ps1 -useLocal`.

The script pins AutoRest to `2.0.4413` via `npx`, and also pins the AutoRest csharp/modeler extensions, so generation is deterministic and does not require a global AutoRest install.

The script also automatically splits generated output by moving model namespaces into `RecordPoint.Connectors.SDK.Client.Abstractions/Models/ClientModels.cs`, while keeping implementation code in `RecordPoint.Connectors.SDK.Client/Client/AutoRestGenerated/ApiClient.cs`.

After regeneration/splitting, the generated code uses the in-repo compatibility runtime in `RecordPoint.Connectors.SDK.Client.Abstractions/Compatibility/MicrosoftRestCompatibility.cs`, so `Microsoft.Rest.ClientRuntime` is no longer required.

## Troubleshoot

Using different versions of node and AutoRest often result in varying errors. Therefore, ensure the versions mentioned above are installed correctly.

If the script still fails, reset the local AutoRest cache and rerun it under Node 16.20.2.

To check the pinned AutoRest version used by the script:

```
npx --yes autorest@2.0.4413 --version
```

If errors are still occurring on the AutoRest side then clear/reset your local AutoRest cache and retry:

```
npx --yes autorest@2.0.4413 --reset
```
