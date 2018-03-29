#ifndef _MP3PLAYER_HPP_
# define _MP3PLAYER_HPP_

# include <windows.h>
# include <algorithm>
# include <iostream>
# include <list>

# include "Music.hpp"

class Mp3Player
{
public:
    std::list<Music*> musics;

    Mp3Player(HWND parent_hwnd);
    ~Mp3Player();

    void OnWmSize(int cx, int cy);
    bool OnWmCommand(WORD cmd_word, WPARAM wParam, LPARAM lParam);
    bool OnWmKeyDown(WPARAM wParam);
    void OnWmHScroll(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);

    void AddMusic(const std::string path);
    void RemoveMusic(Music target);
    void RestartCurrent();
    void PlaySelected();
    void Stop();
    void PlayNext();
    void SetVolume(float volume);

private:
    float dialog_init_width_ = 200;
    float btn_init_width_ = 50;
    float btn_init_height_ = 20;

    float volume_bar_width_ = 250;

    float top_padding_ = 20;
    float bot_padding_ = 10;
    float left_padding_ = 10;
    float right_padding_ = 10;

    float volume_scb_offset_ = 100;
    float left_chanel_volume_ = 100;
    float right_chanel_volume_ = 100;

    HWND parent_hwnd_;
    HWND music_dialog_box_;
    HWND add_music_btn_;
    HWND play_btn_;
    HWND pause_btn_;
    HWND next_btn_;

    HWND volume_bar_;
    HWND left_chanel_volume_bar_;
    HWND right_chanel_volume_bar_;

    Music* currently_playing_ = NULL;

    std::string ChooseFile_();
    int GetSelectedText_(std::string* result);
    void Play_(std::string path);

    static BOOL CALLBACK DeleteItemProc_(
        HWND hwndDlg,
        UINT message,
        WPARAM wParam,
        LPARAM lParam);
};

#endif