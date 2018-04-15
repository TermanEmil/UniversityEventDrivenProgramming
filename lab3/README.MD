# **Lab #3**

This application was successfully compiled with Visual Studio C#.

In this laboratory work we had to implement a mini Paint application, which must support lines, ellipses, rectangles, bezier and pie drawings.

## Objectives
* All the controls are located in the system menu
* Supported drawings:
  * Line
  * Ellipse
  * Rectangle
  * Pie (after drawing the main ellipse, another click is necessary to adjust the parameters [while mouse down, move x or y])
  * Bezier (after drawing the main line, a mini menu appears)
* All actions are saved in a ViewBox, from which can be removed.
* To erase, you can simply draw huge lines.
* I tried to implement Fill tool and Zoom, but I failed.

Every draw action is saved as an *Action* which is invoked on every Paint event of the main picture box.
