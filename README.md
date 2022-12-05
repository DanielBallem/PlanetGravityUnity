# PlanetGravityUnity
Quick implementation of spherical and cube-like gravity for a game. Similar to mario galaxy


https://user-images.githubusercontent.com/33844493/205741693-33a5f9d6-4366-424c-a444-2d56cdfe1f5c.mp4

The demo sets up the gravity fields for two primitive planet shapes: Spheres and Cubes. 

## Benefits of my approach:

1. Objects that are subjected to gravity use the GravityAffectedObject component. They call on the gravity sources they're near to get the gravity they need. They set their orientation accordingly.
2. Gravity sources have trigger boxes that dictate their gravity field (useful for level development). Triggers / field types can be mixed and matched.
3. Gravities acting on objects are dictated by a priority number. The source with the highest priority will be used to dictate object gravity.
  - In the case where sources have the same priority, the closest source is prefered. 
4. More shapes could be implimented in the same way, allowing for more level flexibility. 
5. You could keep the sphere player as the collider, but replace the mesh with an animated figure. This is how Mario Galaxy worked. Orientation will always face the direction of gravity (in the positive direction)
