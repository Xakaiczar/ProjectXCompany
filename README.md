# Project X-Company
Hi! This project is based off the course _Unity Turn-Based Strategy Game: Intermediate C# Coding_ by GameDevTV and CodeMonkey ([Udemy](https://www.udemy.com/course/unity-turn-based-strategy/) / [GameDevTV](https://www.gamedev.tv/p/unity-turn-based-strategy/)). In this course, the instructors cover some intermediate Unity / C# techniques - such as events and delegates - while creating a squad-based strategy game, similar to X-COM or Final Fantasy Tactics. I originally started this course about a year ago, but lost the motivation to continue as I found myself constantly refactoring and rewriting code. This time, I will be working on both their solution _and_ my own solution at the same time.

The code will be written under two separate namespaces: GameDevTV (GDTV) and X-Company (XCOM). The former will encapsulate the original tutorial as a reference for myself if I need it during development, while the latter will include my own implementation of their suggested features. Hopefully, this will make each version of the code much easier to maintain. I don't want a repeat of last time... (trust me, it was a mess!)

The rest of this readme will act as a devlog-type document, explaining the code written and the features added. I will also use that space to explain the differences between my implementation and GDTV's original vision, as well as the logic behind my choices. So let's hope my decisions are, in fact, logical!

So, without further ado, it's time to crack on!

## General Design Principles
This section will detail some of my general design philosophies. Right or wrong, I think it's easier to understand someone's decisions if you understand the logic they use to make those decisions. Plus, if I'm wrong, it'll be easier to explain why!

These principles will likely change over time as I grow as a programmer and as a person. But these are some of the design choices I have made while working on this project.

### Namespaces
GDTV vs XCOM; any extensions

### Single Responsibility Principle
- separate files; cleaner code and separation of responsibilities (SOLID)
  - property files vs. behaviour files
  - personally don't like to use update in property files

### Caching
component caching; naming convention, purpose

### Singletons
No thank you.

## Unit Movement
_Commit(s): [7321ab5](https://github.com/Xakaiczar/ProjectXCompany/commit/7321ab59e0ef8ba09d6a0756004270aa7fc3876a) and [07b2a6a](https://github.com/Xakaiczar/ProjectXCompany/commit/07b2a6a92097ee6917c757aef7d798712506c118)_

> [!NOTE]
> This will likely be one of the longest sections, as I'll have to explain some of my personal design choices. In the future, I will move longer explanations to the section above.

### Code Explanation

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
We know this one! I recently rewrote this exact function while refactoring an old project I was working on. Luckily, it was still fresh in my brain!

The purpose of this code is to translate the on-screen position of the cursor into in-game data. In this case, the game registers and returns any objects that the mouse hovers over.

`Input.MousePosition` seems like a good place to start, but it returns the pixel coordinates of the cursor on the screen, not its corresponding location in-world.

Luckily, `Camera.ScreenPointToRay` takes those coordinates as a parameter and creates a ray, which is essentially just an infinite, invisible line. The ray originates from the camera's near frustrum and passes through the `MousePosition` coordinates. This ray can be used to find in-game objects along its line, so long as they have a collider.

Finally, `Physics.Raycast` / `Physics.RaycastAll` fires off a ray into the world, like an angry Tarkin punishing the heroic Leia for her witty deceptions (albeit less dramatic!). It travels infinitely through world space, possibly colliding with... well... _colliders_ on the way. One output of this function is a `RaycastHit` object, which returns a collider the ray hits (or all of them, in the case of `RaycastAll`).

Buuuut as it turns out, I didn't need to go that far. I may have overdone it a bit...

### Differences in Implementation
Right out the gate, I was never going to call my class "`MouseWorld`". As nice as that sounds for a rodent theme park, I had already used a much more apt name: `Player`. In the future, I might break the controls from `Player` as their own separate class, especially for cross-platform capability; not everyone will use a mouse. But for now, a player is defined by their ability to interact with the game world, which - at the moment - is only done with a mouse, apparently.

The only other notable difference was in factoring out the ray instantiation as its own function (a.k.a. "`GetRayFromMousePosition`"). I only did this to make the code more readable, but it may also be useful in the future!

