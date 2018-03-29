#ifndef _LAB1_HPP_
# define _LAB1_HPP_

# if defined(UNICODE) && !defined(_UNICODE)
    #define _UNICODE
# elif defined(_UNICODE) && !defined(UNICODE)
    #define UNICODE
# endif

# include <tchar.h>
# include <windows.h>
# include <iostream>

# include "Mp3Player.hpp"
# include "ScrollCtrl.hpp"
# include "resources.h"

/*
** Main window parameters
*/

# define MIN_WIN_WIDTH 500
# define MIN_WIN_HEIGHT 300

# define SCROLL_BAR_HEIGHT 20

/*
** Global variables
*/

extern WNDCLASSEX   g_wincl;
extern HWND         g_hwnd;
extern Mp3Player*   g_mp3_player;
extern ScrollCtrl*  g_scroll_ctrl;

/*
** Window procedures
*/

void                OnWmCreate(HWND hwnd, WPARAM wParam, LPARAM lParam);
void                OnWmSize(HWND hwnd, int cx, int cy);
void                OnWmGetMinMaxInfo(LPARAM lParam);
LRESULT CALLBACK    OnWmCTLColorStatic(DWORD ctrlID, WPARAM wParam);
LRESULT CALLBACK    OnWmCTLColorEdit(HDC hdc, DWORD dword);
void                OnWmDrawItem(WPARAM wParam, LPARAM lParam);
void                OnWmCommand(WORD cmd_word, WPARAM wParam, LPARAM lParam);
void                OnWmDestroy();

/*
** Utils
*/

COLORREF            RGBMultiply(COLORREF color, float val);

template<class T>
constexpr const T&  clamp(const T& v, const T& lo, const T& hi)
{
    if (v < lo)
        return lo;
    if (v > hi)
        return hi;
    return v;
}

#endif
