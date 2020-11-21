if [ -z "$1" -a "$1" != " " ]; then
     echo "First argument should not be empty! It's should contains GCC compiler name"
	 exit 1
fi
if [ -z "$2" -a "$2" != " " ]; then
     echo "Second argument should not be empty! It's should contains G++ compiler name"
	 exit 1
fi
./build.sh -D CMAKE_C_COMPILER=$1 -D CMAKE_CXX_COMPILER=$2