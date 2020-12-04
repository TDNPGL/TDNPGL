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
# MIIFZQYJKoZIhvcNAQcCoIIFVjCCBVICAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB
# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR
# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQU++Q6MqZAhRUQ66Ac0qE0VQYp
# ZxKgggMJMIIDBTCCAe2gAwIBAgIQEmfMOfIM97xB8VaXoHA3GDANBgkqhkiG9w0B
# AQsFADARMQ8wDQYDVQQDDAZ6YXRyaXQwHhcNMjAxMjAzMDc1MDMzWhcNMjExMjAz
# MDgxMDMzWjARMQ8wDQYDVQQDDAZ6YXRyaXQwggEiMA0GCSqGSIb3DQEBAQUAA4IB
# DwAwggEKAoIBAQCttMWrYJkfS3qaRpKfTi78wGpcb1gVVnT6UNvu5FzniFlfDwk8
# Kz3ZpoTFRbeNfHEdicHE0XR3zRA70QsoGhqlO6QcX/CT7DJawTrkIuFgdrv2S/KZ
# +V4B7UhP/ZlVeg0PC6A6iJOh1V3RYG9JY95nBTePbHw18cVvW8YDkjlugUqH9pe3
# FHkttmVpSJXpzphoaHk1Iyzxymq4gzEu0+exU5rdWcDCX24w+L55FWaedgRtTd3i
# 0ONfgeid/BAtdavIpmxQyF/mE3aM5q46BY+Vbsp7qy0bBj8qWIim6PVJU1zRqMs4
# UqeG/BjXHs5jYqQDfeQzFrhO6W6Mnvs2w1eBAgMBAAGjWTBXMA4GA1UdDwEB/wQE
# AwIHgDATBgNVHSUEDDAKBggrBgEFBQcDAzARBgNVHREECjAIggZ6YXRyaXQwHQYD
# VR0OBBYEFE2H4jfGqIJ/5TxI0xvnHvXiA8YCMA0GCSqGSIb3DQEBCwUAA4IBAQAd
# KQ4tKqLcdUkGe1xE9U3dQ1tXUQ4MymGPlEmB9aOAOr9Y6Oj9H/wDJh1NxI7OYAbS
# tNE+crHlgQv2rHCDlcxJPD9ASfI1TGyMK7+46iXuPYZM4OFvtu2c/cju7510ybKh
# JmjYOlr+ImIG3kzm1QuZPBFBb1qJ850BrsP6JAGlNP79GXMFMQPDBQEZGZ/dSi06
# QT7KithCUSAcEwSmm8RtLY5rNHeOpgMaJfvCpRr6k/TrsaXzD39AfVbhaj+Om6Ct
# CMLMCQOKg7WVwSPtEunAUft4OwrQuoaHoqLtzFiD34YhMEqOGZoIV2d6WDmNeCKV
# B/UqnzHSCnukllmUr3dKMYIBxjCCAcICAQEwJTARMQ8wDQYDVQQDDAZ6YXRyaXQC
# EBJnzDnyDPe8QfFWl6BwNxgwCQYFKw4DAhoFAKB4MBgGCisGAQQBgjcCAQwxCjAI
# oAKAAKECgAAwGQYJKoZIhvcNAQkDMQwGCisGAQQBgjcCAQQwHAYKKwYBBAGCNwIB
# CzEOMAwGCisGAQQBgjcCARUwIwYJKoZIhvcNAQkEMRYEFNhYlQa0gkgPGIPqOsQF
# 0cJMypHsMA0GCSqGSIb3DQEBAQUABIIBADgUi3A6es7BihLX449XGC+4hDeJP6rE
# A3853jXgQtF6N6mOtbpq1pyCzQwK4nDink9EE4cbqm1Iqg+sE09WDFEjPkJ9SUlO
# 4KqioShk4z/QZ2Iz+FWmGzE7PG59uHRqjyq8t1fLTyK4Mpge0g+vWVuDPO/aX2Bc
# BgFR1BH0VzwrqWgYygrv76/RDB4uYJaI9wd8+eHMKkVgvnuOl2RYve0zggm0AsqY
# vww/3fQ2h/BXVfgWUlsp79Ezrh7rRCmsAzxDjz+ixRvu+4XIOEUYDeqg5rpvvEir
# JQKrJ02q+T8W0LVMdatgxyb/KHvE0cPkZ8ZBL6QJ+Lg3J2vCzcAmr94=
# SIG # End signature block
