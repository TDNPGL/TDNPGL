#!/usr/bin/python
import os
import re
from colorama import init
init()
from colorama import Fore,Style
regex = 'PackageReference Include="([^"]*)"'
print(Style.BRIGHT)
for root, dirs, files in os.walk("."):
	for file in files:
		if file.endswith(".csproj"):
			fileName=os.path.join(root, file)
			print(Fore.GREEN+"Updating "+fileName)
			with open(fileName) as f: 
				s = f.read()
			packages = re.findall(regex,s)
			for package in packages:
				print(Style.BRIGHT)
				print(Fore.YELLOW+"Updating "+package+" in "+file+Style.RESET_ALL)
				os.system("dotnet add "+fileName+" package "+package)
print(Style.RESET_ALL)