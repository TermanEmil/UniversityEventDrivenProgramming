#include "lab1.hpp"
#include <iostream>
#include "idc.h"

void        OnWmCommand(WORD cmd_word, WPARAM wParam, LPARAM lParam)
{
    if (g_mp3_player->OnWmCommand(cmd_word, wParam, lParam))
        return;

    switch (cmd_word)
    {
        case SYS_MENU_EXIT_BTN_IDC:
            SendMessage(g_hwnd, WM_DESTROY, 0, 0);
            break;
        case SYS_MENU_RESTART_BTN_IDC:
            g_mp3_player->RestartCurrent();
            break;
    }
}