#include "lab1.hpp"

LRESULT CALLBACK    OnWmCTLColorEdit(HDC hdc, DWORD dword)
{
    static auto in_text1_brush = CreateSolidBrush(INPUT1_BG_COLOR);

    if (dword == IDC_INTEXT1)
    {
        SetBkColor(hdc, INPUT1_BG_COLOR);
        SetTextColor(hdc, INPUT1_TXT_COLOR);
        return (INT_PTR)in_text1_brush;
    }
    return 0;
}