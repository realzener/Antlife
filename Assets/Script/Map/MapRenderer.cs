using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRenderer : MonoBehaviour
{
    public static MapRenderer Instance { get; private set; }

    public Tilemap tilemapGround;
    public Tilemap tilemapBuild;
    private Tilemap tilemap;
    public MapGenerator generator = MapGenerator.Instance;

    
    public TileBase grassTile;
    public TileBase waterTile;
    public TileBase deepWaterTile;
    public TileBase sandTile;
    public TileBase emptyTile;
    public TileBase mudTile;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void DrawTileGrid(GridGen<TileData> map, int i)
    {
        Tilemap tilemap = (i == 0) ? tilemapGround : tilemapBuild;
        Debug.Log("start");
        tilemap.ClearAllTiles();


        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                tilemap.SetTile(
                    new Vector3Int(x, y, 0),
                    GetTile(map.Get(x, y).Type)
                    
                );
                Debug.Log("middle");
            }
        }
    }
    

    public void DrawTile(int x, int y, TileData data)
    {
        tilemapGround.SetTile(
            new Vector3Int(x, y, 0),
            GetTile(data.Type)
        );
    }



    TileBase GetTile(TileType type)
    {
        return type switch
        {
            TileType.Grass => grassTile,
            TileType.Water => waterTile,
            TileType.DeepWater => deepWaterTile,
            TileType.Sand => sandTile,
            TileType.Empty => emptyTile,
            TileType.Mud => mudTile,
            _ => grassTile
        };
    }
}