#include <iostream>
#include "lab1.hpp"
#include "resources.h"

LRESULT CALLBACK WindowProcedure(HWND, UINT, WPARAM, LPARAM);
TCHAR szClassName[ ] = _T("CodeBlocksWindowsApp");

WNDCLASSEX  g_wincl;
HWND        g_hwnd;

Mp3Player*  g_mp3_player;
ScrollCtrl* g_scroll_ctrl;

int WINAPI WinMain(
    HINSTANCE hThisInstance,
    HINSTANCE hPrevInstance,
    LPSTR lpszArgument,
    int nCmdShow)
{
    MSG messages;

    g_wincl.hInstance = hThisInstance;
    g_wincl.lpszClassName = szClassName;
    g_wincl.lpfnWndProc = WindowProcedure;
    g_wincl.style = CS_DBLCLKS;
    g_wincl.cbSize = sizeof (WNDCLASSEX);

    g_wincl.hIcon = LoadIcon(g_wincl.hInstance, MAKEINTRESOURCE(IDR_ICO_MAIN));
    g_wincl.hIconSm = LoadIcon(g_wincl.hInstance, MAKEINTRESOURCE(IDR_ICO_MAIN));
    g_wincl.hCursor = LoadCursor(g_wincl.hInstance, MAKEINTRESOURCE(IDR_CUSTOM_CURSOR));
    g_wincl.lpszMenuName = MAKEINTRESOURCE(SYSTEM_MENU_ID);
    g_wincl.cbClsExtra = 0;
    g_wincl.cbWndExtra = 0;
    g_wincl.hbrBackground = (HBRUSH)COLOR_BACKGROUND;

    SetCursor(g_wincl.hCursor);

    if (!RegisterClassEx(&g_wincl))
        return 0;

    g_hwnd = CreateWindowEx(
        0,                   /* Extended possibilites for variation */
        szClassName,         /* Classname */
        _T("My sexy wav player"),
        WS_OVERLAPPEDWINDOW | WS_HSCROLL | WS_VSCROLL,
        CW_USEDEFAULT,       /* Windows decides the position */
        CW_USEDEFAULT,       /* where the window ends up on the screen */
        544,                 /* The programs width */
        375,                 /* and height in pixels */
        HWND_DESKTOP,        /* The window is a child-window to desktop */
        NULL,                /* No menu */
        hThisInstance,       /* Program Instance handler */
        NULL                 /* No Window Creation data */
        );
    ShowWindow (g_hwnd, nCmdShow);
    while (GetMessage (&messages, NULL, 0, 0))
    {
        TranslateMessage(&messages);
        DispatchMessage(&messages);
    }
    return messages.wParam;
}

LRESULT CALLBACK WindowProcedure(
    HWND hwnd,
    UINT message,
    WPARAM wParam,
    LPARAM lParam)
{
    switch (message)
    {
        case WM_CREATE:
            OnWmCreate(hwnd, wParam, lParam);
            break;
        case WM_SIZE:
            OnWmSize(hwnd, LOWORD(lParam), HIWORD(lParam));
            break;
        case WM_CTLCOLORSTATIC:
            return OnWmCTLColorStatic(GetDlgCtrlID((HWND)lParam), wParam);
        case WM_CTLCOLOREDIT:
            return OnWmCTLColorEdit((HDC)wParam, GetDlgCtrlID((HWND)lParam));
        case WM_DRAWITEM:
            OnWmDrawItem(wParam, lParam);
            break;
        case WM_COMMAND:
            OnWmCommand(LOWORD(wParam), wParam, lParam);
            break;
        case WM_KEYDOWN:
            g_mp3_player->OnWmKeyDown(wParam);
            break;
        case WM_HSCROLL:
		    g_scroll_ctrl->OnWmHScroll(hwnd, message, wParam, lParam);
            g_mp3_player->OnWmHScroll(hwnd, message, wParam, lParam);
            break;
        case WM_VSCROLL:
            g_scroll_ctrl->OnWmVScroll(hwnd, message, wParam, lParam);
            break;
        case WM_CTLCOLORLISTBOX:
            return g_mp3_player->OnWmCtlColorListBox(wParam, lParam);
        case WM_DESTROY:
            OnWmDestroy();
            break;
        default:
            return DefWindowProc(hwnd, message, wParam, lParam);
    }
    return 0;
}

