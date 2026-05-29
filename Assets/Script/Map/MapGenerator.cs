using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width = 200;
    public int height = 200;
    public float noiseScale = 0.1f;
    [SerializeField] private MapRenderer Renderer;

    public TileData[,] MapGround { get; private set; }
    public TileData[,] MapBuild { get; private set; }

    public void Generate()
    {

        MapGround = new TileData[width, height];
        MapBuild = new TileData[width, height];


        //mapground
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {


                float n = Mathf.PerlinNoise(x * noiseScale, y * noiseScale);


                //  MapGround[location] = new TilData(TileType, walkable?)
                if (n < 0.4f)
                {
                    MapGround[x, y] = new TileData(TileType.Water);
                }
                else if (n < 0.45f)
                {
                    MapGround[x, y] = new TileData(TileType.Sand);
                }
                else
                {
                    MapGround[x, y] = new TileData(TileType.Grass);
                }
            }
        }


        //map build
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                MapBuild[x, y] = new TileData(TileType.Empty);
            }
        }
    }

    public void BuildTile(int x, int y, TileType type)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)  {Debug.LogError("out of bounds"); return;}

        MapBuild[x, y] = new TileData(type);
        Renderer.DrawBuild();
    }


    
}