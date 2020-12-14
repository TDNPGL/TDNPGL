#!/usr/bin/python
import os
import re
from colorama import init
init()
from colorama import Fore
regex = 'PackageReference Include="([^"]*)"'
for root, dirs, files in os.walk("."):
    for file in files:
        if file.endswith(".csproj"):
            fileName=os.path.join(root, file)
            print(Fore.GREEN+"Updating "+fileName+Fore.RESET)
            with open(fileName) as f: 
                s = f.read()
            packages=re.findall(regex,s)
            for package in packages:
				print(Fore.YELLOW+"Updating "+package+" in "+fileName+Fore.RESET)
                os.system("dotnet add "+fileName+" package "+package)
