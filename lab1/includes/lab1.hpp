#ifndef _LAB1_HPP_
# define _LAB1_HPP_

# if defined(UNICODE) && !defined(_UNICODE)
    #define _UNICODE
# elif defined(_UNICODE) && !defined(UNICODE)
    #define UNICODE
# endif

# include <tchar.h>
# include <windows.h>

/*
** Main window parameters
*/

# define MIN_WIN_HEIGHT 300
# define MIN_WIN_WIDTH 500

/*
** IDC
*/

# define IDC_BTN1 1
# define IDC_BTN2 2
# define IDC_INTEXT1 3
# define IDC_INTEXT2 4
# define IDC_LABEL1 5
# define IDC_LABEL2 6

/*
** Buttons
*/

# define BTN_WIDTH 100
# define BTN_HEIGHT 35
# define BTN_XPOS_CELLSPAN 10
# define BTNY_POS 0
# define BTN1_Y 0
# define BTN2_Y (BTN_HEIGHT + 10)
# define BTN_CPY_TOP_TXT "Cpy Top"
# define BTN_CPY_BOT_TXT "Myork"

/*
** Static labels.
*/

# define LABEL1 "I love unicorns "
# define LABEL2 "Coz they are cute"
# define LABEL_HEIGHT 25
# define LABEL1_COLOR   RGB(255, 0, 0)
# define LABEL1_BGCOLOR RGB(255, 192, 203)

/*
** Input fields
*/

# define INPUT_WIDTH_PART 0.7
# define INPUT_HEIGHT_PART 0.3
# define INPUT1_BG_COLOR RGB(255, 192, 203)
# define INPUT1_TXT_COLOR LABEL1_COLOR

/*
** Global variables
*/

extern HWND     btn_top, btn_bot;
extern HWND     text_field_1, text_field_2;
extern HWND     input_field_1, input_field_2;
extern HFONT    hfont;

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
