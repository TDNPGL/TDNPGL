#if defined(_MSC_VER) || defined(_WIN32) || defined(WIN32)
//  Microsoft 
#include <iostream>
#define EXPORT extern "C" __declspec(dllexport)
#define PLATFORM "Windows"
#elif defined(__GNUC__) || defined(__unix__ )
//  GCC
#define EXPORT extern "C"
#define PLATFORM "Unix"
#else
//  do nothing and hope for the best?
#define EXPORT
#define PLATFORM "UNKNOWN"
#pragma warning Unknown dynamic link import/export semantics.
#endif

EXPORT int AABB_IsPointOver(float x, float y, float minx, float miny, float maxx, float maxy) {
	int result = (x > minx) && (x < maxx) && (y > miny) && (y < maxy);
	return result;
}
EXPORT int main() {
	std::cout << "TDNPGL Native Library for " << PLATFORM;
}
