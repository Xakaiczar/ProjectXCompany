## Unit Movement
_Commit(s): [7321ab5](https://github.com/Xakaiczar/ProjectXCompany/commit/7321ab59e0ef8ba09d6a0756004270aa7fc3876a) and [07b2a6a](https://github.com/Xakaiczar/ProjectXCompany/commit/07b2a6a92097ee6917c757aef7d798712506c118)_

### Code Explanation
The purpose of this code is to allow a `Unit` to move in world space.

All things considered, it's pretty straightforward: a destination is passed to a generic `Move` method from the `Mover` component that will be called every frame from `Unit`. The distance between the `Unit`'s current position and the destination will be measured; if it's greater than the `accuracyTolerance` / `stoppingDistance` then it will move a bit closer to its destination, otherwise it will be snapped to the destination and the `Unit` will stop moving.

All you need to do is input the destination into the `moveLocation` vector on the `Unit`, which is accessible in the Unity Inspector. With that, the `Unit` will make its way across the world towards the chosen destination!

### Differences in Implementation
My implementation was somewhat different to the original design. First of all, as I often do, I made two separate scripts instead of one. This may seem a bit redundant, as the original script only does one thing anyway right now. But that's the important part to me: it might not be doing much _right now_, but in the future, it will likely do a lot more. The GDTV `Unit` class will therefore inevitably break the single-responsibility principle once the unit can do more than just move (although technically, it already handles both movement _and_ user input).

The way I usually go about solving this is to create atomic components for individual properties or actions; I go into more detail about why I do this in my [General Design Principles](https://github.com/Xakaiczar/ProjectXCompany/blob/main/Devlog/Log%20File%201%20-%20General%20Design%20Principles.md) log. In this case, the `Unit`'s ability to move has been extracted into a `Mover` class (I hate the name, but it does what it says on the tin!). This `Mover` is a "property component", containing the functionality required for movement. I consider the `Unit` to be a "behavioural component", which contains the complex interactions between each property component on the `GameObject` and decides when or how a given component may be used.

In short, the `Mover` class represents the legs; they literally _move_ an object from its start location to a given destination, but cannot do so independently. A `Unit`, however, is like the brain; it makes the decision as to how and when that object can move, as well as anything else that happens during that time (e.g. animations / sfx / vfx / etc).

In the same vein, I don't like using `Update` in property components. Once again, a component should only have one responsibility, and that's to perform a single action or modify a specific property. It's very rare that a property would necessarily need to change in real time.

One side effect of the instructor's original design is the inappropriately named "`Move`" function, which doesn't actually _move_ the unit; it actually sets the destination. It feels a bit more apt in my interpretation, though - in hindsight - I should really call it "`MoveTowards`", as it's actually `Update` that causes the movement to continue until the target is reached.

Another minor change between the two was the elevation of a couple of variables - `speed` and `accuracyTolerance` / `stoppingDistance` - from `Update` to become serialised member variables. Honestly, I think it's just nice for the designers to have easy access to these properties for fine-tuning gameplay. Not that I have any other designers, but I'm sure future me will be grateful! It also felt a bit redundant recreating and redefining constant values on every frame. It probably wouldn't affect performance too much, but this just felt a bit cleaner. I'm not entirely sure why we're caching the destination though. It seems a bit redundant, especially since we pass the destination every time `Move` is called. I will probably remove it as soon as it seems apt.

Finally, since I know from the design brief that the game is grid-based, I added a line of code that simply snapped the unit to the exact grid position when they got close enough.

As for modifications to my code, after watching the instructor go through their solution, I realised that "`accuracyTolerance`" was not a particularly descriptive name for a variable. "What does that even mean?!" I asked myself in hindsight. They had a much better name: `stoppingDistance`. Simple, but effective, as it should be.
