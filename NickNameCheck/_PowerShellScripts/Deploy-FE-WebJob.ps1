#
# Deploy_FE_WebJob.ps1
#

param(
    [Parameter(Mandatory = $true)]
	[string]$slot
)

$rootDir = Split-Path -parent $PSCommandPath

$archiveFolderName = "$rootDir\FE-WebJob-Archive"
$outputArchiveFile = "$rootDir\FE-azurewebsitejob.zip"

# Check if archive folder exists, remove if it does.
If (Test-Path $archiveFolderName){
	Write-Host "Removing old $archiveFolderName folder"
	Remove-Item $archiveFolderName -Force -Recurse
}

# Check if previous archive file exists, remove if it does.
If (Test-Path $outputArchiveFile){
	Write-Host "Removing old $outputArchiveFile archive file"
	Remove-Item $outputArchiveFile -Force -Recurse
}

# Create archive folder.
New-Item -ItemType Directory -Force -Path $archiveFolderName | Out-Null

# Copy WebJob to archive folder.
$srcFiles = "$rootDir\..\Scripts\WebJob\*"
Copy-Item $srcFiles -Destination $archiveFolderName -Exclude $exclude -Force -Recurse

# Copy settings.job to archive folder.
$srcSettingsFile = "$rootDir\FE-job-settings.job"
$dstSettingsFile = "$archiveFolderName\settings.job"
Copy-Item $srcSettingsFile -Destination $dstSettingsFile -Force

# Create zip archive.
$archiveFiles = "$archiveFolderName\*"
Compress-Archive -Path $archiveFiles -DestinationPath $outputArchiveFile -Force

# Deploy AzureWebsiteJob.
Write-Host "Deploying Azure WebJob"
$site = Get-AzureWebsite -Name "webappkv" -Slot $slot
# $site = Get-AzureWebsite -Name "fr8dev"

New-AzureWebsiteJob -Name $site[0].Name `
  -JobName "FE-WebJob" `
  -JobType Triggered `
  -JobFile $outputArchiveFile `
  -Slot $slot;

# Remove zip archive.
# Write-Host "Removing current deployment zip archive"
# Remove-Item $outputArchiveFile -Force -Recurse

# Remove current archive folder.
Remove-Item $archiveFolderName -Force -Recurse
