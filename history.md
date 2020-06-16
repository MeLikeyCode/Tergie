Clearing the screen and then drawing results in flickering, it is best to just `Console.SetCursorPosition()` and then write characters over previous characters.

`Console.SetWindowSize()` and `SetBufferSize()` are not implemented in linux, so I won't use this functionality (i.e. game engine does not have the capability to resize the window, though the user still can, and if he does, we handle it)

We can use `Process.Start("tput","civis")` to disable the blinking cursor.

After using C# (mainly the `Console` class) to play around with manipulating a terminal, I think I can create a game engine from this. My plan is:
- each frame
  - pass events to objects in the scene (in response to which they may move around)
  - call Update() for all objects (in response to which they may move around)
  - draw the scene in its current state

My `Window` class represents the region of the `Scene` that is currently being drawn. By moving the Window around, you are drawing different parts of the Scene. Note, that we won't have control over the size of the window, the user will (as noted somewhere above).

Can use `Console.KeyAvailable` to see if a key was just pressed, then Console.ReadKey(true) to get the actual key (and modifiers) that was pressed.
~~~~~~~~~~cs
if (Console.KeyAvailable){
    var keyInfo = Console.ReadKey(true);
    var key = keyInfo.Key; // keyInfo.Modifier has modifier info
    if (key == ConsoleKey.W)
        // move up
}
~~~~~~~~~~

TODO My engine is always using CPU, it never sits idle, is this a problem and can I resolve it?