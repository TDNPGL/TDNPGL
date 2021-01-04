import os
import platform
import argparse
import shutil

os_pl=platform.system()
is_win=os_pl=="Windows"

def build():
	os.system("git pull")
	if is_win:
		os.system("bootstrap-vcpkg.bat")
	else:
		os.system("./bootstrap-vcpkg.sh")

def createIfNotExists(dir):
	if not os.path.exists(dir):
		os.mkdir(dir)

parser = argparse.ArgumentParser(description='TDNPGL.Native fetch dependencies script')
parser.add_argument('--rebuild',dest='rebuild', action='store_const',const=True,help='Rebuild VCPKG')

args = parser.parse_args()

os.chdir("vcpkg")

if is_win:
	cp = "copy "
	if not os.path.exists("vcpkg.exe") or args.rebuild:
		build()
	os.system("vcpkg install glfw3")
else:
	cp = "cp "
	os.system("./vcpkg install glfw3")
packages = [f for f in os.listdir("packages") if os.path.isdir(os.path.join("packages", f))]

createIfNotExists("../dependencies")
createIfNotExists("../dependencies/include")

for package in os.listdir("packages"):
	for subdir in os.listdir("packages/"+package):
		fullsubdir="packages/"+package+"/"+subdir
		if os.path.isdir(fullsubdir):
			shutil.copy2(fullsubdir," ../dependencies/")