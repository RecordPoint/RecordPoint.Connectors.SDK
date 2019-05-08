#***********************************************
# Generates Models from the Web Swagger.json
#***********************************************

param (
    [switch]$useLocal = $false
 )

$swaggerUrl = "https://localhost:44366/connector/swagger/v1/swagger.json"
$swaggerOutputPath = $PSScriptRoot + "\swagger.json"

[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12

if (-not ([System.Management.Automation.PSTypeName]'TrustAllCertsPolicy').Type)
{
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

# Download the connector API swagger
# Wait til we get a Response
$count = 0
$content = ""
if (-not $useLocal) {
    while($count -lt 5)
    {
    	try
    	{
    		$content = Invoke-WebRequest -Uri $swaggerUrl
    		break
    	}
    	catch
    	{
    		$count++
    	}
    }
    if($count -ge 5)
    {
    	Write-Host "Web Request did not complete" -ForegroundColor Red
    	pause
    	exit 1
    }
    
    if($content.BaseResponse.StatusCode -ne 200)
    {
    	Write-Host "Web Request failed with "$content.BaseResponse.StatusCode  -ForegroundColor Red
    	pause
    	exit 1
    }

    #Save the JSON to a local file
    [System.IO.File]::WriteAllText($swaggerOutputPath, $content)
}

$autoRestOutput = $PSScriptRoot + "\AutoRestGenerated"
autorest --input-file=$($swaggerOutputPath) --csharp --add-credentials --output-folder=$($autoRestOutput) --override-client-name=ApiClient --output-file=ApiClient.cs --namespace=RecordPoint.Connectors.SDK.Client
	