$files = @("homepage.json", "about-us.json", "adventures.json", "barrameru-v1-shop.json", "barrameru-v1-single-product.json", "barrameru-v1-cart-page.json", "barrameru-v1-checkout-page.json", "contact-us.json", "faqs.json", "team.json")

function Get-Headings($o, $list) {
    if ($null -eq $o) { return }
    if ($o -is [System.Management.Automation.PSCustomObject]) {
        foreach ($prop in $o.PSObject.Properties) {
            if ($prop.Name -match "title|heading_title|sub_heading|testimonial_content|testimonial_name" -and $prop.Value -is [string] -and $prop.Value.Length -gt 2) {
                $clean = $prop.Value -replace '<[^>]+>', ''
                $list.Add($clean) | Out-Null
            }
            Get-Headings $prop.Value $list
        }
    } elseif ($o -is [System.Collections.IEnumerable] -and $o -isnot [string]) {
        foreach ($item in $o) { Get-Headings $item $list }
    }
}

foreach ($f in $files) {
    $path = Join-Path "drive-download\templates" $f
    if (Test-Path $path) {
        $json = Get-Content -Raw $path | ConvertFrom-Json
        $list = [System.Collections.Generic.List[string]]::new()
        Get-Headings $json.content $list
        Write-Host "=== $f ($($list.Count) items) ==="
        $list | Select-Object -Unique | Select-Object -First 8 | ForEach-Object { Write-Host " - $_" }
    }
}
