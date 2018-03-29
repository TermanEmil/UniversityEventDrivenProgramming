#ifndef _MUSIC_HPP_
# define _MUSIC_HPP_

# include <string>
# include <windows.h>

class Music
{
public:
    std::string path;

    Music(std::string file_path);
    
    bool operator == (const Music& r);

private:
};

#endif