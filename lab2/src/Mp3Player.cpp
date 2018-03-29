#include "Mp3Player.hpp"
#include "lab1.hpp"
#include "idc.h"
#include <Commdlg.h>
#include <Winuser.h>
#include <Mmsystem.h>
#include <algorithm> 

Mp3Player::Mp3Player(HWND parent_hwnd)
{
    parent_hwnd_ = parent_hwnd;

    music_dialog_box_ = CreateWindowEx(
        WS_EX_CLIENTEDGE,
        TEXT("listbox"),
        "",
        WS_CHILD | WS_VISIBLE | WS_VSCROLL | ES_AUTOVSCROLL | LBS_NOTIFY,
        0, 0, 0, 0,
        parent_hwnd,
        (HMENU)MP3_MUSIC_DIALOG_BOX_IDC,
        NULL,
        NULL);
    SendMessage(music_dialog_box_, LB_RESETCONTENT, 0, 0);

    add_music_btn_ = CreateWindow(
        "BUTTON",
        "Add",
        WS_VISIBLE | WS_CHILD | WS_BORDER,
        0, 0, 0, 0,
        parent_hwnd,
        (HMENU)MP3_ADD_MUSIC_BTN_IDC,
        NULL,
        NULL);

    play_btn_ = CreateWindow(
        "BUTTON",
        "Play",
        WS_VISIBLE | WS_CHILD | WS_BORDER,
        0, 0, 0, 0,
        parent_hwnd,
        (HMENU)MP3_PLAY_BTN_IDC,
        NULL,
        NULL);

    pause_btn_ = CreateWindow(
        "BUTTON",
        "Stop",
        WS_VISIBLE | WS_CHILD | WS_BORDER,
        0, 0, 0, 0,
        parent_hwnd,
        (HMENU)MP3_STOP_BTN_IDC,
        NULL,
        NULL);

    next_btn_ = CreateWindow(
        "BUTTON",
        "Next",
        WS_VISIBLE | WS_CHILD | WS_BORDER,
        0, 0, 0, 0,
        parent_hwnd,
        (HMENU)MP3_NEXT_BTN_IDC,
        NULL,
        NULL);

    volume_bar_ = CreateWindowEx(
		0,
		"SCROLLBAR",
		NULL,
		WS_CHILD | WS_VISIBLE | SBS_HORZ,
		0, 0, 0, 0,
		parent_hwnd,
		(HMENU)MP3_VOLUME_BAR_IDC,
		GetModuleHandle(NULL),
		NULL
	);
	SetScrollRange(volume_bar_, SB_CTL, 0, 100, TRUE);
    SetScrollPos(volume_bar_, SB_CTL, 100, TRUE);

    left_chanel_volume_bar_ = CreateWindowEx(
		0,
		"SCROLLBAR",
		NULL,
		WS_CHILD | WS_VISIBLE | SBS_HORZ,
		0, 0, 0, 0,
		parent_hwnd,
		(HMENU)MP3_LCHANEL_VOLUME_BAR_IDC,
		GetModuleHandle(NULL),
		NULL
	);
	SetScrollRange(left_chanel_volume_bar_, SB_CTL, 0, 100, TRUE);
    SetScrollPos(left_chanel_volume_bar_, SB_CTL, 100, TRUE);

    right_chanel_volume_bar_ = CreateWindowEx(
		0,
		"SCROLLBAR",
		NULL,
		WS_CHILD | WS_VISIBLE | SBS_HORZ,
		0, 0, 0, 0,
		parent_hwnd,
		(HMENU)MP3_RCHANEL_VOLUME_BAR_IDC,
		GetModuleHandle(NULL),
		NULL
	);
	SetScrollRange(right_chanel_volume_bar_, SB_CTL, 0, 100, TRUE);
    SetScrollPos(right_chanel_volume_bar_, SB_CTL, 100, TRUE);

    musics = std::list<Music*>();
    SetVolume(1);
}

