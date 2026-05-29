using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    

    public static MapGenerator Instance { get; private set; }
    private MapRenderer Renderer;


    [SerializeField] public int width = 1000;
    [SerializeField] public int height = 1000;
    public float noiseScale = 0.1f;
   

    public GridGen<TileData> MapGround;
    public GridGen<TileData> MapBuild;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    public void Generate()
    {
        MapGround = new GridGen<TileData>(width, height, (x,y) => {return new TileData(TileType.Grass);});
        MapBuild = new GridGen<TileData>(width, height, (x,y) => {return new TileData(TileType.Empty);});

        GenerateGround();
        GenerateBuild();
    }

    public void GenerateGround(){
        Debug.Log("GenerateGround");
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {


                float n = Mathf.PerlinNoise((x + 1000f) * noiseScale, (y + 1000f) * noiseScale);


                //  MapGround[location] = new TilData(TileType, walkable?)
                if (n < 0.2f)
                {
                    MapGround.Set(x, y, new TileData(TileType.Water));
                }
                else if (n < 0.75f)
                {
                    MapGround.Set(x, y, new TileData(TileType.Sand));
                }
                else
                {
                    MapGround.Set(x, y, new TileData(TileType.Grass));
                }
            }
        }
    }

    public void GenerateBuild(){
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
        Renderer.DrawTile(x,y, MapBuild.Get(x, y));
    }


    
}