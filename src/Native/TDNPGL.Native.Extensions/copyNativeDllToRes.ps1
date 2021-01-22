Function mkdirA  {
	Param (
	[string]$Name
	)
	if (-Not ( Test-Path -Path $Name ) ) {
		mkdir $Name
	}
}

mkdirA x64
mkdirA x86
mkdirA arm
mkdirA arm64

copy ..\TDNPGL.Native\build\Win\Release\tdnpgl.dll x64\tdnpgl.dll
copy ..\TDNPGL.Native\build\Win\Release\tdnpgl.dll x86\tdnpgl.dll
copy ..\TDNPGL.Native\build\Unix\libtdnpgl_x86.so x86\libtdnpgl.so
copy ..\TDNPGL.Native\build\Unix\libtdnpgl_ARM64.so arm64\libtdnpgl.so
copy ..\TDNPGL.Native\build\Unix\libtdnpgl_x64.so x64\libtdnpgl.so
copy ..\TDNPGL.Native\build\Unix\libtdnpgl_ARM.so arm\libtdnpgl.so
# SIG # Begin signature block
# MIIFQAYJKoZIhvcNAQcCoIIFMTCCBS0CAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUas6kUdibc3A/X0iet37Npo4A
# tnmgggLkMIIC4DCCAcigAwIBAgIQFNgQ8TQuZYpPx82ZwitY4TANBgkqhkiG9w0B
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
# BgorBgEEAYI3AgELMQ4wDAYKKwYBBAGCNwIBFTAjBgkqhkiG9w0BCQQxFgQUmlxu
# lQ9YK2Ud8UIRwh5OZd4LiLEwDQYJKoZIhvcNAQEBBQAEggEAj0hlFcQt8hfk+ETY
# Ly5fxOXhQRIb8u98jqN6VuRTDw7pkHsXi/H3bdxk8UZfIJWkf0oJ6ix6AkcUpvO8
# MkIFJLsH/gNg1/5rKOUlr+0VyHk0EiDJMuWpyC57x+F0M8ouWhAGJZpNE28gPDsJ
# xczQWohuXNBI4X/2J2eblNmO4fS10cvu6qcNJX6/jw91oQpL8/2Q3BZaGXZBpwST
# 6aQgydYdCzs8qefYRSc9Ch+1RpKVJ8Q7zesFgNW+ubV95HSNm5Uful3NUnQoK3em
# Voh9gUXzWbEvO2mzei9wOYS3HzMBkNc7+YRAbsQKnt/5UsxAR3LChMNO8cMU1IAK
# 0gxEfA==
# SIG # End signature block
