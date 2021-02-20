Set-AuthenticodeSignature $Args[0] @(Get-ChildItem cert:\CurrentUser\My -codesign)[1]
