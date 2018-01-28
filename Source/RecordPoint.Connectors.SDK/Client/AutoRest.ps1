#***********************************************
# Generates Models from the Web Swagger.json
#***********************************************

param(
    [string]$EigerSolutionFolder,
	[string]$FileConnectSolutionFolder
)

if([System.String]::IsNullOrEmpty($EigerSolutionFolder) -eq $true)
{
    $EigerSolutionFolder = 'C:\Eiger\Main\RPFabric\'
    write-host("EigerSolutionFolder parameter empty, using default route '$EigerSolutionFolder'");
}
$eigerRootPath = $EigerSolutionFolder

if([System.String]::IsNullOrEmpty($FileConnectSolutionFolder) -eq $true)
{
    $FileConnectSolutionFolder = 'C:\FileConnect\'
    write-host("FileConnectSolutionFolder parameter empty, using default route '$FileConnectSolutionFolder'");
}
$fileConnectRootPath = $FileConnectSolutionFolder

$paths = @()
	
# The connector api swagger
$paths += @{
		name = "ConnectorsAPI"
		webPath = "src\RPFabric.Web.API.Connector\bin\AnyCPU\Debug\net46\win7-x64"
		webExePath = "RPFabric.Web.API.Connector.exe"
		swaggerPath = "/connector/swagger/v1/swagger.json"
	}

$autoRestOutput = [System.IO.Path]::Combine($fileConnectRootPath, "Source\RecordPoint.Common\Client\AutoRestGenerated")
$args = "--protocols http:91,https:9091"
$baseUrl = "https://localhost:9091"
$swaggerOutputPath = [System.IO.Path]::Combine($env:TEMP, "swagger.json")

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

foreach($path in $paths){

	$webPath = [System.IO.Path]::Combine($eigerRootPath, $path.webPath)
	$webExePath = [System.IO.Path]::Combine($webPath, $path.webExePath)

	Write-Host "Starting $webExePath to get swagger.json"

	$swaggerUrl = $baseUrl + $path.swaggerPath
	$procWeb = Start-Process -FilePath $webExePath -WorkingDirectory $webPath -ArgumentList $args -PassThru

	#Wait til we get a Response
	$count = 0
	$content = ""
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
		Stop-Process $procWeb.Id
		$procWeb.WaitForExit()
		pause
		exit 1
	}

	if($content.BaseResponse.StatusCode -ne 200)
	{
		Write-Host "Web Request failed with "$content.BaseResponse.StatusCode  -ForegroundColor Red
		Stop-Process $procWeb.Id
		$procWeb.WaitForExit()
		pause
		exit 1
	}

	#Save the JSON to a local file
	[System.IO.File]::WriteAllText($swaggerOutputPath, $content)

	#Stop Kestrel
	Stop-Process $procWeb.Id
	$procWeb.WaitForExit()
	Write-Host "swagger.json is copied to $swaggerOutputPath. Closing $webExePath, running AutoRest"

	$autoRestExe = "autorest"
	$outputFile = "ApiClient.cs"

	$outputFilePath =[System.IO.Path]::Combine($autoRestOutput, $outputFile)
    $filesExist = Test-Path $outputFilePath
    if($fileExist){
		Write-Host "Removing existing file $outputFilePath"
        Remove-Item $([System.IO.Path]::Combine($autoRestOutput, $outputFile))
    }

	$autoRestArgs = " --input-file=$($swaggerOutputPath) --csharp --add-credentials --output-folder=$($autoRestOutput) --override-client-name=ApiClient --output-file=$($outputFile) --namespace=RecordPoint.Connectors.Client"
	Write-Host "Running autorest with arguments: $autoRestArgs"
	#Start AutoRest
	$procAutoRest = Start-Process -FilePath $autoRestExe -ArgumentList $autoRestArgs -PassThru
	$procAutoRest.WaitForExit()
}

Write-Host "Auto Rest Completed for $autoRestOutput. Output not what you expected? Double check that you rebuilt the API project."
pause