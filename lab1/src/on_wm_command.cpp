#include "lab1.hpp"
#include <iostream>

static void TransferText_(HWND edit1, HWND edit2)
{
    char    buf[256];

    GetWindowText(edit1, buf, sizeof(buf) - 1);
    SetWindowText(edit2, buf);
}

void        OnWmCommand(WORD cmd_word)
{
    switch (cmd_word)
    {
        case IDC_BTN1:
            TransferText_(input_field_1, input_field_2);
            break;
        case IDC_BTN2:
            SendMessage(btn_bot, WM_SETFONT, (WPARAM)hfont, TRUE);
            break;
    }
}