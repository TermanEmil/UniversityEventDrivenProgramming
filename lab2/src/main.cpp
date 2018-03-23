#include <iostream>

#include "lab1.hpp"

LRESULT CALLBACK WindowProcedure(HWND, UINT, WPARAM, LPARAM);
TCHAR szClassName[ ] = _T("CodeBlocksWindowsApp");

WNDCLASSEX  g_wincl;
Mp3Player*  g_mp3_player;

int WINAPI WinMain(HINSTANCE hThisInstance,
    HINSTANCE hPrevInstance,
    LPSTR lpszArgument,
    int nCmdShow)
{
    HWND hwnd;
    MSG messages;

    g_wincl.hInstance = hThisInstance;
    g_wincl.lpszClassName = szClassName;
    g_wincl.lpfnWndProc = WindowProcedure;
    g_wincl.style = CS_DBLCLKS;
    g_wincl.cbSize = sizeof (WNDCLASSEX);

    g_wincl.hIcon = LoadIcon (NULL, IDI_APPLICATION);
    g_wincl.hIconSm = LoadIcon (NULL, IDI_APPLICATION);
    g_wincl.hCursor = LoadCursor (NULL, IDC_ARROW);
    g_wincl.lpszMenuName = NULL;
    g_wincl.cbClsExtra = 0;
    g_wincl.cbWndExtra = 0;
    g_wincl.hbrBackground = (HBRUSH)COLOR_BACKGROUND;

    if (!RegisterClassEx(&g_wincl))
        return 0;

    hwnd = CreateWindowEx(
        0,                   /* Extended possibilites for variation */
        szClassName,         /* Classname */
        _T("Lab1"),          /* Title Text */
        WS_OVERLAPPEDWINDOW, /* default window */
        CW_USEDEFAULT,       /* Windows decides the position */
        CW_USEDEFAULT,       /* where the window ends up on the screen */
        544,                 /* The programs width */
        375,                 /* and height in pixels */
        HWND_DESKTOP,        /* The window is a child-window to desktop */
        NULL,                /* No menu */
        hThisInstance,       /* Program Instance handler */
        NULL                 /* No Window Creation data */
        );
    ShowWindow (hwnd, nCmdShow);
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
            OnWmSize(hwnd, wParam, lParam);
            break;
        case WM_GETMINMAXINFO:
            OnWmGetMinMaxInfo(lParam);
            break;
        case WM_CTLCOLORSTATIC:
            return OnWmCTLColorStatic(GetDlgCtrlID((HWND)lParam), wParam);
        case WM_CTLCOLOREDIT:
            return OnWmCTLColorEdit((HDC)wParam, GetDlgCtrlID((HWND)lParam));
        case WM_DRAWITEM:
            OnWmDrawItem(wParam, lParam);
            break;
        case WM_COMMAND:
            OnWmCommand(LOWORD(wParam));
            break;
        case WM_DESTROY:
            OnWmDestroy();
            break;
        default:
            return DefWindowProc(hwnd, message, wParam, lParam);
    }

    return 0;
}
