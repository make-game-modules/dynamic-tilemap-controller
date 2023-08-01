using UnityEngine.Tilemaps;
using UnityEngine;
/// <summary>
/// 动态生成地图的脚本，该脚本应挂载在主角角色上。
/// 根据角色的移动，动态在角色周围生成地图，并定时移除离角色过远的地图，以保持性能。
/// 通过设置属性可以控制地图的生成范围、更新阈值、移除间隔和移除距离。
/// </summary>
public class DynamicTilemapController : MonoBehaviour
{
    /// <summary>
    /// 要操作的Tilemap组件
    /// </summary>
    public Tilemap tilemap;

    /// <summary>
    /// 要生成的地图瓦片
    /// </summary>
    public TileBase tile;

    /// <summary>
    /// 地图的可见范围相对于相机视野的倍数
    /// </summary>
    public float visibilityMultiplier = 2f;

    /// <summary>
    /// 当角色移动到地图边缘的百分比时，触发地图更新
    /// </summary>
    public float edgeThresholdPercentage = 0.8f;

    /// <summary>
    /// 地图瓦片的移除间隔（秒）
    /// </summary>
    public float removalInterval = 5f;

    /// <summary>
    /// 移除地图瓦片的距离相对于相机视野的倍数
    /// </summary>
    public float removalDistanceMultiplier = 2f;

    /// <summary>
    /// 记录上一次地图更新的位置
    /// </summary>
    private Vector3 lastUpdatePos;

    /// <summary>
    /// 在游戏开始时，生成地图并设置定时移除任务
    /// </summary>
    void Start()
    {
        UpdateMap();
        lastUpdatePos = transform.position;
        InvokeRepeating(nameof(RemoveOldTiles), removalInterval, removalInterval);
    }

    /// <summary>
    /// 在每一帧，检查角色是否移动到了地图的边缘，如果是，就更新地图
    /// </summary>
    void Update()
    {
        if (Vector3.Distance(lastUpdatePos, transform.position) > Mathf.Min(tilemap.cellSize.x, tilemap.cellSize.y) * visibilityMultiplier * edgeThresholdPercentage)
        {
            UpdateMap();
            lastUpdatePos = transform.position;
        }
    }

    /// <summary>
    /// 更新地图，根据角色的位置，生成一片新的地图
    /// </summary>
    void UpdateMap()
    {
        // 计算Tilemap的宽度和高度
        int mapWidth = (int)(Camera.main.orthographicSize * visibilityMultiplier * Camera.main.aspect);
        int mapHeight = (int)(Camera.main.orthographicSize * visibilityMultiplier);

        Vector3Int currentPos = tilemap.WorldToCell(transform.position);

        for (int y = currentPos.y - mapHeight; y < currentPos.y + mapHeight; y++)
        {
            for (int x = currentPos.x - mapWidth; x < currentPos.x + mapWidth; x++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                if (tilemap.GetTile(tilePos) == null)
                {
                    tilemap.SetTile(tilePos, tile);
                }
            }
        }
    }

    /// <summary>
    /// 移除离角色过远的地图瓦片，以保持性能
    /// </summary>
    void RemoveOldTiles()
    {
        Vector3Int currentPos = tilemap.WorldToCell(transform.position);

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            // 对比这个位置和主角位置的水平和垂直距离，如果任何一个距离大于清理的距离，就移除这个瓦片
            if (Mathf.Abs(currentPos.x - pos.x) > Camera.main.orthographicSize * removalDistanceMultiplier * Camera.main.aspect ||
                Mathf.Abs(currentPos.y - pos.y) > Camera.main.orthographicSize * removalDistanceMultiplier)
            {
                tilemap.SetTile(pos, null);
            }
        }
    }
}
