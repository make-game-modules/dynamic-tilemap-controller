using UnityEngine.Tilemaps;
using UnityEngine;
/// <summary>
/// ��̬���ɵ�ͼ�Ľű����ýű�Ӧ���������ǽ�ɫ�ϡ�
/// ���ݽ�ɫ���ƶ�����̬�ڽ�ɫ��Χ���ɵ�ͼ������ʱ�Ƴ����ɫ��Զ�ĵ�ͼ���Ա������ܡ�
/// ͨ���������Կ��Կ��Ƶ�ͼ�����ɷ�Χ��������ֵ���Ƴ�������Ƴ����롣
/// </summary>
public class DynamicTilemapController : MonoBehaviour
{
    /// <summary>
    /// Ҫ������Tilemap���
    /// </summary>
    public Tilemap tilemap;

    /// <summary>
    /// Ҫ���ɵĵ�ͼ��Ƭ
    /// </summary>
    public TileBase tile;

    /// <summary>
    /// ��ͼ�Ŀɼ���Χ����������Ұ�ı���
    /// </summary>
    public float visibilityMultiplier = 2f;

    /// <summary>
    /// ����ɫ�ƶ�����ͼ��Ե�İٷֱ�ʱ��������ͼ����
    /// </summary>
    public float edgeThresholdPercentage = 0.8f;

    /// <summary>
    /// ��ͼ��Ƭ���Ƴ�������룩
    /// </summary>
    public float removalInterval = 5f;

    /// <summary>
    /// �Ƴ���ͼ��Ƭ�ľ�������������Ұ�ı���
    /// </summary>
    public float removalDistanceMultiplier = 2f;

    /// <summary>
    /// ��¼��һ�ε�ͼ���µ�λ��
    /// </summary>
    private Vector3 lastUpdatePos;

    /// <summary>
    /// ����Ϸ��ʼʱ�����ɵ�ͼ�����ö�ʱ�Ƴ�����
    /// </summary>
    void Start()
    {
        UpdateMap();
        lastUpdatePos = transform.position;
        InvokeRepeating(nameof(RemoveOldTiles), removalInterval, removalInterval);
    }

    /// <summary>
    /// ��ÿһ֡������ɫ�Ƿ��ƶ����˵�ͼ�ı�Ե������ǣ��͸��µ�ͼ
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
    /// ���µ�ͼ�����ݽ�ɫ��λ�ã�����һƬ�µĵ�ͼ
    /// </summary>
    void UpdateMap()
    {
        // ����Tilemap�Ŀ�Ⱥ͸߶�
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
    /// �Ƴ����ɫ��Զ�ĵ�ͼ��Ƭ���Ա�������
    /// </summary>
    void RemoveOldTiles()
    {
        Vector3Int currentPos = tilemap.WorldToCell(transform.position);

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            // �Ա����λ�ú�����λ�õ�ˮƽ�ʹ�ֱ���룬����κ�һ�������������ľ��룬���Ƴ������Ƭ
            if (Mathf.Abs(currentPos.x - pos.x) > Camera.main.orthographicSize * removalDistanceMultiplier * Camera.main.aspect ||
                Mathf.Abs(currentPos.y - pos.y) > Camera.main.orthographicSize * removalDistanceMultiplier)
            {
                tilemap.SetTile(pos, null);
            }
        }
    }
}
