#include "lab1.hpp"
#include <iostream>

static void CreateFont_()
{
    LOGFONT logFont;

    memset(&logFont, 0, sizeof(logFont));
    logFont.lfHeight = -(LABEL_HEIGHT - 5);
    logFont.lfWeight = FW_BOLD;
    logFont.lfPitchAndFamily = FF_MODERN;
    strcpy(logFont.lfFaceName, "Broadway");
    hfont = CreateFontIndirect(&logFont);
}

static void CreateBtns_(HWND hwnd)
{
    btn_top = CreateWindow(
        "BUTTON",
        BTN_CPY_TOP_TXT,
        WS_VISIBLE | WS_CHILD | WS_BORDER | BS_OWNERDRAW,
        0,
        0,
        BTN_WIDTH,
        BTN_HEIGHT,
        hwnd,
        (HMENU)IDC_BTN1,
        NULL,
        NULL);

    btn_bot = CreateWindow(
        "BUTTON",
        BTN_CPY_BOT_TXT,
        WS_VISIBLE | WS_CHILD | WS_BORDER,
        0, 0, 0, 0,
        hwnd,
        (HMENU)IDC_BTN2,
        NULL,
        NULL);

    SendMessage(btn_top, WM_SETFONT, (WPARAM)hfont, TRUE);
}

static void CreateLabels_(HWND hwnd)
{
    text_field_1 = CreateWindow(
        "STATIC",
        LABEL1,
        WS_VISIBLE | WS_CHILD | SS_SUNKEN | SS_CENTER | SS_CENTERIMAGE,
        0, 0, 0, 0,
        hwnd,
        (HMENU)IDC_LABEL1,
        NULL,
        NULL);
    
    text_field_2 = CreateWindow(
        "STATIC",
        LABEL2,
        WS_VISIBLE | WS_CHILD | SS_SUNKEN | SS_CENTER | SS_CENTERIMAGE,
        0, 0, 0, 0,
        hwnd,
        (HMENU)IDC_LABEL2,
        NULL,
        NULL);
}

static void CreateEditTexts_(HWND hwnd)
{
    input_field_1 = CreateWindow(
        "EDIT",
        "Some fluffy text",
        WS_VISIBLE | WS_CHILD | ES_MULTILINE | ES_AUTOVSCROLL | WS_VSCROLL,
        0, 0, 0, 0,
        hwnd,
        (HMENU)IDC_INTEXT1,
        NULL,
        NULL);

    input_field_2 = CreateWindow(
        "EDIT",
        "Pinky winky",
        WS_VISIBLE | WS_CHILD | ES_MULTILINE | ES_AUTOVSCROLL | WS_VSCROLL,
        0, 0, 0, 0,
        hwnd,
        (HMENU)IDC_INTEXT2,
        NULL,
        NULL);

    SendMessage(
        input_field_1,
        WM_SETFONT,
        (WPARAM)hfont,
        0);
}

void        OnWmCreate(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
    std::cout << "On Create" << std::endl;

    CreateFont_();
    CreateBtns_(hwnd);
    CreateLabels_(hwnd);
    CreateEditTexts_(hwnd);
}