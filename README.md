# Camera Follow Character Controller

This project provides a Unity script that makes the camera follow the character's movement smoothly. This script needs to be attached to the camera.

## How to Install

In your Unity project, clone this repository at any location using Git.

## How to Use

1. Attach this script to the main camera.
2. Set the public properties of the script in the Unity editor.

## Parameter Settings

- `playerTransform`: The Transform component of the character to follow. Set this property in the Unity editor to point to the character's Transform component.
- `smoothSpeed`: The parameter that controls the smoothness of the camera movement. The larger the value, the faster the camera follows the character, and vice versa.
- `offset`: The offset of the camera relative to the character. Set this property in the Unity editor to control the camera's perspective.

## Operating Principle

At each physical update, the script calculates the target position of the camera and smoothly moves the camera using the Lerp function, achieving a smooth follow effect.

## Copyright Information

This project uses the MIT open source license. Everyone is welcome to improve and use the project.
