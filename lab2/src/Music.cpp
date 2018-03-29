#include "Music.hpp"

Music::Music(std::string file_path)
{
    path = file_path;
}

bool Music::operator == (const Music& r)
{
    return path.compare(r.path) == 0;
}