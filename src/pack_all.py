#!/usr/bin/python
import os
import re
import shutil
from colorama import init
init()
from colorama import Fore,Style
print(Style.BRIGHT+Fore.GREEN+"Packing all projects"+Fore.RESET)
if not os.path.exists("packed"):
	os.mkdir("packed")
os.system("dotnet pack > ./packed/packingOutput.txt")
for root, dirs, files in os.walk("."):
	for file in files:
		pkg = os.path.join(root, file)
		if file.endswith(".nupkg") and not pkg.startswith(".\packed") and not pkg.startswith("./packed"):
			print(Fore.YELLOW+"Moving "+pkg)
			shutil.move(pkg, os.path.join("packed", file))
print(Style.RESET_ALL)
