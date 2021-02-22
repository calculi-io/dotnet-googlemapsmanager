$ErrorActionPreference = "Stop"
Set-PSDebug -Trace 2
import-module WebAdministration

Write-Output "Add-Type -assembly 'system.io.compression.filesystem'"
Add-Type -assembly "system.io.compression.filesystem"

$wwwroot = "C:\inetpub\wwwroot\"
$wwwdest = $wwwroot + $env:CSPROJECT_NAME
$wwwrootRemoveFiles = $wwwroot + "*"
$archiveLocation = "$env:PKG_BASE_DIR\$env:APPLICATION_NAME\$env:APPLICATION_NAME.zip"

Write-Output "Directory of  $env:PKG_BASE_DIR"
dir $env:PKG_BASE_DIR

Write-Output "Directory of  $env:PKG_BASE_DIR\$env:APPLICATION_NAME"
dir $env:PKG_BASE_DIR\$env:APPLICATION_NAME

Write-Output "wwwroot = " + $wwwroot
Write-Output "archiveLocation = " + $archiveLocation

Get-Date

Write-Output "Removing root files - Remove-Item $wwwrootRemoveFiles -Recurse"
Remove-Item $wwwrootRemoveFiles -Recurse

Write-Output "Copying site into wwwroot"
New-Item -ItemType Directory -Force -Path $wwwdest
Copy-Item -Force -Recurse $env:PKG_BASE_DIR\$env:APPLICATION_NAME\* -Destination $wwwdest

Write-Output "converting to application"
ConvertTo-WebApplication "IIS:\Sites\Default Web Site\$env:CSPROJECT_NAME"

Get-Date

Write-Output "Starting IIS - Set-Service W3SVC -StartupType Automatic"
Set-Service W3SVC -StartupType Automatic
iisreset /start

Get-Date

Write-Output "Starting site on port 80 C:\windows\system32\inetsrv\appcmd start site 'Default Web Site'"
C:\windows\system32\inetsrv\appcmd start site "Default Web Site"

Set-ItemProperty "IIS:Sites\Default Web Site" applicationPool ".NET v4.5"
Set-ItemProperty "IIS:Sites\Default Web Site\googlemapsmanager" applicationPool ".NET v4.5"

Write-Output "enabling tracing for 404"
Enable-WebRequestTracing -Name "Default Web Site" -StatusCodes "404"