Mp3Player::~Mp3Player()
{
    for (auto iter = musics.begin(), end = musics.end(); iter != end; iter++)
        delete(*iter);
}

void Mp3Player::AddMusic(std::string path)
{
    if (path.empty())
        return;

    auto new_music = new Music(path);
    musics.push_back(new_music);

    SendMessage(
        music_dialog_box_,
        LB_ADDSTRING,
        0,
        (LPARAM)path.c_str());
}

void Mp3Player::RestartCurrent()
{
    if (currently_playing_ == NULL)
        return;

    auto music_path = currently_playing_->path;

    if (PlaySound(TEXT(music_path.c_str()), NULL, SND_FILENAME | SND_ASYNC) == FALSE)
    {
        MessageBox(
            NULL,
            ("Failed to restart " + music_path).c_str(),
            _T("Error"),
            MB_OK);
        return;
    }
}

void Mp3Player::PlaySelected()
{
    std::string music_path;

    std::cout << "Play Selected" << std::endl;

    if (GetSelectedText_(&music_path) == -1)
    {
        MessageBox(NULL, "Please select a music", _T("Nothing selected"), MB_OK);
        return;
    }

    Play_(music_path);
}

void Mp3Player::Stop()
{
    std::cout << "Stop" << std::endl;

    if (currently_playing_ == NULL)
        return;

    PlaySound(NULL, NULL, 0);
    currently_playing_ = NULL;
}

void Mp3Player::PlayNext()
{
    static TCHAR text_buf[MAX_PATH + 1];
    int index;

    std::cout << "Play Next" << std::endl;

    auto item = GetDlgItem(parent_hwnd_, MP3_MUSIC_DIALOG_BOX_IDC);
    int max_count = SendMessage(item, LB_GETCOUNT, 0, 0);

    if (max_count == 0)
        return;

    if (currently_playing_ == NULL)
        index = 0;
    else
    {
        int current_index = SendMessage(
            item,
            LB_FINDSTRING,
            -1,
            (LPARAM)(currently_playing_->path.c_str()));

        if (current_index + 1 >= max_count)
        {
            index = 0;
        }
        else
            index = current_index + 1;
    }
    SendMessage(item, LB_GETTEXT, (WPARAM)index, (LPARAM)text_buf);
    Play_(text_buf);
}

/*
** Receives a normalized value.
*/

void Mp3Player::SetVolume(float volume)
{
    DWORD win_volume = MAKELPARAM(
        0xFFFF * volume * left_chanel_volume_ / 100,
        0xFFFF * volume * right_chanel_volume_ / 100);
    waveOutSetVolume(NULL, win_volume);
}

/*
** Built in.
*/

