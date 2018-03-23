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
    std::list<Music> musics;

    Mp3Player(HWND parent_hwnd);

    void OnWmSize(int cx, int cy);

    void AddMusic(Music new_music);
    void RemoveMusic(Music target);

private:
    const int idc_ = 105;

    float dialog_init_width_ = 200;
    float top_padding_ = 20;
    float bot_padding_ = 0;

    HWND music_dialog_box_;
    HWND add_music_btn_;
    HWND play_btn_;
    HWND pause_btn_;
    Music currently_playing_;
};

#endif