#include "lab1.hpp"

static void     DrawBtn_(
    LPDRAWITEMSTRUCT lpdis,
    const char *btn_txt,
    COLORREF textColor,
    COLORREF bgColor)
{
    SIZE    size;

    GetTextExtentPoint32(lpdis->hDC, btn_txt, strlen(btn_txt), &size);
    SetTextColor(lpdis->hDC, bgColor);
    SetBkColor(lpdis->hDC, textColor);

    ExtTextOut(
        lpdis->hDC,
        ((lpdis->rcItem.right - lpdis->rcItem.left) - size.cx) / 2,
        ((lpdis->rcItem.bottom - lpdis->rcItem.top) - size.cy) / 2,
        ETO_OPAQUE | ETO_CLIPPED,
        &lpdis->rcItem,
        btn_txt,
        strlen(btn_txt),
        NULL);

    DrawEdge(
        lpdis->hDC,
        &lpdis->rcItem,
        (lpdis->itemState & ODS_SELECTED) ? EDGE_SUNKEN : EDGE_RAISED,
        BF_RECT);
}

void        OnWmDrawItem(WPARAM wParam, LPARAM lParam)
{
    auto lpdis = (DRAWITEMSTRUCT*)lParam;

    if ((UINT)wParam == IDC_BTN1)
        DrawBtn_(lpdis, BTN_CPY_TOP_TXT, RGB(240, 128, 128), RGB(255, 0, 102));
}