#!/usr/bin/python
from shutil import copyfile
copyfile("../TDNPGL.Native/build/Win/Release/tdnpgl.dll","x64/tdnpgl.dll")
copyfile("../TDNPGL.Native/build/Win/Release/tdnpgl.dll","x86/tdnpgl.dll")
copyfile("../TDNPGL.Native/build/Win/Release/tdnpgl.dll","x64/tdnpgl.dll")
copyfile("../TDNPGL.Native/build/Unix/libtdnpgl_ARM64.so","arm64/libtdnpgl.so")
copyfile("../TDNPGL.Native/build/Unix/libtdnpgl_x86.so","x86/libtdnpgl.so")
