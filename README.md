# Dynamic Tilemap Controller

[中文](https://github.com/make-game-modules/dynamic-tilemap-controller/blob/main/README.zh-cn.md)

This project provides a Unity script for dynamically generating maps based on the character's movement and regularly removing maps that are too far from the character to maintain performance. By setting properties, you can control the range of map generation, update threshold, removal interval, and removal distance.

## How to Install

In your Unity project, clone this repository at any location using Git.

## How to Use

Mount this script to the main character.

## Parameter Settings

1. `Tilemap tilemap`: The Tilemap component to operate
2. `TileBase tile`: The map tile to be generated
3. `float visibilityMultiplier`: The visible range of the map relative to the camera's field of view multiplier
4. `float edgeThresholdPercentage`: When the character moves to a certain percentage of the map edge, the map update is triggered
5. `float removalInterval`: The removal interval (seconds) of the map tile
6. `float removalDistanceMultiplier`: The removal distance of the map tile relative to the camera's field of view multiplier

## Operating Principle

This script checks every frame if the character has moved to the edge of the map. If so, it updates the map and regularly removes map tiles that are too far from the character to maintain performance.

## Copyright Information

This project uses the MIT open source license. Everyone is welcome to improve and use the project.
