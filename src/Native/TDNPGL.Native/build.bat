@echo off

vswhere -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe > msbpath
set /p Build=<msbpath
del msbpath

mkdir build
mkdir build\Win
cd build\Win

cmake build -S "..\.."

echo MSBuild locaion: %Build%
"%Build%" tdnpgl.vcxproj /t:Rebuild /p:Configuration=Release

cd ..\..

pause