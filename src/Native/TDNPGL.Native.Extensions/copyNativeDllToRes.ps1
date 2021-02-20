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
