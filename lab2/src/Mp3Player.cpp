#include "Mp3Player.hpp"
#include "lab1.hpp"

Mp3Player::Mp3Player(HWND parent_hwnd)
{
    music_dialog_box_ = CreateWindowEx(
        WS_EX_CLIENTEDGE,
        TEXT("listbox"),
        "",
        WS_CHILD | WS_VISIBLE | WS_VSCROLL | ES_AUTOVSCROLL,
        0, 0, 0, 0,
        parent_hwnd,
        (HMENU)idc_,
        NULL,
        NULL);
    SendMessage(music_dialog_box_, LB_RESETCONTENT, 0, 0);

    musics = std::list<Music>();
}

void Mp3Player::AddMusic(Music new_music)
{
    musics.push_back(new_music);
    SendMessage(
        music_dialog_box_,
        LB_ADDSTRING,
        0,
        (LPARAM)new_music.path.c_str());
}

void Mp3Player::OnWmSize(int cx, int cy)
{
    auto width = dialog_init_width_ / MIN_WIN_WIDTH * cx;
    auto height = cy - top_padding_ - bot_padding_;

    MoveWindow(
        music_dialog_box_,
        cx / 2 - width / 2,
        top_padding_,
        width,
        height,
        TRUE
    );
}