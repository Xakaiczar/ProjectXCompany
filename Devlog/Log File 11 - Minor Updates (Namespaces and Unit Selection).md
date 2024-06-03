## Minor Updates (Namespaces and Unit Selection)
_Commit(s): [c234571](https://github.com/Xakaiczar/ProjectXCompany/commit/c2345715e4ac29ab8b2cd8021b028b1e0b07abad) and [9b4c070](https://github.com/Xakaiczar/ProjectXCompany/commit/9b4c070c4315cd928b05a13cec0d86c5a7bf82f8)_ 

### Code Explanation
The purpose of this code is whatever I want it to be! Haha!

But seriously, I took some time to make a few little updates to the code.

Firstly, inspired by the folder restructuring in the previous lecture, I decided to move my grid-based objects into a folder together. At the same time however, I also changed their namespace from `XCOM` to `XCOM.Grid`. Typically, separating large namespaces out like this is a good idea. It means I have to be more mindful of the dependencies I have in my code, removing any that don't seem appropriate.

I did the same with my UI folder, converting their namespaces to `XCOM.UI`. When I did this, I realised just how badly designed some of my classes were! A weird mix of logic and UI, these classes need to be changed somewhere down the line. However, I think I'd need to spend a bit of time considering how I'd break these dependencies before I start hacking away. What I'm most likely to do is create an overarching `Game` class, which manages gameplay elements like `GridObject` and `Unit`, as well as a `UIManager` (or similar control class). I'm thinking of hooking them up with events, which would _finally_ make my `OnUnitSelected` event actually useful.

But, as you can probably tell, that's all a bit convoluted right now, and that's not even considering how or if I change the `Unit` and `GridObject` prefabs. Needless to say, I think I'll see how the rest of the project is designed before I make such a drastic change.

Besides that endeavour, I created a function to get a `GridObject` from a `GridPosition` in `GridSystem` and made some minor changes to the Unit Selection code I noticed while writing the log. Specifically, I moved the `rotationSpeed` (which was actually just a constant `5f` in code... yikes...) up into a class variable and made it accessible in the inspector. I also made sure to do a `null` check on the `selectedUnit` in `Player`, just on the off chance there isn't a selected unit.

That rounds off my own adventures into cleaning up my code! Next time I should be good and ready to finish off this grid system!
