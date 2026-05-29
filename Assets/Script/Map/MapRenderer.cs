using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRenderer : MonoBehaviour
{
    public Tilemap tilemap;
    public Tilemap tilemapBuild;
    public MapGenerator generator;

    public TileBase grassTile;
    public TileBase waterTile;
    public TileBase sandTile;
    public TileBase emptyTile;

    
    public void DrawTileGrid(GridGen<TileData> map)
    {
        tilemap.ClearAllTiles();


        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                tilemap.SetTile(
                    new Vector3Int(x, y, 0),
                    GetTile(map.Get(x, y).Type)
                );
            }
        }
    }

    TileBase GetTile(TileType type)
    {
        return type switch
        {
            TileType.Grass => grassTile,
            TileType.Water => waterTile,

            TileType.Sand => sandTile,
            TileType.Empty => emptyTile,
            _ => grassTile
        };
    }
}