#!/usr/bin/python
import os
import sys

os.system("dotnet msbuild /property:Configuration=Release")

if not os.path.exists("bin/tdnpgl-deb/usr/lib/tdnpgl"):
	os.mkdir("./bin")
    os.mkdir("./bin/tdnpgl-deb")
    os.mkdir("./bin/tdnpgl-deb/usr")
    os.mkdir("./bin/tdnpgl-deb/usr/bin")
    os.mkdir("./bin/tdnpgl-deb/usr/lib")
    os.mkdir("./bin/tdnpgl-deb/usr/lib/tdnpgl")
    os.mkdir("./bin/tdnpgl-deb/DEBIAN")

is_win=sys.platform=="win32"
if(is_win):
    cpcmd="copy"
    release_path=".\\bin\\Release\\netcoreapp3.1\\*"
    out_path="bin\\tdnpgl-deb\\usr\\lib\\tdnpgl\\"
else:
    cpcmd="cp"
    release_path="./bin/Release/netcoreapp3.1/*"
    out_path="./bin/tdnpgl-deb/usr/lib/tdnpgl/"

os.system(cpcmd+" "+release_path+" "+out_path+" -r -f")

f = open("./bin/tdnpgl-deb/DEBIAN/control","w")
f.write("Package: tdnpgl\nVersion: 1.5.4.4\nArchitecture: all\nEssential: no\nSection: web\nPriority: optional\nDepends: dotnet-sdk-3.1\nMaintainer: zatrit\nInstalled-Size: 10240\nDescription: SDK for TDNPGL\n")
f.close()

f2 = open("./bin/tdnpgl-deb/usr/bin/tdnpgl","w")
f2.write("#!/bin/bash\ndotnet /usr/lib/tdnpgl/tdnpgl.dll $*")
f2.close()
if not is_win:
    os.system("chmod +x ./bin/tdnpgl-deb/usr/bin/tdnpgl")
    os.system("dpkg -b ./bin/tdnpgl-deb/ ./bin/tdnpgl-any-cpu-1.5.4.4.deb")
