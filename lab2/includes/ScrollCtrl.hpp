#ifndef _SCROLL_CTRL_HPP_
# define _SCROLL_CTRL_HPP_

# include <windows.h>

class ScrollCtrl
{
public:
    float horiz_offset = 0;
    float vert_offset = 0;

    void OnWmSizeInternal(int cx, int cy);
    void OnWmHScroll(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);
    void OnWmVScroll(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);
    
private:
    int cx_;
    int cy_;
};

#endif