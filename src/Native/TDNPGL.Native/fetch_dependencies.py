import os
import platform
import argparse

os_pl=platform.system()
is_win=os_pl=="Windows"

def build():
	if is_win:
		os.system("bootstrap-vcpkg.bat")
	else:
		os.system("./bootstrap-vcpkg.sh")

parser = argparse.ArgumentParser(description='TDNPGL.Native fetch dependencies script')
parser.add_argument('--rebuild',dest='rebuild', action='store_const',const=True,help='Rebuild VCPKG')

args = parser.parse_args()

os.chdir("vcpkg")
os.system("git pull")

if is_win:
	if not os.path.exists("vcpkg.exe") or args.rebuild:
		build()
	os.system("vcpkg install glfw3")
else:
	os.system("./vcpkg install glfw3")
packages = [f for f in os.listdir("packages") if os.path.isdir(os.path.join("packages", f))]