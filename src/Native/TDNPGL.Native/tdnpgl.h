#ifndef TDNPGL_H
#define TDNPGL_H
#include <string>

int AABB_IsPointOver(float x, float y, float minx, float miny, float maxx, float maxy);
int NewGameWindow(std::string const& title);
#endif