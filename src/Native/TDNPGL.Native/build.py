#!/usr/bin/python
#Imports
import wget
from pathlib import Path
import platform
import sys
import os
from colorama import init
init()
from colorama import Fore
import argparse

#Methods
def gotoUnix():
	if not Path("build/Unix").is_dir():
		os.mkdir("build/Unix")
	os.chdir("build/Unix")
def gotoWin():
	if not Path("build/Win").is_dir():
		os.mkdir("build/Win")
	os.chdir("build/Win")
#Defines
parser = argparse.ArgumentParser(description='TDNPGL.Native build script')
parser.add_argument("-wsl", help="Allow compile using WSL(boolean)",default="false", type=str)
parser.add_argument("-cpu", help="Sets target CPU",default="tdnpgl", type=str)
args = parser.parse_args()
os_pl=platform.system()
is_linux=os_pl=="Linux"

wsl_mode=args.wsl=="true"
target_cpu=args.cpu
#Code
if not Path("build").is_dir():
	os.mkdir("build")
print("Current OS: " + os_pl)
if os_pl=="Windows" and not wsl_mode:
	#Update VSWhere
	vswhere = "vswhere.exe"
	vswhere_link="https://github.com/microsoft/vswhere/releases/latest/download/vswhere.exe"
	if not Path(vswhere).is_file():
		print(Fore.GREEN+vswhere + " not found! Downloading..."+Fore.RESET)
		wget.download(vswhere_link,vswhere)
	#Find MSBuild
	vspath=os.popen("vswhere -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe").read()
	Index=len(vspath)-1
	vspath=vspath[:Index] + vspath[Index + 1:]
	print("MSBuild found at \""+vspath+"\"")
	#Build
	gotoWin()
	os.system("cmake ../..")
	os.system("\""+vspath + "\"" + " tdnpgl.vcxproj /t:Rebuild /p:Configuration=Release")
#WSL
elif wsl_mode and not is_linux:
	print(Fore.YELLOW+"Enabled WSL build mode"+Fore.RESET)
	gotoUnix()
	os.system("wsl cmake ../..")
	print("Started Makefile build")
	os.system("wsl make "+target_cpu)
#Error
elif wsl_mode and is_linux:
	print(Fore.RED+"Could not enable WSL build mode in Linux"+Fore.RESET)
#Just linux
elif not wsl_mode and is_linux:
	gotoUnix()
	os.system("cmake ../..")
	print("Started Makefile build")
	os.system("make "+target_cpu)