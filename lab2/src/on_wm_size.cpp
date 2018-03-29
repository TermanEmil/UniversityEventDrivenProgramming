#include "lab1.hpp"
#include <iostream>

void        OnWmSize(HWND hwnd, int cx, int cy)
{
    g_scroll_ctrl->OnWmSizeInternal(cx, cy);
    g_mp3_player->OnWmSize(cx, cy);

    RedrawWindow(hwnd, NULL, NULL, RDW_INVALIDATE | RDW_ERASE);
}