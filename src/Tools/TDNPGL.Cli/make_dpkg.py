#!/usr/bin/python
import os
import sys
import re

def mkdir(dir):
    if not os.path.isdir(dir):
        os.mkdir(dir)

os.system("dotnet msbuild /property:Configuration=Release")

s=open("../../Directory.Build.props",'r').read()

regex = '<Version>([^"]*)</Version>'

ver = re.findall(regex,s)

mkdir("./bin")
mkdir("./bin/tdnpgl-deb")
mkdir("./bin/tdnpgl-deb/usr")
mkdir("./bin/tdnpgl-deb/usr/bin")
mkdir("./bin/tdnpgl-deb/usr/share")
mkdir("./bin/tdnpgl-deb/usr/share/tdnpgl")
mkdir("./bin/tdnpgl-deb/DEBIAN")

is_win=sys.platform=="win32"
if(is_win):
    cpcmd="copy"
    release_path=".\\bin\\Release\\netcoreapp3.1\\*"
    out_path="bin\\tdnpgl-deb\\usr\\share\\tdnpgl\\"
else:
    cpcmd="cp"
    release_path="./bin/Release/netcoreapp3.1/*"
    out_path="./bin/tdnpgl-deb/usr/share/tdnpgl/"

os.system(cpcmd+" "+release_path+" "+out_path+" -r -f")

f = open("./bin/tdnpgl-deb/DEBIAN/control","w")
f.write("Package: tdnpgl\nVersion: "+ver[0]+"\nArchitecture: all\nEssential: no\nSection: web\nPriority: optional\nDepends: dotnet-sdk-3.1\nMaintainer: zatrit\nInstalled-Size: 10240\nDescription: SDK for TDNPGL\n")
f.close()

f2 = open("./bin/tdnpgl-deb/usr/bin/tdnpgl","w")
f2.write("#!/bin/bash\ndotnet /usr/share/tdnpgl/tdnpgl.dll $*")
f2.close()
if not is_win:
    os.system("chmod 755 ./bin/tdnpgl-deb/")
    os.system("dpkg -b ./bin/tdnpgl-deb/ ./bin/tdnpgl-any-cpu-1.5.4.4.deb")
