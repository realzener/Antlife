public class TileData
{
    public TileType Type { get; private set; }
    public bool Walkable { get; private set; }
    public float Walkspeed{ get; private set; }

    public TileData(TileType type)
    {
        Type = type;

        Walkable = type switch
        {
            TileType.Water => false,
            TileType.Empty => false,
            _ => true
        };

        Walkspeed = type switch
        {
            TileType.Sand => 0.3f,
            _ => 1
        };

    }
}