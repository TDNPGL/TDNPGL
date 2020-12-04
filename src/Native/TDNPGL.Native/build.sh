#!/bin/bash

mkdir "build" 2>/dev/null
mkdir "build/Unix" 2>/dev/null
cd "build/Unix"
cmake $2 $3 $4 $5 $6 $7 $8 $9 -S "../.."
if [ [$1] == [""] ]
then
architec="tdnpgl"
else
architec=$1
fi
make $architec
exit 0;