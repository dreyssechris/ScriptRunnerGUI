# get Profile path
$profilePath = $PROFILE

# VS-Funktion to be added
$function = @'
function vs {
    param(
        [string]$Path
    )

    $devenv = "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.exe"

    if ($Path -eq $null) {
        # Just start blank VS
        Start-Process $devenv
        # Terminal Output
        Write-Host "Started Visual Studio without path" -ForegroundColor Yellow
    } else {
        # Resolve path and open in VS
        Start-Process $devenv -ArgumentList(Resolve-Path $Path)
        # Terminal Output
        Write-Host "Started Visual Studio with path" -ForegroundColor Green
    }
}
'@

# check if PS1-Profile exists, if not create it
if (-not (Test-Path $profilePath)) {
    New-Item -ItemType File -Path $profilePath
    Write-Host "PS1 Profile file was created: $profilePath" -ForegroundColor Green
}

# get the current content of the file as one single formatted string
$currentContent = Get-Content $profilePath -Raw

# check if function already exists
if ($currentContent -notmatch 'function\s+vs\s*{') {
    $newContent = $function + "`n`n" + $currentContent
    Set-Content $profilePath $newContent -Encoding UTF8
    Write-Host "VS Function was added to the Powershell Profile" -ForegroundColor Green
} else {
    Write-Host "VS Function already exists in this Powershell Profile. `nNothing was added." -ForegroundColor Yellow
}