#!/usr/bin/python
from shutil import copyfile
def cp(frm,to):
	try:
		copyfile(frm,to)
	except:
		print(frm+" not exists!")

cp("../TDNPGL.Native/build/Win/Release/tdnpgl.dll","x64/tdnpgl.dll")
cp("../TDNPGL.Native/build/Win/Release/tdnpgl.dll","x86/tdnpgl.dll")
cp("../TDNPGL.Native/build/Unix/libtdnpgl_x64.so","x64/tdnpgl.so")
cp("../TDNPGL.Native/build/Unix/libtdnpgl_ARM64.so","arm64/libtdnpgl.so")
cp("../TDNPGL.Native/build/Unix/libtdnpgl_ARM.so","arm/libtdnpgl.so")
cp("../TDNPGL.Native/build/Unix/libtdnpgl_x86.so","x86/libtdnpgl.so")
