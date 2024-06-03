## Unit Selection
_Commit(s): [812717e](https://github.com/Xakaiczar/ProjectXCompany/commit/812717e0565bc7a35807eaf15202d4e233be0a23), [252f970](https://github.com/Xakaiczar/ProjectXCompany/commit/252f9702ee6035c27c5c94e6716691691496da87) and [9b4c070](https://github.com/Xakaiczar/ProjectXCompany/commit/9b4c070c4315cd928b05a13cec0d86c5a7bf82f8)_ 

### Code Explanation
The purpose of this code is to handle the control and movement of multiple units.

First of all, I realised after adding a second unit to the scene that I had to remove the `moveLocation` from the inspector, and set it to the `Transform.position` of the `Unit`. Otherwise, every unit walked straight to the origin at the start of the game!

Now I needed a way of finding a unit using the mouse. I went against the lecturer's original design (or so I thought!) and created a unit layer. This didn't matter so much to me, since the box collider I had made previously was at ground level anyway.

I then extracted the `Raycast` logic into `GetHitOnLayer` so it no longer just checked for objects on the floor layer; instead, it now took a layer as a parameter. I did keep `GetHitLocation` however, as a general catch-all for finding where on the ground was clicked.

I changed up the `MoveUnit` function - now named `HandleClickEvent` - which now controls both the functions for clicking on a unit and clicking on the floor. When a unit is clicked, it sets the `selectedUnit` to be a reference to the `Unit` clicked. When the floor is clicked while there's a `selectedUnit` (or rather, at any time, since I apparently didn't validate that a `selectedUnit` existed until [9b4c070](https://github.com/Xakaiczar/ProjectXCompany/commit/9b4c070c4315cd928b05a13cec0d86c5a7bf82f8)), that `Unit` will be moved.

Sure enough, it all worked as intended. That's that done!

Well, that's all the _relevant_ code done, anyway. Ideally I should've done the rest in a separate commit, but that's the issue with trying to follow a lecture series too rigidly.

I went ahead and made a `Model` class to handle all the animations and stuff; basically, anything aesthetic and not related to the actual logic of the game.

Because my hitbox was attached to the base of the unit, I made the model spin instead, meaning the perfectly square hitbox would always align with the grid space the unit was supposed to be occupying.

During this refactorisation, I noticed that - once they got to their destination - the units kept snapping to face straight along the z-axis. I also realised I kept getting a warning in the inspector.

> Look rotation viewing vector is zero

It seemed like the unit didn't really have a `direction` once they reached their destination, presumably because the distance between their current position and the destination was 0. To solve this, I simply returned if the `direction` was 0.

### Differences in Implementation
First of all, from my experience in strategy games, left-click often selects a unit, while right-click often moves them (or performs some other action). I copied this design in my code: left click will be used to select objects, whereas right click will be used to perform their actions. It keeps the function of each button clear and separate.

Another difference between the two is the location of the units' hitboxes. I aligned mine with the ground, but the lecturer made theirs cover the unit itself. I'm not sure which is better, honestly, but it felt like the design was a bit confused; are we selecting the tiles or the units?

Finally, the lecturer created a `UnitActionSystem` to handle the `Unit`s. I actually like this approach, perhaps as a component to `Player`, so I can separate out the control logic from the unit management logic. I know there will be unit actions later in the course, so this may become relevant, and I'm more than likely to refactor my code to adopt this class if and when I need to.
