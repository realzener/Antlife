public class TileData
{
    public TileType Type { get; private set; }
    public bool Walkable { get; private set; }

    public TileData(TileType type)
    {
        Type = type;

        Walkable = type switch
        {
            TileType.Water => false,
            _ => true
        };
    }
}