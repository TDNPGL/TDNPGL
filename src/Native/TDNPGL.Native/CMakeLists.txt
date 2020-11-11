cmake_minimum_required(VERSION 3.10)

# set the project name
project(TDNPGL.Native.Linux)

include(GenerateExportHeader)

set(CMAKE_SYSTEM_NAME Linux)
set(CMAKE_SYSTEM_PROCESSOR arm)

option(PROC_ARCH "Target processor architecture" "amd64")

# add the executable
set( SOURCE_LIB tdnpgl.cpp)
message("-- Building TDNPGL")
add_library(tdnpgl SHARED ${SOURCE_LIB})
include_directories(tdnpgl_native ../TDNPGL.Native)