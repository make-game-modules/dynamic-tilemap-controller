# 动态地图生成控制器

[English](https://github.com/make-game-modules/dynamic-tilemap-controller/blob/main/README.md)

这个项目提供了一个 Unity 脚本，用于根据角色的移动动态生成地图，并定时移除离角色过远的地图，以保持性能。通过设置属性，您可以控制地图的生成范围、更新阈值、移除间隔和移除距离。

## 如何安装

在 Unity 项目中，任意位置使用 git clone 本仓库即可。

## 如何使用

将该脚本挂载到主角角色上即可。

## 参数设置

1. `Tilemap tilemap`: 要操作的Tilemap组件
2. `TileBase tile`: 要生成的地图瓦片
3. `float visibilityMultiplier`: 地图的可见范围相对于相机视野的倍数
4. `float edgeThresholdPercentage`: 当角色移动到地图边缘的百分比时，触发地图更新
5. `float removalInterval`: 地图瓦片的移除间隔（秒）
6. `float removalDistanceMultiplier`: 移除地图瓦片的距离相对于相机视野的倍数

## 运行原理

这个脚本在每一帧检查角色是否移动到了地图的边缘，如果是，就更新地图，并定时移除离角色过远的地图瓦片，以保持性能。

## 版权信息

本项目采用 MIT 开源许可证，欢迎任何人对项目的改进和使用。
