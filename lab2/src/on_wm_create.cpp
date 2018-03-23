#include "lab1.hpp"
#include <iostream>

void        OnWmCreate(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
    std::cout << "On Create" << std::endl;
    g_mp3_player = new Mp3Player(hwnd);
    // DialogBox(wincl.hInstance, MAKEINTRESOURCE(IDD_DLGFIRST),
	//           hwnd, reinterpret_cast<DLGPROC>(DlgProc));
}