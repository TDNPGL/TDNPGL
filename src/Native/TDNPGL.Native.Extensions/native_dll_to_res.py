#!/usr/bin/python3
from shutil import copyfile
import os

def cp(frm,to):
	try:
		copyfile(frm,to)
	except:
		print(frm+" not exists!")
def mkdir(direc):
  if not os.path.isdir(direc):
    os.mkdir(direc)

os.system("python3 ../TDNPGL.Native/build.py")

mkdir("x64")
mkdir("x86")
mkdir("arm")
mkdir("arm64")

cp("../TDNPGL.Native/build/Win/Release/tdnpgl.dll","x64/tdnpgl.dll")
cp("../TDNPGL.Native/build/Win/Release/tdnpgl.dll","x86/tdnpgl.dll")
cp("../TDNPGL.Native/build/Unix/libtdnpgl_x64.so","x64/libtdnpgl.so")
cp("../TDNPGL.Native/build/Unix/libtdnpgl_ARM64.so","arm64/libtdnpgl.so")
cp("../TDNPGL.Native/build/Unix/libtdnpgl_ARM.so","arm/libtdnpgl.so")
cp("../TDNPGL.Native/build/Unix/libtdnpgl_x86.so","x86/libtdnpgl.so")
