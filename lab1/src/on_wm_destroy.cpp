#include "lab1.hpp"

void    OnWmDestroy()
{
    DeleteObject(hfont);
    PostQuitMessage(0);
}
