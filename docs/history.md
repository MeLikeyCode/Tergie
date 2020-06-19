Video 1
=======
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

Don't have a way to do diagonal movement. I.e. don't have a way to detect if two keys are pressed simultaneously. Gotta figure this out. I can't seem to figure this out. I think the operating system only gives me one of the two keyboard events when I'm pressing two keys...not much I can do about this. Time to move on.

Added KeyPressEvent and KeyReleaseEvent to Game.

Let's create an Entity class (can go in Scene).
Entity
- pos
- width
- height
- ASCII graphics (2d char array)
- can be loaded from txt file(reuse function in Scene)

Video 2
=======
TODO My engine is always using CPU, it never sits idle, is this a problem and can I resolve it?

Handle "transparent" characters
- represented by the character '\0' (null character)
- TransparentChar in Utility
- Util.Blit() can now optionally copy transparent pixels
- Util.FileToCharArray() can now optionally replace space characters with the null char

We now only run on windows, because windows allows me to write a bunch of characters on the console screen buffer, and then draw them all at once. Linux doesn't expose this for me, so it's a lot slower. Our Window.Draw() method has changed a decent amount, it now draws on back buffer, then swaps buffers.

Window cannot be resized (for now), because when it is resized, we have to allocate new screen buffers, and I don't wanna handle that rignt now. So for now, the size of the window is specified in the ctor, and then it never changes.

TODO introduce concept of layers (in a scene) to solve z problem easily