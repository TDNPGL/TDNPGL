#!/bin/bash

mkdir "build" 2>/dev/null
mkdir "build/Unix" 2>/dev/null
cd "build/Unix"
cmake $1 $2 $3 $4 $5 $6 $7 $8 $9 -S "../.."
make
exit 0;