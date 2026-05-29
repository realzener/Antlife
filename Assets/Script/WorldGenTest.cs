using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGenTest : MonoBehaviour
{

    public TileData[,] Map { get; private set; }

    [SerializeField] private Tile Groundtile;
    public Vector3Int position;
    public Tilemap tilemap;


    [ContextMenu("paint")]


    void paint()
    {
        tilemap.SetTile(position, Groundtile);
    }




   
    
}


