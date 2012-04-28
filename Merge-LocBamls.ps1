<#
.SYNOPSIS
Merge-LocBaml.ps1 merges two LocBaml csv files.

.DESCRIPTION
Merge-LocBaml.ps1 may be used to merge to LocBaml csv files together. This 
is useful for updating an existing translation.

.PARAMETER oldLocation
The location of the old csv file.

.PARAMETER newLocation
The location of the new csv file.

.PARAMETER mergedLocation
The location that the merged file should be written to. If the file already
exists, it will be overwritten.

.LINK
WPF Globalization and Localization Overview: http://msdn.microsoft.com/en-us/library/ms788718.aspx

.LINK
LocBaml source direct download: http://archive.msdn.microsoft.com/Project/Download/FileDownload.aspx?ProjectName=wpfsamples&DownloadId=9333

.EXAMPLE
.\Merge-LocBamls.ps1 .\fr.csv.old .\fr.csv .\fr.merge.csv

#>

param ([Parameter(Mandatory=$TRUE)][string]$oldLocation,[Parameter(Mandatory=$TRUE)][string]$newLocation,[Parameter(Mandatory=$TRUE)][string]$mergedLocation)

$header = "BamlName","ResourceKey","LocalizationCategory","Readable","Modifiable","Comments","Value"

$oldEntries = Import-Csv $oldLocation -Header $header
$newEntries = Import-Csv $newLocation -Header $header
$mergedEntries = @()

$oldEntries | ForEach-Object { if (($newEntries | Select -ExpandProperty ResourceKey) -contains $_.ResourceKey) { 
    # add new entry 
    $outer = $_.ResourceKey
    $mergedEntries += ($newEntries | Where { $_.ResourceKey -eq $outer})
} 
else { 
    # add old entry
    $outer = $_.ResourceKey
    $mergedEntries += ($oldEntries | Where { $_.ResourceKey -eq $outer})    
}}

$newEntries | ForEach-Object { if (($mergedEntries | Select -ExpandProperty ResourceKey) -notcontains $_.ResourceKey ){
    $outer = $_.ResourceKey
    $mergedEntries += ($newEntries | Where { $_.ResourceKey -eq $outer})
}}

$mergedEntries | Select-Object BamlName,ResourceKey,LocalizationCategory,Readable,Modifiable,Comments,Value | ConvertTo-Csv -OutVariable mergedEntriesCSV -NoTypeInformation

if (Test-Path $mergedLocation) {
    Remove-Item $mergedLocation
}

$mergedEntriesCSV[1..($mergedEntriesCSV.count - 1)] | ForEach-Object { Add-Content -Value $_ -Path $mergedLocation -Encoding UTF8 }