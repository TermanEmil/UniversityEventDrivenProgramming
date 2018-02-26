#include "lab1.hpp"
#include <iostream>

static void MoveBtns_(int cxCoord, int cyCoord)
{
    MoveWindow(
        btn_top,
        cxCoord - BTN_WIDTH - BTN_XPOS_CELLSPAN,
        (cyCoord) / 3 - BTNY_POS - BTN1_Y,
        BTN_WIDTH,
        BTN_HEIGHT,
        TRUE);

    MoveWindow(
        btn_bot,
        cxCoord - BTN_WIDTH - BTN_XPOS_CELLSPAN,
        (cyCoord) / 3 - BTNY_POS - BTN2_Y,
        BTN_WIDTH,
        BTN_HEIGHT,
        TRUE);
}

static void MoveLabels_(int cxCoord, int cyCoord)
{
    MoveWindow(
        text_field_1,
        (cxCoord - 230) / 2 ,
        20,
        230,
        LABEL_HEIGHT,
        TRUE);

    MoveWindow(
        text_field_2,
        (cxCoord - 150) / 2 ,
        cyCoord - LABEL_HEIGHT - 10,
        150,
        LABEL_HEIGHT,
        TRUE);
}

static void MoveEditTexts_(int cxCoord, int cyCoord)
{
    int width, height;

    width = cxCoord * INPUT_WIDTH_PART;
    height = cyCoord * INPUT_HEIGHT_PART;

    MoveWindow(
        input_field_1,
        10,
        50,
        width,
        height,
        TRUE);

    MoveWindow(
        input_field_2,
        10,
        cyCoord - height - 50,
        width,
        height,
        TRUE);
}

void        OnWmSize(HWND hwnd, WPARAM wParam, LPARAM lParam)
{
    int cxCoord, cyCoord;

    cxCoord = LOWORD(lParam);
    cyCoord = HIWORD(lParam);

    MoveBtns_(cxCoord, cyCoord);
    MoveLabels_(cxCoord, cyCoord);
    MoveEditTexts_(cxCoord, cyCoord);

    RedrawWindow(hwnd, NULL, NULL, RDW_INVALIDATE | RDW_ERASE);
}