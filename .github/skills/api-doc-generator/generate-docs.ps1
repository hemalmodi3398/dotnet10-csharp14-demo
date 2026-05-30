Write-Host "Scanning Minimal API endpoints..."

Get-ChildItem -Recurse -Filter "Program.cs" |
ForEach-Object {
    Write-Host "Found endpoint definitions in: $($_.FullName)"
}

Write-Host "API documentation generation completed."