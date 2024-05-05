## General Design Principles
This section will detail some of my general design philosophies. Right or wrong, I think it's easier to understand someone's decisions if you understand the logic they use to make those decisions. Plus, if I'm wrong, it'll be easier to explain why!

These principles will likely change over time as I grow as a programmer and as a person. But these are some of the design choices I have made while working on this project.

### Namespaces
Every time I do one of these courses, I end up changing _something_. Sometimes there are only minor differences, which can have a big impact on how the code is structured, but are often easy to implement and maintain as the course progresses. For example, in another course I may have moved the player's death logic out of the `Health` script and into the `Player` script, or another behaviour-oriented script.

Other times, I end up changing the code so fundamentally that it becomes virtually impossible to follow along with the course. This is often compounded by the instructor's opacity in terms of design; they often make questionable decisions early on that sometimes become relevant - or even cause the instructor to correct themselves - later down the line. Unfortunately, I cannot read minds, and often don't know why sections of code are designed the way they are (or if they were even designed at all!). An example of this is the instructor's questionable reliance on singletons that I've had to completely work around, which I will talk more about later.

Either way, I end up making a lot of extra work for myself for very little gain. However, there are benefits to each approach:
- coding it their way means I learn new techniques and design patterns
- coding it my way means I can learn to make my own decisions

So, I figured, why not do both?

I decided to split the code into two core namespaces: one for the GameDevTV lecturer's code (named `GDTV`) and one for my own personal design (named `XCOM`). It's a little bit of extra work, but it means I can focus on making my own solutions to the lecturer's problems (as described at the start of the video) from scratch, as if I had been tasked to write and design it independently. Then, after I'm done, I get to copy their implementation for my own reference and adapt my code with my new knowledge.

Already this method seems to be working a lot better than the last; it means I can spend an appropriate amount of time focusing on both independent problem solving and guided learning. Truly the best of both worlds!

### Single Responsibility Principle
- separate files; cleaner code and separation of responsibilities (SOLID)
  - property files vs. behaviour files
  - personally don't like to use update in property files

### Caching
component caching; naming convention, purpose

### Singletons
No thank you.
