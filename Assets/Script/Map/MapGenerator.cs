using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    
    [SerializeField] private int width = 200;
    [SerializeField] private int height = 200;
    public float noiseScale = 0.1f;
    [SerializeField] private MapRenderer Renderer;

    public GridGen<TileData> MapGround;
    public GridGen<TileData> MapBuild;

    public void Generate()
    {

        MapBuild = new(width, height);
        MapGround = new(width, height);


        //mapground
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {


                float n = Mathf.PerlinNoise(x * noiseScale, y * noiseScale);


                //  MapGround[location] = new TilData(TileType, walkable?)
                if (n < 0.4f)
                {
                    MapGround.Set(x, y, new TileData(TileType.Water));
                }
                else if (n < 0.45f)
                {
                    MapGround.Set(x, y, new TileData(TileType.Sand));
                }
                else
                {
                    MapGround.Set(x, y, new TileData(TileType.Grass));
                }
            }
        }


        //map build
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                MapBuild.Set(x, y, new TileData(TileType.Empty));
            }
        }
    }

    public void BuildTile(int x, int y, TileType type)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)  {Debug.LogError("out of bounds"); return;}

        MapBuild.Set(x,y, new TileData(type));
        Renderer.DrawTileGrid(MapGround);
    }


    
}