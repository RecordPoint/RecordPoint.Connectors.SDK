#***********************************************
# Generates Models from the Web Swagger.json
#***********************************************

param (
    [switch]$useLocal = $false
)

$swaggerUrl = "https://localhost:44366/connector/swagger/v1/swagger.json"
$swaggerOutputPath = $PSScriptRoot + "\swagger.json"
$abstractionsModelsPath = Join-Path $PSScriptRoot "..\..\RecordPoint.Connectors.SDK.Client.Abstractions\Models\ClientModels.cs"
$requiredNodeVersion = "16.20.2"
$requiredAutoRestVersion = "2.0.4413"

[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12

if (-not ([System.Management.Automation.PSTypeName]'TrustAllCertsPolicy').Type) {
    add-type @"
        using System.Net;
        using System.Security.Cryptography.X509Certificates;
        public class TrustAllCertsPolicy : ICertificatePolicy {
            public bool CheckValidationResult(
                ServicePoint srvPoint, X509Certificate certificate,
                WebRequest request, int certificateProblem) {
                return true;
            }
        }
"@
}
[System.Net.ServicePointManager]::CertificatePolicy = New-Object TrustAllCertsPolicy

$isNodeVersionInstalled = nvm list | Select-String -Pattern ".*$([regex]::Escape($requiredNodeVersion)).*"
if (-not [bool]$isNodeVersionInstalled) {
    Write-Host "Required node version $requiredNodeVersion is not installed. Installing ..." -ForegroundColor Yellow
    nvm install $requiredNodeVersion
    Write-Host "Installed. Please restart Powershell and rerun the script." -ForegroundColor Red
    pause
    exit 1
}

# Download the connector API swagger
# Wait til we get a Response
$count = 0
$content = ""
if (-not $useLocal) {
    while ($count -lt 5) {
        Write-Host "Calling Eiger Management API ..."

        try {
            $content = Invoke-WebRequest -Uri $swaggerUrl
            break
        }
        catch {
            $count++
        }
    }
    if ($count -ge 5) {
        Write-Host "Web Request did not complete" -ForegroundColor Red
        pause
        exit 1
    }
    
    if ($content.BaseResponse.StatusCode -ne 200) {
        Write-Host "Web Request failed with "$content.BaseResponse.StatusCode  -ForegroundColor Red
        pause
        exit 1
    }

    #Save the JSON to a local file
    [System.IO.File]::WriteAllText($swaggerOutputPath, $content)
    Write-Host "Swagger JSON retrieved from API."
}

$currentNodeVersion = nvm current
if ($currentNodeVersion -ne $requiredNodeVersion) {
    nvm use $requiredNodeVersion
}

Write-Host "Running AutoRest $requiredAutoRestVersion via npx ..."

try {
    $autoRestOutput = $PSScriptRoot + "\AutoRestGenerated"
    npx --yes autorest@$requiredAutoRestVersion --use=@microsoft.azure/autorest.csharp@2.3.79 --use=@microsoft.azure/autorest.modeler@2.3.55 --input-file=$($swaggerOutputPath) --csharp --add-credentials --output-folder=$($autoRestOutput) --override-client-name=ApiClient --output-file=ApiClient.cs --namespace=RecordPoint.Connectors.SDK.Client
}
catch {
    Write-Host "Error occurred running AutoRest: $_" -ForegroundColor Red
    nvm use $currentNodeVersion
    pause
    exit 1
}

Write-Host "AutoRest run successfully."

$generatedClientPath = Join-Path $autoRestOutput "ApiClient.cs"
$generatedLines = Get-Content $generatedClientPath

$modelNamespacePattern = '^\s*namespace\s+RecordPoint\.Connectors\.SDK\.Client\.Models\s*$'
$firstModelNamespaceIndex = -1

for ($i = 0; $i -lt $generatedLines.Count; $i++) {
    if ($generatedLines[$i] -match $modelNamespacePattern) {
        $firstModelNamespaceIndex = $i
        break
    }
}

$modelLines = New-Object System.Collections.Generic.List[string]
$clientLines = New-Object System.Collections.Generic.List[string]

if ($firstModelNamespaceIndex -ge 0) {
    if ($firstModelNamespaceIndex -gt 0) {
        foreach ($line in $generatedLines[0..($firstModelNamespaceIndex - 1)]) {
            $clientLines.Add($line)
        }
    }

    foreach ($line in $generatedLines[$firstModelNamespaceIndex..($generatedLines.Count - 1)]) {
        $modelLines.Add($line)
    }

    Set-Content -Path $generatedClientPath -Value $clientLines
    Set-Content -Path $abstractionsModelsPath -Value $modelLines
    Write-Host "Moved generated model namespaces into $abstractionsModelsPath" -ForegroundColor Green
}
else {
    Write-Host "No model namespaces were found to move. ApiClient.cs was left unchanged." -ForegroundColor Yellow
}

Write-Host "The generated client depends on the in-repo compatibility runtime at RecordPoint.Connectors.SDK.Client.Abstractions/Compatibility/MicrosoftRestCompatibility.cs (no Microsoft.Rest.ClientRuntime package required)." -ForegroundColor Yellow
