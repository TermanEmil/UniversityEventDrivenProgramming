# Laboratory #5
In this laboratory work Miculets Victor and me, we worked on a .NET Windows Forms mini game. This game is a 2D top-down shooter.

We divided the tasks in the following way:
* I worked on the main mechanics of the game.
* Victor has implemented the enemy tanks' AI and its controls. He has also resolved the Flickering problem using manual Double Buffering.

The player is controlled from keyboard and it can shoot by pressing space. It can also regenerate HP. All tanks have a shoot cool down so that they can't continuously fire.

The architecture of the project is inspired from Unity, so that it can be easily expanded for further development. We initially planned to make the game played in multiplayer on LAN. But we didn't have enough time.

Each tank object has its own position, rotation, scale, mesh, collider, controller and life. If a tank explodes, new one is randomly spawned.

There are some animations played when a tank explodes.

To solve the flickering problem, we have used the double buffering method, which involves having everything drawn on the bitmap, then the this bitmap is drawn when necessary.
