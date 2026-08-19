$h = Get-Content -Raw "drive-download\templates\barrameru-v1-header.json" | ConvertFrom-Json
$f = Get-Content -Raw "drive-download\templates\barrameru-v1-footer.json" | ConvertFrom-Json

function Get-Strings($o, $list) {
    if ($null -eq $o) { return }
    if ($o -is [System.Management.Automation.PSCustomObject]) {
        foreach ($prop in $o.PSObject.Properties) {
            if ($prop.Value -is [string] -and $prop.Value.Length -gt 2 -and $prop.Value.Length -lt 250 -and $prop.Name -notmatch "id|widgetType|elType|font|unit|color") {
                $list.Add("$($prop.Name): $($prop.Value)") | Out-Null
            }
            Get-Strings $prop.Value $list
        }
    } elseif ($o -is [System.Collections.IEnumerable] -and $o -isnot [string]) {
        foreach ($item in $o) { Get-Strings $item $list }
    }
}

$hList = [System.Collections.Generic.List[string]]::new()
Get-Strings $h.content $hList
Write-Host "=== HEADER STRINGS ==="
$hList | Select-Object -Unique | Select-Object -First 30

$fList = [System.Collections.Generic.List[string]]::new()
Get-Strings $f.content $fList
Write-Host "=== FOOTER STRINGS ==="
$fList | Select-Object -Unique | Select-Object -First 30
