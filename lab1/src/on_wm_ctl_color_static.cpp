#include "lab1.hpp"
#include <iostream>

LRESULT CALLBACK       OnWmCTLColorStatic(DWORD ctrlID, WPARAM wParam)
{
    HDC hdcStatic;

    hdcStatic = (HDC)wParam;
    if (ctrlID == IDC_LABEL1)
    {
        SetTextColor(hdcStatic, LABEL1_COLOR);
        SetBkColor(hdcStatic, LABEL1_BGCOLOR);
        SelectObject(hdcStatic, hfont);
        return (LRESULT)GetStockObject(HOLLOW_BRUSH);
    }
    return 0;
}