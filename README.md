# Project X-Company
Hi! This project is based off the course _Unity Turn-Based Strategy Game: Intermediate C# Coding_ by GameDevTV and CodeMonkey ([Udemy](https://www.udemy.com/course/unity-turn-based-strategy/) / [GameDevTV](https://www.gamedev.tv/p/unity-turn-based-strategy/)). In this course, the instructors cover some intermediate Unity / C# techniques - such as events and delegates - while creating a squad-based strategy game, similar to X-COM or Final Fantasy Tactics. I originally started this course about a year ago, but lost the motivation to continue as I found myself constantly refactoring and rewriting code. This time, I will be working on both their solution _and_ my own solution at the same time.

The code will be written under two separate namespaces: GameDevTV (GDTV) and X-Company (XCOM). The former will encapsulate the original tutorial as a reference for myself if I need it during development, while the latter will include my own implementation of their suggested features. Hopefully, this will make each version of the code much easier to maintain. I don't want a repeat of last time... (trust me, it was a mess!)

Below are links to my logs, explaining the code written and the features added. I will also use that space to explain the differences between my implementation and GDTV's original vision, as well as the logic behind my choices. So let's hope my decisions are, in fact, logical!

## Logs
[Log File 1 - General Design Principles](https://github.com/Xakaiczar/ProjectXCompany/blob/main/Log%20File%201%20-%20General%20Design%20Principles.md)

## Unit Movement
_Commit(s): [7321ab5](https://github.com/Xakaiczar/ProjectXCompany/commit/7321ab59e0ef8ba09d6a0756004270aa7fc3876a) and [07b2a6a](https://github.com/Xakaiczar/ProjectXCompany/commit/07b2a6a92097ee6917c757aef7d798712506c118)_

> [!NOTE]
> This will likely be one of the longest sections, as I'll have to explain some of my personal design choices. In the future, I will move longer explanations to the [General Design Principles](https://github.com/Xakaiczar/ProjectXCompany/blob/main/Log%20File%201%20-%20General%20Design%20Principles.md) doc.

### Code Explanation
The purpose of this code is to allow a `Unit` to move in world space.

All things considered, it's pretty straightforward: a destination is passed to a generic `Move` method from the `Mover` component that will be called every frame from `Unit`. The distance between the `Unit`'s current position and the destination will be measured; if it's greater than the `accuracyTolerance` / `stoppingDistance` then it will move a bit closer to its destination, otherwise it will be snapped to the destination and the `Unit` will stop moving.

All you need to do is input the destination into the `moveLocation` vector on the `Unit`, which is accessible in the Unity Inspector. With that, the `Unit` will make its way across the world towards the chosen destination!

### Differences in Implementation
My implementation was somewhat different to the original design. First of all, as I often do, I made two separate scripts instead of one. This may seem a bit redundant, as the original script only does one thing anyway right now. But that's the important part to me: it might not be doing much _right now_, but in the future, it will likely do a lot more. The GDTV `Unit` class will therefore inevitably break the single-responsibility principle once the unit can do more than just move (although technically, it already handles both movement _and_ user input).

The way I usually go about solving this is to create atomic components for individual properties or actions. In this case, the `Unit`'s ability to move has been extracted into a `Mover` class (I hate the name, but it does what it says on the tin!). So, then, what is the point of the `Unit`? I consider the `Unit` to be a controller class of sorts, which models the behaviour of the unit object. It contains the complex interactions between each component and decides when or how a given component may be used.

In short, the `Mover` class represents the legs; they literally _move_ an object from its start location to a given destination, but cannot do so independently. A `Unit`, however, is like the brain; it makes the decision as to how and when that object can move, as well as anything else that happens during that time (e.g. animations / sfx / vfx / etc).

In the same vein, I don't like using `Update` property components. Once again, a component should only have one responsibility, and that's to perform a single action or modify a specific property. It's very rare that a property would necessarily need to change in real time.

One side effect of the instructor's original design is the inappropriately named "`Move`" function, which doesn't actually _move_ the unit. In my interpretation it feels a bit more apt, though - in hindsight - I should really call it "`MoveTowards`", as it's actually `Update` that causes the movement to be continuous.

Another minor change between the two was the elevation of a couple of variables - `speed` and `accuracyTolerance` / `stoppingDistance` - from `Update` to become serialised member variables. Honestly, I think it's just nice for the designers to have easy access to these properties for fine-tuning gameplay. Not that I have any other designers, but I'm sure future me will be grateful! It also felt a bit redundant recreating and redefining constant values on every frame. Probably wouldn't affect performance too much, but this just felt a bit cleaner. I'm not entirely sure why we're caching the destination though. Seems a bit redundant, especially since we pass the destination every time `Move` is called. I will probably remove it as soon as it seems apt.

Finally, since I know from the design brief that the game is grid-based, I added a line of code that simply snapped the unit to the exact grid position when they got close enough.

As for modifications to my code after watching the instructor go through their solution, I realised that "`accuracyTolerance`" was not a particularly descriptive name for a variable. "What does that even mean?!" I asked myself in hindsight. They had a much better name: `stoppingDistance`. Simple, but effective, as it should be.

## Mouse Raycast
_Commit(s): [264a371](https://github.com/Xakaiczar/ProjectXCompany/commit/264a371531e296ad24cfcc483048dd411ee7a761) and [34b3093](https://github.com/Xakaiczar/ProjectXCompany/commit/34b30939be52756ae52ee620582eab3773f49690)_

### Code Explanation
The purpose of this code is to translate the on-screen position of the cursor into in-game data. In this case, the game registers and returns any objects that the mouse hovers over.

We know this one! I recently rewrote this exact function while refactoring an old project I was working on. Luckily, it was still fresh in my brain!

`Input.MousePosition` seems like a good place to start, but it returns the pixel coordinates of the cursor on the screen, not its corresponding location in-world.

Luckily, `Camera.ScreenPointToRay` takes those coordinates as a parameter and creates a ray, which is essentially just an infinite, invisible line. The ray originates from the camera's near frustrum and passes through the `MousePosition` coordinates. This ray can be used to find in-game objects along its line, so long as they have a collider.

Finally, `Physics.Raycast` / `Physics.RaycastAll` fires off a ray into the world, like an angry Tarkin punishing the heroic Leia for her witty deceptions (albeit less dramatic!). It travels infinitely through world space, possibly colliding with... well... _colliders_ on the way. One output of this function is a `RaycastHit` object, which returns a collider the ray hits (or all of them, in the case of `RaycastAll`).

Buuuut as it turns out, I didn't need to go that far. I may have overdone it a bit...

### Differences in Implementation
Right out the gate, I was never going to call my class "`MouseWorld`". As nice as that sounds for a rodent theme park, I had already used a much more apt name: `Player`. In the future, I might break the controls from `Player` as their own separate class, especially for cross-platform capability; not everyone will use a mouse. But for now, a player is defined by their ability to interact with the game world, which - at the moment - is only done with a mouse, apparently.

The only other notable difference was in factoring out the ray instantiation as its own function (a.k.a. "`GetRayFromMousePosition`"). I only did this to make the code more readable, but it may also be useful in the future!

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


