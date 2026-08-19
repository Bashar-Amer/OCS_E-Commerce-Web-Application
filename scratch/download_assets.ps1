$json = Get-Content -Raw "drive-download\manifest.json" | ConvertFrom-Json
$dest = "wwwroot\images"
if (!(Test-Path $dest)) { New-Item -ItemType Directory -Force -Path $dest | Out-Null }
$downloaded = 0
foreach ($item in $json.images) {
    $fn = $item.filename
    $url = $item.thumbnail_url
    if ($fn -and $url) {
        $target = Join-Path $dest $fn
        if (!(Test-Path $target)) {
            try {
                Invoke-WebRequest -Uri $url -OutFile $target -TimeoutSec 15 -UserAgent "Mozilla/5.0"
                $downloaded++
                Write-Host "Downloaded: $fn"
            } catch {
                Write-Host "Could not download $fn"
            }
        }
    }
}
Write-Host "Finished! Downloaded $downloaded images."
