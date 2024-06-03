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
