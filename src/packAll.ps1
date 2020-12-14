write-host "Packing all projects"  -foreground 'green'
dotnet pack > ".\packed\packingOutput.txt"
If (-Not (Test-Path -Path packed)){
	mkdir packed
}
$arr = Get-ChildItem . -recurse *.nupkg | 
       Foreach-Object {$_.FullName}
ForEach ($file in $arr)
	{
		$fileName=[System.IO.Path]::GetFileName($file)
		$packPath=".\packed\$fileName" 
		write-host "Copying $fileName"  -foreground 'yellow'
		Move-Item -Path $file -Destination $packPath -Force
	}

# SIG # Begin signature block
# MIIFQAYJKoZIhvcNAQcCoIIFMTCCBS0CAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUY+eO/yhUl2UivwolNhJse2lH
# phqgggLkMIIC4DCCAcigAwIBAgIQFNgQ8TQuZYpPx82ZwitY4TANBgkqhkiG9w0B
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
# BgorBgEEAYI3AgELMQ4wDAYKKwYBBAGCNwIBFTAjBgkqhkiG9w0BCQQxFgQUqCK0
# FFpwNcFnFFjTFQIsooFTWm8wDQYJKoZIhvcNAQEBBQAEggEAPveFIYmjOyWe3X/+
# vFH3L/QuOpipdUWzMI+qHQtf7kBOUlqL1NB/+4n3KKagXkmtxPNxgApE0FsFTw+3
# YHJhgZb7LzSB+tq1xPNM+NaoWkwHvHXl2Ickpis/vVJYHfJzlDMDYMsgp5rx3i53
# PsKM2juLomHK95+kRzOHEg8QhOkgMCPtzU7hKT6WREr7gvwakfC+aec23rTZlrfQ
# kFlk1NlpBhra5QUNSuUOss/dJK6pUtTKElaKbzHEi+ZR88SsMmBz9egnWcGdvMrN
# c8Gj16LSNkrNJMjX462HLPM1JYSHS5xg1laIpmZzeVL0dmAQ8FmnTiLDShB9hRD5
# SmK1bw==
# SIG # End signature block
