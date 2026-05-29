using UnityEngine;

public class GameController : MonoBehaviour
{
    private MapGenerator generator;

    private MapRenderer mapRenderer ;


     private void Awake()
    {
        
    }
    void Start()
    {
        generator = MapGenerator.Instance;
        mapRenderer = MapRenderer.Instance;


        
        generator.Generate();
        mapRenderer.DrawTileGrid(generator.MapGround,0 );
        mapRenderer.DrawTileGrid(generator.MapBuild, 1);
    }

    public void Drawall()
    {
        
    }
}