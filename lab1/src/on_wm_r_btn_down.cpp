#include "lab1.hpp"
#include <string>
#include <sstream>

void    OnWmRBtnDown(HWND hwnd)
{
    std::stringstream   buf;
    POINT               mouse_pos;

    GetCursorPos(&mouse_pos);
    buf << "X: ";
    buf << std::to_string((LONG)mouse_pos.x);
    buf << " Y: ";
    buf << std::to_string((LONG)mouse_pos.y);
    SetWindowText(hwnd, buf.str().c_str());
}