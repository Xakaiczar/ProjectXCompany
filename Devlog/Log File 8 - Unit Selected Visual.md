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
Of course, I was never going to make a singleton like `UnitActionSystem`. An explanation can be found in my [first log](https://github.com/Xakaiczar/ProjectXCompany/blob/main/Devlog/Log%20File%201%20-%20General%20Design%20Principles.md#singletons). As usual, I just pass down the necessary data. In this case, a simple bool that tells the `UISelectedUnit` whether the unit is selected or not.

The idea of using events intrigued me. But at the moment, it seems like there isn't a massive difference between using events and a loop in a function call. Since I need to go through every single object anyway to not only enable the selected one, but disable the others, it felt like I didn't really get any value from an event call. I did eventually create an event in [c06e83a](https://github.com/Xakaiczar/ProjectXCompany/commit/c06e83a0493056e6db0b20576dfe690709b55878), but it currently doesn't do anything.
