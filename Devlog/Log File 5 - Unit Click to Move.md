## Unit Click to Move
_Commit(s): [9a8f4b3](https://github.com/Xakaiczar/ProjectXCompany/commit/9a8f4b3d2e793d1c94d29ef1f889cb494da4ae65) and [f70a80d](https://github.com/Xakaiczar/ProjectXCompany/commit/f70a80d2e67b23d46fd6c09c9fcc87e0d5d74e50)_

### Code Explanation
The purpose of this code is to move the unit to a clicked point in the game world.

This was pretty straightforward. As stated in the last log, I will not be passing around any mouse-based logic. Instead, I created a function in `Unit` called `SetMoveLocation` that takes a `Vector3` and stores it in `Unit` for the `Mover` to access. I'm not sure if I'll keep this member variable (or, at least, not in `Unit`), but it's there for now, so I'll use it.

In my `Player` class, I temporarily created a serialised `Unit` to test the function, selecting an existing unit in the world as the target.

I then renamed `GetHit` to `GetHitLocation` and refactored it to return the `Vector3` needed for the `SetMoveLocation` mentioned above. I then passed this value into `SetMoveLocation` on mouse click and voilà! The unit was off!

### Differences in Implementation
Again, the use of singletons in the lecture does call for a bit of a paradigm shift in my code, but at this point it's manageable. I just did what I said I would: passed in a `Vector3` so the `Unit` doesn't care about what controller the `Player` is using. Each class only has the information it actually _needs_ to function.
