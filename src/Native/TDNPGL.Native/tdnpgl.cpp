#if defined(_MSC_VER) || defined(_WIN32) || defined(WIN32)
//  Microsoft 
#define EXPORT extern "C" __declspec(dllexport)
#define PLATFORM "Windows"
#elif defined(__GNUC__) || defined(__unix__ )
//  GCC
#include <game_window.h>
#define EXPORT extern "C"
#define PLATFORM "Unix"
EXPORT int NewNativeWindow() {
	KeyAction action = KeyAction::PRESS;
	return (int)action;
}
#else
//  do nothing and hope for the best?
#define EXPORT
#define PLATFORM "UNKNOWN"
#pragma warning Unknown dynamic link import/export semantics.
#endif

EXPORT int AABB_IsPointOver(float x, float y, float minx, float miny, float maxx, float maxy) {
	int result = (x > minx) 
		&& (x < maxx) 
		&& (y > miny) 
		&& (y < maxy);
	return result;
}

