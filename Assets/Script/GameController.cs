using UnityEngine;

public class GameController : MonoBehaviour
{
    public MapGenerator generator;
    public MapRenderer Renderer;

    void Start()
    {
        generator.Generate();
        Renderer.DrawGround();
        Renderer.DrawBuild();
    }
}