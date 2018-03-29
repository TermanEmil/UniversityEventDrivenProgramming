#include "ScrollCtrl.hpp"
#include "lab1.hpp"
#include <algorithm>

void ScrollCtrl::OnWmSizeInternal(int cx, int cy)
{
    SCROLLINFO horz_scroll_info;
    SCROLLINFO vert_scroll_info;
    
    cx_ = cx;
    cy_ = cy;

	horz_scroll_info.cbSize = sizeof(horz_scroll_info);
	horz_scroll_info.fMask = SIF_ALL;
	horz_scroll_info.nMin = 0;
	horz_scroll_info.nMax = (cx < MIN_WIN_WIDTH) ? MIN_WIN_WIDTH - cx : 0;
	horz_scroll_info.nPage = cx / 20;
    horz_scroll_info.nPos = horiz_offset;

	vert_scroll_info.cbSize = sizeof(vert_scroll_info);
	vert_scroll_info.fMask = SIF_ALL;
	vert_scroll_info.nMin = 0;
	vert_scroll_info.nMax = (cy < MIN_WIN_HEIGHT) ? MIN_WIN_HEIGHT - cy : 0;
	vert_scroll_info.nPage = cy / 20;
    vert_scroll_info.nPos = vert_offset;

    SetScrollInfo(g_hwnd, SB_HORZ, &horz_scroll_info, TRUE);
    SetScrollInfo(g_hwnd, SB_VERT, &vert_scroll_info, TRUE);

    if (cx > MIN_WIN_WIDTH)
        horiz_offset = 0;
    if (cy > MIN_WIN_HEIGHT)
        vert_offset = 0;
}

void ScrollCtrl::OnWmHScroll(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    int scroll_pos = HIWORD(wParam);
	
    if (lParam == 0)
	{
		switch (LOWORD(wParam))
		{
            case SB_THUMBTRACK:
                horiz_offset = scroll_pos;
                break;
            case SB_LINERIGHT:
                horiz_offset++;
                break;
            case SB_LINELEFT:
                horiz_offset--;
                break;
		}
        horiz_offset = clamp((int)horiz_offset, 0, MIN_WIN_WIDTH - cx_);

		SetScrollPos(hwnd, SB_HORZ, horiz_offset, TRUE);
		OnWmSize(g_hwnd, (int)cx_, (int)cy_);
	}
}

void ScrollCtrl::OnWmVScroll(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    int scroll_pos = HIWORD(wParam);

	if (lParam == 0)
	{
		switch(LOWORD(wParam))
		{
            case SB_THUMBTRACK:
                vert_offset = scroll_pos;
                break;
            case SB_LINEUP:
                vert_offset--;
                break;
            case SB_LINEDOWN:
                vert_offset++;
                break;
		}
        vert_offset = clamp((int)vert_offset, 0, MIN_WIN_HEIGHT - cy_);

        SetScrollPos(hwnd, SB_VERT, vert_offset, TRUE);
		OnWmSize(g_hwnd, (int)cx_, (int)cy_);
	}
}
