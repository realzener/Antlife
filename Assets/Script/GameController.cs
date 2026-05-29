using UnityEngine;

public class GameController : MonoBehaviour
{
    public MapGenerator generator;
    public MapRenderer Renderer;

    void Start()
    {
        generator.Generate();
        Renderer.DrawTileGrid(generator.MapGround);
        Renderer.DrawTileGrid(generator.MapBuild);
    }
}