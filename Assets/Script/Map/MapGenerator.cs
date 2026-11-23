using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    

    public static MapGenerator Instance { get; private set; }

    public Pathfinding pathfinding;
    float offsetX;
    float offsetY;



    [SerializeField] public int width = 1000;
    [SerializeField] public int height = 1000;
    public float noiseScale = 0.1f;
   

    public GridGen<TileData> MapGround;
    public GridGen<TileData> MapBuild;

    [SerializeField] private float deepWaterThreshold = 0.15f;
    [SerializeField] private float waterThreshold = 0.20f;
    [SerializeField] private float sandThreshold = 0.25f;
    [SerializeField] private float mudThreshold = 0.35f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        offsetX = Random.Range(0f, 10000f);
        offsetY = Random.Range(0f, 10000f);
    }


    public void Generate()
    {
        MapGround = new GridGen<TileData>(width, height, (x,y) => {return new TileData(TileType.Grass);});
        MapBuild = new GridGen<TileData>(width, height, (x,y) => {return new TileData(TileType.Empty);});

        pathfinding = new Pathfinding(width, height);

        GenerateGround();
        GenerateBuild();

    }

    public void GenerateGround(){
        Debug.Log("GenerateGround");
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {


                float n = Mathf.PerlinNoise((x + offsetX) * noiseScale, (y + offsetY) * noiseScale);


                //  MapGround[location] = new TilData(TileType, walkable?)
                if (n < deepWaterThreshold)
                {
                    MapGround.Set(x, y, new TileData(TileType.DeepWater));
                }
                else if (n < waterThreshold)
                {
                    MapGround.Set(x, y, new TileData(TileType.Water));
                }
                else if (n < sandThreshold)
                {
                    MapGround.Set(x, y, new TileData(TileType.Sand));
                }
                else if (n < mudThreshold)
                {
                    MapGround.Set(x, y, new TileData(TileType.Mud));
                }
                else
                {
                    MapGround.Set(x, y, new TileData(TileType.Grass));
                }
            }
        }
        pathfinding.RefreshGrid();
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
        MapRenderer.Instance.DrawTile(1, x,y, MapBuild.Get(x, y));
    }


    
}