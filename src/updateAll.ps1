$regex = 'PackageReference Include="([^"]*)" Version="([^"]*)"'

ForEach ($file in get-childitem . -recurse | where {$_.extension -like "*proj"})
{
    $packages = Get-Content $file.FullName |
        select-string -pattern $regex -AllMatches | 
        ForEach-Object {$_.Matches} | 
        ForEach-Object {$_.Groups[1].Value.ToString()}| 
        sort -Unique

    ForEach ($package in $packages)
    {
        write-host "Update $file package :$package"  -foreground 'magenta'
        $fullName = $file.FullName
        iex "dotnet add $fullName package $package"
    }
}
# SIG # Begin signature block
# MIIFQAYJKoZIhvcNAQcCoIIFMTCCBS0CAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQU++Q6MqZAhRUQ66Ac0qE0VQYp
# ZxKgggLkMIIC4DCCAcigAwIBAgIQFNgQ8TQuZYpPx82ZwitY4TANBgkqhkiG9w0B
# AQsFADARMQ8wDQYDVQQDEwZaYXRyaXQwHhcNMjAwNjE5MjA0NjUxWhcNMjEwNjIw
# MDI0NjUxWjARMQ8wDQYDVQQDEwZaYXRyaXQwggEiMA0GCSqGSIb3DQEBAQUAA4IB
# DwAwggEKAoIBAQDnn3YEAR72zq6hCp7NEVWd7RK2cPMbzQic7AIpl9Rmw75wGuKZ
# OgsLpNtB6zpZ0aE6hEFtjXQBx4NkcoxYbvar1UTXlHzY2zAbgaDq3gzhvxTs1ypT
# 7bWNZhDccATNmgc7Y1z/056Iork1ERQG3m96NjBIghaZpIJ6/A+fAZqysCnrOH5j
# 0sAA1Or+Y9We0WMbbd9feM13ljFfPBhllHxjcAdvzDGovn6UaXQLOGOkndJp/+4N
# 2fWvY+hY8as8MErw819W0+31g+NRzWjse7femu3q+XmAWHfzjtPED2h9nNdxD3Vb
# hdyF1odA/PL4XYdx5TkqiD2WMWSQia3OhcJJAgMBAAGjNDAyMAwGA1UdEwEB/wQC
# MAAwIgYDVR0lAQH/BBgwFgYIKwYBBQUHAwMGCisGAQQBgjdUAwEwDQYJKoZIhvcN
# AQELBQADggEBAFofeTOSKZmgW/kClSjH2qchLgohsSnrO9/8MsWHg0DsChArKUOU
# 07ecycdeCzRdmK0Oj64jliLj92OfEwWga54N2VPctlYu51iglla4epodDe8TpR83
# sEYzXByy15DDRrGfAPkCHxvOi/n4uti8tub9A9A7zIii1vjMfi7cWyKZPdyuTPxE
# ti0n1I6ItdDrXygMWonvKOWww97wku7c1ZE0yRXbMMpKW8n/3M/MoybGLlBZB7hs
# x2zZUtdR/vH2FVQW2GzJbzeiVPfiQdIksSpihyp5MV4eIyOf+2OTZu+Y/XdPEYri
# JqEmVAzqAStR/spDTXIvzixHnjgZf53dZ0sxggHGMIIBwgIBATAlMBExDzANBgNV
# BAMTBlphdHJpdAIQFNgQ8TQuZYpPx82ZwitY4TAJBgUrDgMCGgUAoHgwGAYKKwYB
# BAGCNwIBDDEKMAigAoAAoQKAADAZBgkqhkiG9w0BCQMxDAYKKwYBBAGCNwIBBDAc
# BgorBgEEAYI3AgELMQ4wDAYKKwYBBAGCNwIBFTAjBgkqhkiG9w0BCQQxFgQU2FiV
# BrSCSA8Yg+o6xAXRwkzKkewwDQYJKoZIhvcNAQEBBQAEggEABHaW1tNYSg2iRd2R
# bCxYnIo2G8S3z/IH1Xt0Z1B18OoiCz5rcELRhGQFivVQBQg7m+NRdD5+gpbKxvIH
# zKX1GBk4lX0Vn3OjoH+nCx10c81J0vXf7yr088jxTIXpssguvPfKXWEJOAHEkfJq
# XxJaiuWI1/uxWY/5C9ineBznXHwVhcoJqn4+ZBAAvTL2UO5SikaHTIc+1LIktQQI
# hGqFCqziiyKkUMMJiHLYiMaRcMOb8mnpuWTf7UFeKjw6xGhkcXGdeB85GCkiyv/F
# uS+a7MQ3OKy6Q2tR1rsDl2XhZax7MOVcCn025bX3om0XV0WOiAEYGz5RYRro96In
# 6Kjdww==
# SIG # End signature block
