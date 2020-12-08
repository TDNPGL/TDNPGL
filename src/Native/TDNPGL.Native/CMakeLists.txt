cmake_minimum_required(VERSION 3.10)

# set the project name
project(tdnpgl)

include(GenerateExportHeader)

set(CMAKE_SYSTEM_NAME Linux)
set(CMAKE_SYSTEM_PROCESSOR arm)

# add the executable
set(SOURCE_LIB tdnpgl.cpp native_window.cpp)
message("-- Building TDNPGL")
add_library(tdnpgl SHARED ${SOURCE_LIB})

#Unix
if(UNIX)
	#Current architecture
	set(CMAKE_CXX_COMPILER x86_64-linux-gnux32-g++)
	set(CMAKE_C_COMPILER x86_64-linux-gnux32-gcc)
	add_library(tdnpgl_x86 SHARED ${SOURCE_LIB})
	#ARM64
	set(CMAKE_CXX_COMPILER aarch64-linux-gnu-g++)
	set(CMAKE_C_COMPILER aarch64-linux-gnu-gcc)
	add_library(tdnpgl_ARM64 SHARED ${SOURCE_LIB})
	#I686
	set(CMAKE_CXX_COMPILER i686-linux-gnu-g++)
	set(CMAKE_C_COMPILER i686-linux-gnu-gcc)
	add_library(tdnpgl_i686 SHARED ${SOURCE_LIB})
	#ARM
	set(CMAKE_CXX_COMPILER arm-none-eabi-g++)
	set(CMAKE_C_COMPILER arm-none-eabi-gcc)
	add_library(tdnpgl_ARM SHARED ${SOURCE_LIB})
endif(UNIX)
include_directories(tdnpgl_native ../TDNPGL.Native)