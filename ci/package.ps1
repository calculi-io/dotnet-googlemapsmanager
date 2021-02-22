$archiveName = $env:APPLICATION_NAME + ".zip"

msbuild /t:Rebuild /p:Configuration=Release

Remove-Item $archiveName -ErrorAction Ignore

Write-Output "Creating Archive: $archiveName"

$Command=" & 'C:\Program Files\7-Zip\7z.exe' a $archiveName .\$env:CSPROJECT_NAME"
Write-Output "Running: $Command"
$Command | Invoke-Expression

if(-Not(Test-Path $archiveName)){
    Write-Output "WARNING: zip file $archiveName was not created"
}
