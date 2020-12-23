#!/usr/bin/python
import os
import re
import shutil
from colorama import init
init()
from colorama import Fore,Style
print(Style.BRIGHT+Fore.GREEN+"Packing all projects"+Fore.RESET)
os.system("dotnet pack > ./packed/packingOutput.txt")
for root, dirs, files in os.walk("."):
	for file in files:
		if file.endswith(".nupkg"):
			pkg = os.path.join(root, file)
			print(Fore.YELLOW+"Moving "+pkg)
			shutil.move(pkg, os.path.join("packed", file))
print(Style.RESET_ALL)