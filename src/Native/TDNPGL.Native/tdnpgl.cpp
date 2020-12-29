#if defined(_MSC_VER) || defined(_WIN32) || defined(WIN32)
//  Microsoft 
#define EXPORT extern "C" __declspec(dllexport)
#define PLATFORM "Windows"
#include <string>
EXPORT int NewGameWindow(std::string title){
	return 0;
}
#elif defined(__GNUC__) || defined(__unix__ )
//  GCC
#include "linux-include.h"
#define EXPORT extern "C"
#define PLATFORM "Unix"
EXPORT int NewGameWindow(std::string title) {
	GLFWGameWindow* wnd=new GLFWGameWindow(title,800,600,GraphicsApi::OPENGL);
	wnd->show();
	GLFWGameWindow::DrawCallback draw([]{ std::cout << "Draw callback"; });
	wnd->setDrawCallback(draw);
	return 0;
}
#else
//  do nothing and hope for the best?
#define EXPORT
#define PLATFORM "UNKNOWN"
#pragma warning Unknown dynamic link export semantics.
#endif

EXPORT int AABB_IsPointOver(float x, float y, float minx, float miny, float maxx, float maxy) {
	int result = (x > minx) 
		&& (x < maxx) 
		&& (y > miny)
		&& (y < maxy);
	return result;
}

