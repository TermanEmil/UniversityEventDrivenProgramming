#include "lab1.hpp"
#include <iostream>

LRESULT CALLBACK WindowProcedure(HWND, UINT, WPARAM, LPARAM);
TCHAR szClassName[ ] = _T("CodeBlocksWindowsApp");

HWND    btn_top, btn_bot;
HWND    text_field_1, text_field_2;
HWND    input_field_1, input_field_2;
HFONT   hfont;

int WINAPI WinMain(HINSTANCE hThisInstance,
    HINSTANCE hPrevInstance,
    LPSTR lpszArgument,
    int nCmdShow)
{
    HWND hwnd;
    MSG messages;
    WNDCLASSEX wincl;

    wincl.hInstance = hThisInstance;
    wincl.lpszClassName = szClassName;
    wincl.lpfnWndProc = WindowProcedure;
    wincl.style = CS_DBLCLKS;
    wincl.cbSize = sizeof (WNDCLASSEX);

    wincl.hIcon = LoadIcon (NULL, IDI_APPLICATION);
    wincl.hIconSm = LoadIcon (NULL, IDI_APPLICATION);
    wincl.hCursor = LoadCursor (NULL, IDC_ARROW);
    wincl.lpszMenuName = NULL;
    wincl.cbClsExtra = 0;
    wincl.cbWndExtra = 0;
    wincl.hbrBackground = (HBRUSH)COLOR_BACKGROUND;

    if (!RegisterClassEx(&wincl))
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
        case WM_RBUTTONDOWN:
            OnWmRBtnDown(hwnd);
            break;
        case WM_RBUTTONDBLCLK:
            MoveWindow(hwnd, 0, 0, 500, 500, TRUE);
            break;
        case WM_LBUTTONDOWN:
            MoveWindow(hwnd, 0, 0, 1000, 1000, TRUE);
            break;
        case WM_DESTROY:
            OnWmDestroy();
            break;
        default:
            return DefWindowProc(hwnd, message, wParam, lParam);
    }

    return 0;
}
