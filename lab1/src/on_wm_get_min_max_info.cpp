#include "lab1.hpp"

void        OnWmGetMinMaxInfo(LPARAM lParam)
{
    LPMINMAXINFO winSize = (LPMINMAXINFO)lParam;
    winSize->ptMinTrackSize.x = MIN_WIN_WIDTH;
    winSize->ptMinTrackSize.y = MIN_WIN_HEIGHT;
}