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
