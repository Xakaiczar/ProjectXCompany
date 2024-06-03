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

The only other notable difference was in factoring out the ray instantiation as its own function (a.k.a. `GetRayFromMousePosition`). I only did this to make the code more readable, but it may also be useful in the future!