void Mp3Player::OnWmSize(int cx, int cy)
{
    cx = std::max(MIN_WIN_WIDTH, cx);
    cy = std::max(MIN_WIN_HEIGHT, cy);

    auto width = dialog_init_width_ / MIN_WIN_WIDTH * cx;
    auto height = cy - top_padding_ - bot_padding_;

    MoveWindow(
        music_dialog_box_,
        cx / 2 - width / 2  - g_scroll_ctrl->horiz_offset,
        top_padding_ - g_scroll_ctrl->vert_offset,
        width,
        height - (SCROLL_BAR_HEIGHT + 3) * 3 - bot_padding_,
        TRUE);

    width = btn_init_width_;
    height = btn_init_height_;

    MoveWindow(
        add_music_btn_,
        cx - right_padding_ - width - g_scroll_ctrl->horiz_offset,
        cy - bot_padding_ - height - g_scroll_ctrl->vert_offset,
        width,
        height,
        TRUE);
    
    MoveWindow(
        play_btn_,
        left_padding_ - g_scroll_ctrl->horiz_offset,
        top_padding_ - g_scroll_ctrl->vert_offset,
        width,
        height,
        TRUE);
    
    MoveWindow(
        pause_btn_,
        left_padding_ + width + 10 - g_scroll_ctrl->horiz_offset,
        top_padding_ - g_scroll_ctrl->vert_offset,
        width,
        height,
        TRUE);
    
    MoveWindow(
        next_btn_,
        left_padding_ - g_scroll_ctrl->horiz_offset,
        top_padding_ * 2 + height - g_scroll_ctrl->vert_offset,
        width,
        height,
        TRUE);

    width = volume_bar_width_ / MIN_WIN_WIDTH * cx;
    height = SCROLL_BAR_HEIGHT;

    MoveWindow(
        volume_bar_,
		cx / 2 - width / 2 - g_scroll_ctrl->horiz_offset,
        cy - bot_padding_ - 3 * (height + 3) - g_scroll_ctrl->vert_offset,
		width,
        height,
		TRUE);
    
    MoveWindow(
        left_chanel_volume_bar_,
		cx / 2 - width / 2 - g_scroll_ctrl->horiz_offset,
        cy - bot_padding_ - 2 * (height + 3) - g_scroll_ctrl->vert_offset,
		width,
        height,
		TRUE);
    
    MoveWindow(
        right_chanel_volume_bar_,
		cx / 2 - width / 2 - g_scroll_ctrl->horiz_offset,
        cy - bot_padding_ - 1 * (height + 3) - g_scroll_ctrl->vert_offset,
		width,
        height,
		TRUE);
}

bool Mp3Player::OnWmCommand(WORD cmd_word, WPARAM wParam, LPARAM lParam)
{
    auto rs = false;

    switch (cmd_word)
    {
        case MP3_ADD_MUSIC_BTN_IDC:
            AddMusic(ChooseFile_());
            rs = true;
            break;
        case MP3_PLAY_BTN_IDC:
            PlaySelected();
            rs = true;
            break;
        case MP3_STOP_BTN_IDC:
            Stop();
            rs = true;
            break;
        case MP3_NEXT_BTN_IDC:
            PlayNext();
            rs = true;
            break;
        case MP3_MUSIC_DIALOG_BOX_IDC:
            switch (HIWORD(wParam))
            {
                case LBN_DBLCLK:
                    DialogBox(
                        g_wincl.hInstance,
                        MAKEINTRESOURCE(IDD_DLGFIRST),
	                    parent_hwnd_,
                        (DLGPROC)DeleteItemProc_);
                    return true;
            }
            return true;
    }
    if (rs)
        SetFocus(g_hwnd);
    return false;
}

bool Mp3Player::OnWmKeyDown(WPARAM wParam)
{
    std::cout << "Key down" << std::endl;
    switch(wParam)
    {
        case 'N':
            if(GetAsyncKeyState(VK_CONTROL))
            {
                std::cout << "Ctrl + N" << std::endl;
                PlayNext();
                return true;
            }
            break;
        case 'S':
            if(GetAsyncKeyState(VK_CONTROL))
            {
                std::cout << "Ctrl + S" << std::endl;
                Stop();
                return true;
            }
            break;
    }
    return false;
}

void Mp3Player::OnWmHScroll(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    int scroll_pos = HIWORD(wParam);

    if (hwnd == NULL)
        return;

	auto scb_handle = (HWND)lParam;
    auto idc = GetDlgCtrlID(scb_handle);

    float* target_offset;

    switch (idc)
    {
        case MP3_VOLUME_BAR_IDC:
            target_offset = &volume_scb_offset_;
            break;
        case MP3_LCHANEL_VOLUME_BAR_IDC:
            target_offset = &left_chanel_volume_;
            break;
        case MP3_RCHANEL_VOLUME_BAR_IDC:
            target_offset = &right_chanel_volume_;
            break;
        default:
            return;
    }

    switch (LOWORD(wParam))
    {
        case SB_THUMBTRACK:
        case SB_THUMBPOSITION:
            *target_offset = scroll_pos;
            break;
        case SB_LINEUP:
            (*target_offset)--;
            break;
        case SB_LINEDOWN:
            (*target_offset)++;
            break;
    }

    SetScrollPos(scb_handle, SB_CTL, *target_offset, TRUE);
    SetVolume(volume_scb_offset_ / 100);

    if (idc == MP3_VOLUME_BAR_IDC)
        RedrawWindow(music_dialog_box_, NULL, NULL, RDW_INVALIDATE | RDW_ERASE);
}

