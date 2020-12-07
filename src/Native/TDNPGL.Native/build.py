#!/usr/bin/python
import wget
from pathlib import Path
import platform
import sys
import os
from colorama import init
init()
from colorama import Fore
import argparse

parser = argparse.ArgumentParser(description='TDNPGL.Native build script')
parser.add_argument("-wsl", help="Allow compile using WSL(boolean)", type=bool)
args = parser.parse_args()
print(args)

argv=sys.argv
os_pl=platform.system()
wsl_mode=len(argv)>1 and argv[1]=="wsl"
print("Current OS: " + os_pl)
if os_pl=="Windows" and not wsl_mode:
	vswhere = "vswhere.exe"
	vswhere_link="https://github.com/microsoft/vswhere/releases/latest/download/vswhere.exe"
	if not Path(vswhere).is_file():
		print(Fore.GREEN+vswhere + " not found! Downloading..."+Fore.RESET)
		wget.download(vswhere_link,vswhere)
elif wsl_mode and not os_pl=="Linux":
	print(Fore.YELLOW+"Enabled WSL build mode"+Fore.RESET)
elif wsl_mode and os_pl=="Linux":
	print(Fore.RED+"Could not enable WSL build mode in Linux"+Fore.RESET)