#include "lab1.hpp"
#include <iostream>

void        OnWmSize(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
    int cxCoord, cyCoord;

    cxCoord = LOWORD(lParam);
    cyCoord = HIWORD(lParam);

    g_mp3_player->OnWmSize(cxCoord, cyCoord);

    RedrawWindow(hwnd, NULL, NULL, RDW_INVALIDATE | RDW_ERASE);
}