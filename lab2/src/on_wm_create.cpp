#include "lab1.hpp"
#include <iostream>

void        OnWmCreate(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
    std::cout << "On Create" << std::endl;
    g_mp3_player = new Mp3Player(hwnd);
    g_scroll_ctrl = new ScrollCtrl();
}