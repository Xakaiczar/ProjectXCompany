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
