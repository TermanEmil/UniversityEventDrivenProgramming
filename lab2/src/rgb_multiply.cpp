#include <windows.h>

COLORREF RGBMultiply(COLORREF color, float val)
{
    return RGB(
        GetRValue(color) * val,
        GetGValue(color) * val,
        GetBValue(color) * val);
}