cmake_minimum_required(VERSION 3.10)
# set the project name
project(tdnpgl)

include(GenerateExportHeader)

if(DEBUG)
	set(DEBUG_ARG "-ggdb")
endif(DEBUG)

set(GCC "gcc" ${DEBUG_ARG})
set(GPP "g++" ${DEBUG_ARG})

# add the executable
set(SOURCE_LIB tdnpgl.cpp tdnpgl.h)
message("-- Building TDNPGL")

add_library(tdnpgl SHARED ${SOURCE_LIB})
include_directories(tdnpgl "game-window/include" "game-window/src")

#Unix
if(UNIX)
	message("-- Adding UNIX-supporting builds")
	#x64
	set(CMAKE_CXX_COMPILER x86_64-linux-gnux32-${GPP})
	set(CMAKE_C_COMPILER x86_64-linux-gnux32-${GCC})
	add_library(tdnpgl_x86 SHARED ${SOURCE_LIB})
	#ARM64
	set(CMAKE_CXX_COMPILER aarch64-linux-gnu-${GPP})
	set(CMAKE_C_COMPILER aarch64-linux-gnu-${GCC})
	add_library(tdnpgl_ARM64 SHARED ${SOURCE_LIB})
	#x86
	set(CMAKE_CXX_COMPILER i686-linux-gnu-${GPP})
	set(CMAKE_C_COMPILER i686-linux-gnu-${${GCC}})
	add_library(tdnpgl_x86 SHARED ${SOURCE_LIB})
	#ARM
	set(CMAKE_CXX_COMPILER arm-none-eabi-${GPP})
	set(CMAKE_C_COMPILER arm-none-eabi-${GCC})
	add_library(tdnpgl_ARM SHARED ${SOURCE_LIB})
endif(UNIX)

set(CMAKE_CXX_COMPILER clang++)
set(CMAKE_C_COMPILER clang)
add_library(tdnpgl_clang SHARED ${SOURCE_LIB})