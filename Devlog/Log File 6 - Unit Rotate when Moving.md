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
