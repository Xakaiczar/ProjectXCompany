# Project X-Company
Hi! This project is based off the course _Unity Turn-Based Strategy Game: Intermediate C# Coding_ by GameDevTV and CodeMonkey ([Udemy](https://www.udemy.com/course/unity-turn-based-strategy/) / [GameDevTV](https://www.gamedev.tv/p/unity-turn-based-strategy/)). In this course, the instructors cover some intermediate Unity / C# techniques - such as events and delegates - while creating a squad-based strategy game, similar to X-COM or Final Fantasy Tactics.

I originally started this course about a year ago, but lost the motivation to continue as I found myself constantly refactoring and rewriting code. This time, I will be working on both their solution _and_ my own solution at the same time.

I will follow a fairly rigid workflow to ensure I'm making the most of both solutions:
1. At the start of the lecture, the instructor will open with the lecture aims and objectives. I will first attempt to complete those objectives using only that opening brief, as if I have been given a task by my lead to be completed independently.
2. After that, I will follow along with their implementation, both as a learning exercise and for later reference if I need it.
3. Finally, if on reflection I feel like I can improve my solution using the lecturer's solution, I will do so.

Following this pattern, each lecture will be split into 2 or 3 separate commits, all of which will be listed in a respective log file (see below).

The code will be written under two separate namespaces: GameDevTV (GDTV) and X-Company (XCOM). The former will encapsulate the original tutorial, while the latter will include my own implementation of their suggested features. Hopefully, this will make each version of the code much easier to maintain. I don't want a repeat of last time... (trust me, it was a mess!)

## Logs
Below are links to my logs, explaining the code written and the features added. I will also use that space to explain the differences between my implementation and GDTV's original vision, as well as the logic behind my choices. So let's hope my decisions are, in fact, logical!

