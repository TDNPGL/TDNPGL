write-host "Packing all projects"  -foreground 'green'
dotnet pack > ".\packed\packingOutput.txt"
If (-Not (Test-Path -Path packed)){
	mkdir packed
}
$arr = Get-ChildItem . -recurse *.nupkg |
       Foreach-Object {$_.FullName}
ForEach ($file in $arr)
	{
		if( -Not($file -clike "*\packed\*")){
			$fileName=[System.IO.Path]::GetFileName($file)
			$packPath=".\packed\$fileName" 
			write-host "Moving $fileName"  -foreground 'yellow'
			Move-Item -Path $file -Destination $packPath -Force
		}
	}

# SIG # Begin signature block
# MIIFQAYJKoZIhvcNAQcCoIIFMTCCBS0CAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUryxYTbBvmmLA0YlsUVzXWcha
# 7DugggLkMIIC4DCCAcigAwIBAgIQFNgQ8TQuZYpPx82ZwitY4TANBgkqhkiG9w0B
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
# BgorBgEEAYI3AgELMQ4wDAYKKwYBBAGCNwIBFTAjBgkqhkiG9w0BCQQxFgQUqNWG
# LVam00DFzggsdeGf1EE9vaYwDQYJKoZIhvcNAQEBBQAEggEAiQxI8AVbaxPvJYcc
# sJ8X7FOfF0Yca/trZyFWX7/fA5zwbyF0Wnle3kEsxk3oLEuc/FT3SMaZdeTAx1fQ
# kePJALkIy3Ph0/DrIHsCI8+yZpLe/fgHVoMIoInQ50063DJ8ntEvKJqnIu/fS9+b
# zJTJop+4zfrvvX5TWHh8+ADrNhmISepS+C/004B5z6dNYvDIOqrvu+dPWb8P3eBm
# VBTi1Dx+t2pdLH/uiVwNNOLFXPlsbH+YRLTFQ/himqPrtp2NmwqwZv0Q0V59Hraf
# PihZh5I0QePW6Gu7bjVoc4CyCPvbYVl0M2SfkWmP1YXhkb2mhpj7jX3EkjbkkLsY
# Cn0JDQ==
# SIG # End signature block
