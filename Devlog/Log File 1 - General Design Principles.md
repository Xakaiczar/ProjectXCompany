## General Design Principles
This section will detail some of my general design philosophies. Right or wrong, I think it's easier to understand someone's decisions if you understand the logic they use to make those decisions. Plus, if I'm wrong, it'll be easier to explain why!

These principles will likely change over time as I grow as a programmer and as a person. But these are some of the design choices I have made while working on this project.

### Namespaces
Every time I do one of these courses, I end up changing _something_. Sometimes there are only minor differences, which can have a big impact on how the code is structured, but are often easy to implement and maintain as the course progresses. For example, in another course I may have moved the player's death logic out of the `Health` script and into the `Player`, `Game`, and / or another behaviour-oriented script.

Other times, I end up changing the code so fundamentally that it becomes virtually impossible to follow along with the course. This is often compounded by the instructor's opacity in terms of design; they often make unexplained decisions early on that sometimes become relevant - or perhaps even cause the instructor to correct themselves - later down the line. Unfortunately, I cannot read minds, and often don't know why sections of code are designed the way they are (or if they were even designed at all!). An example in this course is the instructor's questionable reliance on singletons that I've had to completely work around, meaning that none of my solutions are designed like the lecturer originally intended.

Either way, I end up making a lot of extra work for myself for very little gain. However, there are benefits to each approach:
- coding it their way means I learn new techniques and design patterns
- coding it my way means I can learn to make my own decisions

So, I figured, why not do both?

I decided to split the code into two core namespaces: one for the GameDevTV lecturer's code (named `GDTV`) and one for my own personal design (named `XCOM`). It's a little bit of extra work, but it means I can focus on making my own solutions to the lecturer's problems (described by them at the start of the video) from scratch, as if I had been tasked to write and design it independently. Then, after I'm done, I get to copy their implementation for my own reference and adapt my code with my new knowledge.

Already this method seems to be working a lot better than the last; it means I can spend an appropriate amount of time focusing on both independent problem solving and guided learning. Truly the best of both worlds!

### Single Responsibility Principle
Each class should only really do one thing. This is called the Single Responsibility Principle, part of the SOLID design principles for object-oriented programming. In that vein, each class I write tends to be atomic, handling one single part of an object's functionality. I have two different design philosophies for components:
- **Property components:** these components represent a single property or action. Their sole responsibility is to either modify or return a piece of data attached to an object (such as a player's health), or perform a function on that object (for instance, pathfinding for a unit on a grid-based game). These components cannot operate alone; they describe what an object is and what it can do, but not how or when it must do it.
- **Behavioural components**: these components coordinate property components in order to create a desired behaviour in the object they're attached to. While a bit more complex, their sole purpose is to decide when certain actions must be taken, as well as the conditions under which properties can change. These components are almost like the "brains" of the object, tying together data and functions to create more complex behaviours.

For example: `Health` would be a "property component", as it stores a value, as well as methods to modify and return that value for UI or logical processing elsewhere in the program. However, in order to adhere to the Single Responsibility Principle, it does _not_ contain any logic about if or when that object dies, nor what happens after. That is decided by `Unit`. For instance, when the value of `Health` reaches 0, a `Unit` may trigger a death function, which would disable the unit object and possibly trigger any animation / sfx / vfx / etc from another property component.

The result is cleaner, easier to read (and debug!) code.

### Component Caching
Calling methods like `GetComponent` or `FindObjectOfType` can be expensive at scale. If you have to call one of these every time you want to access a component, that's a lot of extra work for your poor CPU. The simple solution is to call the function once during `Start` or `Awake` and cache the results. Easy!

The problem I often had with that is in the script execution order, or even sometimes missing references from scene changes. These can be worked around in a convoluted way, but I set up a simple system to reduce (and, so far, resolve!) the null pointer errors from cached components.

I still store the reference in a private variable as usual, but now I also create a protected getter. From now on, whenever I try to access the reference, I go through the getter. If that variable has a reference (i.e. is not null) then great! All the getter will do is return the reference. If not, however, the getter actually tries to get a reference first and stores it in the private variable before returning it. Each component only gets the references it needs as and when it needs them, as opposed to fetching them all on `Start` and just _hoping_ they start before another key component...

### Singletons
Singletons are defined as classes that do two things:
1. Restrict a class to a single instance of itself
2. Make a class publicly accessible to the rest of the system

Singletons are necessary in Unity sometimes. Take the first point: sometimes, you need a class to only exist once, with some consistency between scene loads. For example, going from scene to scene you might need some persistent data. This can vary between games, but at the very least, you probably don't want the music stopping and starting every scene load, nor do you want the player to lose all their data between scenes. If those systems aren't singletons however, you may have the opposite problem: conflicting player data and double the music! What a nightmare!

That said, singletons are often described as an "anti-pattern" (including by GameDevTV themselves) for exactly the second point: they are functionally global data that can be silently accessed by anything in the scene without passing references. If they are stateful, they are doubly as bad, as stale state may persist throughout the runtime of the game, creating all kinds of untestable issues. As far as I'm concerned, unless they're on the top level of code - in other words, they _are_ the global state - then they're not the ideal solution to any problem.

Which is why it's very odd that - throughout the course - the lecturer creates _so many singletons_.

In this specific case, the lecturer uses so many singletons so freely that a lot of the code is highly dependent on these objects existing in the first place. If _any_ of these objects are not in the game for any reason, half the code base would break!

Since this is such a huge part of the lecturer's design, I felt like I needed to justify my use (or not, as the case may be) of singletons. This is how I go about doing it:
- if an object needs to persist between scenes, and that object must be the only one of its kind: it's a singleton
- otherwise, it can - and should - be done the old fashioned way: it's a regular component

I would also _never_ have a singleton be considered global state. Global state has the potential to introduce too many hard-to-track bugs even in single-threaded applications. I would much rather just pass  the necessary data down through parameters, rather than the _entire_ global object, or use events to synchronise unrelated - but thematically linked - procedures. In the words of the lecturer of this very course:
> _Static should be used very sparingly. They basically act as global state, which is always tricky and prone to mistakes._

Plus, if and when the need arises, I would not have a component _also_ be a singleton; that violates the single responsibility principle. I would probably just create a `Singleton` component whose sole purpose is to make the attached object a singleton, or even a `SingletonManager` that tracks all the wannabe-singleton objects and makes sure they persist as you'd expect, deleting any duplicates.

Needless to say, a lot of the original singletons didn't make it. I regret nothing.