All logs can be found in [this](https://github.com/Xakaiczar/ProjectXCompany/tree/main/Devlog) folder, or you can navigate via the list of links below!

<details>
<summary><h3>All Logs</h3></summary>
<ul>
  <li><a href="https://github.com/Xakaiczar/ProjectXCompany/blob/main/Devlog/Log%20File%201%20-%20General%20Design%20Principles.md">Log File 1 - General Design Principles</a></li>
  <li><a href="https://github.com/Xakaiczar/ProjectXCompany/blob/main/Devlog/Log%20File%202%20-%20Unit%20Movement.md">Log File 2 - Unit Movement</a></li>
  <li><a href="https://github.com/Xakaiczar/ProjectXCompany/blob/main/Devlog/Log%20File%203%20-%20Mouse%20Raycast.md">Log File 3 - Mouse Raycast</a></li>
</ul>
</details>

## Mouse World Position
_Commit(s): [3141a89](https://github.com/Xakaiczar/ProjectXCompany/commit/3141a895cc16870627c81f6de210fd4bf97a2f74) and [556e6fa](https://github.com/Xakaiczar/ProjectXCompany/commit/556e6fa4bf670346dfc4e3dee803d8dc9afc8dea)_

### Code Explanation
The purpose of this code is to use the position of the cursor obtained in the last lecture to find the point on the floor that the mouse is hovering over. Needless to say, I definitely overdid it last time...

I made a bit of a mistake last time in assuming that we wanted to get all (or - to be honest - _any_) objects hit by the ray. As seen by the intro to the lecture, all I really needed to know was where the mouse was hovering in world space... oops...

I created `GetHit`, a method that uses `Physics.Raycast` to fetch only _one_ collider instead of _every_ collider (though I did keep the original, just in case!). The regular `Raycast` object has an output parameter, a `RaycastHit` I aptly named `hit`. Using that `RaycastHit`, I stored the fired ray's `RaycastHit.point` in a local variable called `mousePosition`. This means that `mousePosition` is set to wherever the ray collided with the returned object. Perfect! I created a sphere cursor as shown in the intro to the video; the `transform.position` of the sphere was set to that `mousePosition`, so it would always be beneath the mouse. I compiled the scripts, hit play aaaand...

The sphere kept flying straight towards the camera!

I thought I was being clever when I realised I could cut the `y` value of `RaycastHit.point`. It worked like a charm! However, upon closer inspection, I realised I hadn't actually turned off the sphere's collider. This meant that the ray was colliding with the sphere's edge (i.e. the edge of the collider), then `GetHit` was setting the centre of the sphere to that location, which then meant the sphere - and therefore the collider - were closer and... you get the idea...

To add insult to injury, after watching the lecturer go through their solution, I realised I'd made yet _another_ mistake...

In my defence, it wasn't completely my fault, but it required common sense which I seemed to lack in that moment!

I start each of these with a "purpose" line, to set the context for you as a reader to understand my objectives. However, while they're based on the brief in the first 5 or so seconds of the lecture, these are often written retroactively. Unfortunately for past me, I didn't realise we _only_ wanted to interact with the floor, as it wasn't mentioned in the brief. I had assumed that - because the project overview showed clips of units and objects being selected - I would need to know _anything_ the mouse was hovering over. However, common sense should dictate that - for a grid-based game - you probably want to select grid tiles more than the things on them. Then, you can use that grid tile to access whatever is on it, whether that's a unit or another object.

Hindsight is 20/20, but luckily, it was an easy fix.

I simply created a new layer mask for the floor, then used the `layerMask` parameter of `Physics.Raycast` to filter out any objects not on the `floor` layer. Now everything was working as intended.

### Differences in Implementation
Originally, I mistakenly cut the `y` value of `RaycastHit.point`, both forgetting about the sphere's collider and not fully understanding the design brief. However, part of me still thinks this may be a more effective solution down the line. After all, if we're only selecting the floor, then why would we need a higher `y` value than 0?

In my original version, this meant that the ray would collide with a wall, but not travel up it, which I assume must be acceptable behaviour if we're only selecting the floor. A perhaps unintentional side effect of the current implementation (by design) is that the cursor - in its search for floors - now _ignores_ walls, meaning you can actually select _beyond_ a wall to a square you can't see. I'm not sure if this is intended behaviour, or even if it's a good thing either way.

I have stuck to the design for now, but this may change in the future, as I believe some aspects of my original design worked better. I think the answer is somewhere between my design and their intended design. I will see how it works out in practice before I make any adjustments however.

There's also one big glaring difference between the two: singletons... 

And so it begins...

I have hopefully made my stance on singletons very clear in my [first log](https://github.com/Xakaiczar/ProjectXCompany/blob/main/Log%20File%201%20-%20General%20Design%20Principles.md#singletons). If memory serves me correctly, they crop up a _lot_ in this course, and was one of the biggest frustrations about doing it in the first place.

This is definitely one of those cases where we _do not_ want a weird dependency on the mouse. Best-case scenario, even if you want this as a singleton, using its global scope to directly reference it in `Unit` restricts the player's controller to mouse only. If you wanted to implement controller support, for example (or even just _keyboard_ support), you'd have to do a lot of rewriting. It also makes the `Unit` code harder to reuse and harder to debug. It just seems like a bad idea all round, really.

Ideally, you just want to pass the location to the `Unit`. How the program actually gets that location should be completely irrelevant to the `Unit`.

## Unit Click to Move
_Commit(s): [9a8f4b3](https://github.com/Xakaiczar/ProjectXCompany/commit/9a8f4b3d2e793d1c94d29ef1f889cb494da4ae65) and [f70a80d](https://github.com/Xakaiczar/ProjectXCompany/commit/f70a80d2e67b23d46fd6c09c9fcc87e0d5d74e50)_

### Code Explanation
The purpose of this code is to move the unit to a clicked point in the game world.

This was pretty straightforward. As stated in the last log, I will not be passing around any mouse-based logic. Instead, I created a function in `Unit` called `SetMoveLocation` that takes a `Vector3` and stores it in `Unit` for the `Mover` to access. I'm not sure if I'll keep this member variable (or, at least, not in `Unit`), but it's there for now, so I'll use it.

In my `Player` class, I temporarily created a serialised `Unit` to test the function, selecting an existing unit in the world as the target.

I then renamed `GetHit` to `GetHitLocation` and refactored it to return the `Vector3` needed for the `SetMoveLocation` mentioned above. I then passed this value into `SetMoveLocation` on mouse click and voilà! The unit was off!

### Differences in Implementation
Again, the use of singletons in the lecture does call for a bit of a paradigm shift in my code, but at this point it's manageable. I just did what I said I would: passed in a `Vector3` so the `Unit` doesn't care about what controller the `Player` is using. Each class only has the information it actually _needs_ to function.

## Unit Rotate when Moving
_Commit(s): [316f9f1](https://github.com/Xakaiczar/ProjectXCompany/commit/316f9f109357e47fe79c66c2d689019a5b06457d) and [d8e4fec](https://github.com/Xakaiczar/ProjectXCompany/commit/d8e4fec1f570b26e92f96e915914f5140311118f)_

### Code Explanation
The purpose of this code is to rotate the unit so it faces the direction it's moving.

After animating the unit to walk, it now needed to face the right way...

I may have done this in a bit of a convoluted way compared with the lecture, but I set the rotation with `Quaternion.LookRotation`. The first parameter is the direction to look in, the second is the relative "upwards" direction in the world, which I just set to `Vector3.up` (which is the same as `new Vector3(0f, 1f, 0f)`, a path that follows the y axis).

I gave it a test and noticed it snapped too quickly. To fix this, I used `Vector3.Lerp` to gradually shift between the two values (the original direction and the move direction) over time.

### Differences in Implementation
As it turns out, I only needed one line of code...

As much as I think their solution is much more succinct, I felt like the rotation at 180 degrees was a bit less janky using `Quaternion.LookRotation`. When setting the `transform.forward` directly, it seems to swing around much slower as it moves, despite actually having a _quicker_ rotation speed. So I kept mine as-is, for better or for worse!

I may also implement a separate `rotateSpeed` like they did; this will give the designer more control over the rotation process. But for now, I'm happy with my implementation.

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

## Unit Selected Visual
_Commit(s): [9d1e4c7](https://github.com/Xakaiczar/ProjectXCompany/commit/9d1e4c71d84741ae7f1207889157121e9510a1d7), [2c160c4](https://github.com/Xakaiczar/ProjectXCompany/commit/2c160c4e0ea184d2c736f04a25e843e089504ee6) and [c06e83a](https://github.com/Xakaiczar/ProjectXCompany/commit/c06e83a0493056e6db0b20576dfe690709b55878)_ 

### Code Explanation
The purpose of this code is to visually highlight the selected unit.

Straight away, I have no idea how the lecturer is going to achieve this. The task is somehow split into two lectures: one about events, the other about singletons.

I tried to make it work with events but didn't like the results I produced, so I tried an eventless version first.

First, I made `UISelectedUnit` for the visual selection. All it contains is a reference to its `MeshRenderer` and a function - `ToggleDisplay` - that shows and hides the object's mesh. This function is then passed forward into `Unit` to be accessed by the `Player`, though in future I may separate it out a bit better so the `Unit` is not dependent on UI features.

In `Player`, I created some units at runtime using the prefab, like they would be instantiated in a real game. I stored these in a `List<Unit>`. When a unit is selected, `Player` iterates through the list of `Unit`s and compares each one to the `selectedUnit`; if it is the `selectedUnit`, then the visual will appear, else it will be hidden.

This works fine. I might do some refactoring down the line to make it work with events, but I don't really see any reason to overcomplicate this.

### Differences in Implementation
Of course, I was never going to make a singleton like `UnitActionSystem`. An explanation can be found in my [first log](https://github.com/Xakaiczar/ProjectXCompany/blob/main/Log%20File%201%20-%20General%20Design%20Principles.md#singletons). As usual, I just pass down the necessary data. In this case, a simple bool that tells the `UISelectedUnit` whether the unit is selected or not.

The idea of using events intrigued me. But at the moment, it seems like there isn't a massive difference between using events and a loop in a function call. Since I need to go through every single object anyway to not only enable the selected one, but disable the others, it felt like I didn't really get any value from an event call. I did eventually create an event in [c06e83a](https://github.com/Xakaiczar/ProjectXCompany/commit/c06e83a0493056e6db0b20576dfe690709b55878), but it currently doesn't do anything.

## Grid System
_Commit(s): [14fcd36](https://github.com/Xakaiczar/ProjectXCompany/commit/14fcd36918dfd6488596d0a776f7dbbdeb2ea96e), [723cd23](https://github.com/Xakaiczar/ProjectXCompany/commit/723cd23fc1a9a2769bd560f2b45f57ec985bd7bf) and [8ae208a](https://github.com/Xakaiczar/ProjectXCompany/commit/8ae208a927ff431d82b37531b028d84f92a7bcf2)_ 

### Code Explanation
The purpose of this code is to separate the world into distinct cells for grid-based movement.

This one starts off with a nice brief by the lecturer explaining how the grid system needs to be designed. That makes things easier!

Given this will be a grid-based game, the world will be separated into cells called `GridPosition`s. These cells will form the grid, which will have a set width along the x axis and a set length across the z axis. The cells will also have a size in Unity units, which we need to be able to convert between in order to get to / from the `GridPosition`. For instance, if each cell has a size of 2, the cell at `(2, 2)` in world space would be the `GridPosition` at `(1, 1)`. Each `GridPosition` will also be a `struct` instead of a `Vector2`, as we need `x` and `z` values, not `x` and `y`.

Seems pretty straightforward!

I started by creating the `struct` for grid coordinates, called `GridPosition`.

I created a `GridSystem` class. Inside, I created private properties `gridWidth` and `gridLength`, then made them available to the inspector. While it ended up being unnecessary, I defined a 2D array of `GridPosition`s called `Grid` using `gridWidth` and `gridLength` to set the size of the array.

Then, in `Awake`, I iterated along the `x` and `z` axes, creating a new `GridPosition` for each value pair and storing the result in `Grid` at the appropriate index.

To visualise the grid, I generated a collection of spheres at runtime. I serialised `cellSize`, an integer which determines the size of the grid square in Unity units. Finally, I set the position of each sphere to `x * cellSize` and `z * cellSize`.

The spheres generated as expected, taking up the centre of each soon-to-be grid square. Printing out the values of all the `GridPosition`s in `grid` also returned the expected results. Nice!

After running through their version, I made some corrections to my own:
- I added the `Vector3` to / from `GridPosition` functions, including an overload for `GetWorldPosition` that takes a `GridPosition` instead of an `x` and `z`
- I made the `struct` public and rewrote it a bit so it fell in line with the lecturer's design

### Differences in Implementation
In the lecturer's version, the `GridSystem` isn't actually a `MonoBehaviour`. I'm honestly not sure on the value of that decision, if any, but given it still relies on the `MonoBehaviour` simply known as `Testing` right now, I think I'll keep mine as is until I see a reason to change. However, I will take this opportunity to look up when something _shouldn't_ be a `MonoBehaviour`.

## Grid Object
_Commit(s): [3fa6dd0](https://github.com/Xakaiczar/ProjectXCompany/commit/3fa6dd0a93aa64df1effab6c9c035a05fd38b7b9), [ce1f161](https://github.com/Xakaiczar/ProjectXCompany/commit/ce1f161a865304e69e8b738f773bd42264c19099) and [e6a13ff](https://github.com/Xakaiczar/ProjectXCompany/commit/e6a13fff931b7f4085013c3e8528503d548a20e3)_ 

### Code Explanation
The purpose of this code is to turn the `GridPosition`s into slightly more detailed `GridObject`s.

I started by creating my `GridObject`, as well as a `UIGridObject` (which is actually a step ahead into the Grid Object Debug section... oops!). The former was attached to a `GameObject`, the latter attached to a child `GameObject` with `TextMeshPro` text to display the coordinates of the tile.

I added a `GridPosition` property to the `GridObject` and created a setter. When `SetPosition` is called, both the name of the `GameObject` and the text on the `UIGridObject` are also set to the `GridPosition` coordinates.

Once that was done and dusted, I prefabbed the `GameObject`, replacing the original spheres in `GridSystem` with the new swanky `GridObject` prefab.

Finally, I converted `grid` from a 2D array of `GridPosition`s to `GridObject`s; the old `GridPosition`s are now stored within their respective `GridObject`s.

Now, when the game is run, each grid space is marked with its coordinates!

### Differences in Implementation
The `GridObject` in the lecturer's version was - once again - _not_ a `MonoBehaviour`. It wouldn't really make sense to make the `GridObject` a `MonoBehaviour` but leave the `GridSystem` as is, but I could've gone either way. Like last log, I didn't see any benefit to taking away `MonoBehaviour`, so I left it.

I've also noticed there's a circular dependency between `GridSystem` and `GridObject`; they each have a reference to each other, which is a pain. I tend to have objects at the top of the hierarchy hold references to those below, and not the other way around.

In my version, I also used `GetComponent` to find the `TextMeshPro` component, rather than passing it in the inspector. This seems less prone to errors should the reference be lost somehow. But maybe I just overengineer things, who knows!

One thing I did like though was the alteration of the folder structure. In the next log, I do the same with my own namespace, and I even take that a step further...

## Minor Updates (Namespaces and Unit Selection)
_Commit(s): [c234571](https://github.com/Xakaiczar/ProjectXCompany/commit/c2345715e4ac29ab8b2cd8021b028b1e0b07abad) and [9b4c070](https://github.com/Xakaiczar/ProjectXCompany/commit/9b4c070c4315cd928b05a13cec0d86c5a7bf82f8)_ 

### Code Explanation
The purpose of this code is whatever I want it to be! Haha!

But seriously, I took some time to make a few little updates to the code.

Firstly, inspired by the folder restructuring in the previous lecture, I decided to move my grid-based objects into a folder together. At the same time however, I also changed their namespace from `XCOM` to `XCOM.Grid`. Typically, separating large namespaces out like this is a good idea. It means I have to be more mindful of the dependencies I have in my code, removing any that don't seem appropriate.

Case in point, I did the same with my UI folder, converting their namespaces to `XCOM.UI`. When I did this, I realised just how badly designed some of my classes were! A weird mix of logic and UI, these classes need to be changed somewhere down the line. However, I think I'd need to spend a bit of time considering how I'd break these dependencies before I start hacking away. What I'm most likely to do is create an overarching `Game` class, which manages gameplay elements like `GridObject` and `Unit`, as well as a `UIManager` (or similar control class). I'm thinking of hooking them up with events, which would _finally_ make my `OnUnitSelected` event actually useful.

But, as you can probably tell, that's all a bit convoluted right now, and that's not even considering how or if I change the `Unit` and `GridObject` prefabs. Needless to say, I think I'll see how the rest of the project is designed before I make such a drastic change.

Besides that endeavour, I created a function to get a `GridObject` from a `GridPosition` in `GridSystem` and made some minor changes to the Unit Selection code I noticed while writing the log. Specifically, I moved the `rotationSpeed` (which was actually just a constant `5f` in code... yikes...) up into a class variable and made it accessible in the inspector. I also made sure to do a null check on the `selectedUnit` in `Player`, just on the off chance there isn't a selected unit.

That rounds off my own adventures into cleaning up my code! Next time I should be good and ready to finish off this grid system!
