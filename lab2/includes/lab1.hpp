#ifndef _LAB1_HPP_
# define _LAB1_HPP_

# include "resources.h"

# if defined(UNICODE) && !defined(_UNICODE)
    #define _UNICODE
# elif defined(_UNICODE) && !defined(UNICODE)
    #define UNICODE
# endif

# include <tchar.h>
# include <windows.h>

# include "Mp3Player.hpp"

/*
** Main window parameters
*/

# define MIN_WIN_WIDTH 500
# define MIN_WIN_HEIGHT 300

/*
** Global variables
*/

extern WNDCLASSEX   g_wincl;
extern Mp3Player*   g_mp3_player;

/*
** Window procedures
*/

void                OnWmCreate(HWND hwnd, WPARAM wParam, LPARAM lParam);
void                OnWmSize(HWND hwnd, WPARAM wParam, LPARAM lParam);
void                OnWmGetMinMaxInfo(LPARAM lParam);
LRESULT CALLBACK    OnWmCTLColorStatic(DWORD ctrlID, WPARAM wParam);
LRESULT CALLBACK    OnWmCTLColorEdit(HDC hdc, DWORD dword);
void                OnWmDrawItem(WPARAM wParam, LPARAM lParam);
void                OnWmCommand(WORD cmd_word);
void                OnWmDestroy();
void                OnWmRBtnDown(HWND hwnd);

#endif