LRESULT CALLBACK Mp3Player::OnWmCtlColorListBox(WPARAM wParam, LPARAM lParam)
{
    auto hdc = (HDC)wParam;
    auto color_mult = volume_scb_offset_ / 100;

    color_mult = (1 - color_mult) * 0.5 + color_mult * 1;

    SetTextColor(hdc, RGBMultiply(listbox_txt_, color_mult));
    SetBkColor(hdc, RGBMultiply(listbox_bg_, color_mult));

    return (LRESULT)CreateSolidBrush(RGBMultiply(listbox_bg_, color_mult));
}

/*
** Open a dialog box to choose a music file.
*/

std::string Mp3Player::ChooseFile_()
{
    OPENFILENAME    ofn;
    char            szFileName[MAX_PATH] = "";

    ZeroMemory(&ofn, sizeof(ofn));

    ofn.lStructSize = sizeof(ofn);
    ofn.hwndOwner = parent_hwnd_;
    ofn.lpstrFilter = "Wav\0*.wav\0";
    ofn.lpstrFile = szFileName;
    ofn.nMaxFile = MAX_PATH;
    ofn.Flags = OFN_EXPLORER | OFN_FILEMUSTEXIST | OFN_HIDEREADONLY;
    ofn.lpstrDefExt = "wav";

    if(GetOpenFileName(&ofn))
        return std::string(szFileName);
    else
        return std::string("");
}

/*
** Get the text of the currently selected element from the list box.
*/

int Mp3Player::GetSelectedText_(std::string* result)
{
    static TCHAR text_buf[MAX_PATH + 1];

    auto item = GetDlgItem(g_hwnd, MP3_MUSIC_DIALOG_BOX_IDC);
    auto iCurSel = SendMessage(item, LB_GETCURSEL, 0, 0);

    if (iCurSel == LB_ERR)
        return -1;

    SendMessage(item, LB_GETTEXT, (WPARAM)iCurSel, (LPARAM)text_buf);
    *result = std::string(text_buf);
    return 0;
}

void Mp3Player::Play_(std::string music_path)
{
    if (PlaySound(TEXT(music_path.c_str()), NULL, SND_FILENAME | SND_ASYNC) == FALSE)
    {
        MessageBox(
            NULL,
            ("Failed to play " + music_path).c_str(),
            _T("Error"),
            MB_OK);
        return;
    }
    
    auto finder = std::find_if(musics.begin(), musics.end(),
        [music_path](const Music* i)
        {
            return i->path.compare(music_path) == 0;
        });
    currently_playing_ = *finder;
}

/*
** List box: item dialog box.
*/

BOOL CALLBACK Mp3Player::DeleteItemProc_(
    HWND hwndDlg,
    UINT message,
    WPARAM wParam,
    LPARAM lParam)
{ 
    switch (message) 
    { 
        case WM_COMMAND: 
            switch (LOWORD(wParam)) 
            { 
                case IDOK:
                {
                    auto item = GetDlgItem(g_hwnd, MP3_MUSIC_DIALOG_BOX_IDC);
                    auto iCurSel = SendMessage(item, LB_GETCURSEL, 0, 0);
                    SendMessage(item, LB_DELETESTRING, iCurSel, 0);
                }
                case IDCANCEL:
                    EndDialog(hwndDlg, wParam); 
                    return TRUE; 
            }
            break;
    }
    return FALSE; 
}