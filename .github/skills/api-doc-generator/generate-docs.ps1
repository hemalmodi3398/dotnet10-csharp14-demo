Write-Host "Scanning Minimal API endpoints..."

$repoRoot = Resolve-Path (Join-Path $PSScriptRoot "../../..")

$programFiles = Get-ChildItem -Path $repoRoot -Recurse -Filter "Program.cs"

if ($programFiles.Count -eq 0)
{
    Write-Warning "No Program.cs files were found."
    return
}

foreach ($file in $programFiles)
{
    Write-Host "Found endpoint definitions in: $($file.FullName)"
}

Write-Host "Endpoint discovery completed."
throw "Documentation generation is not yet implemented."
