public class TileData
{
    public TileType Type { get; private set; }
    public bool Walkable { get; private set; }

    public TileData(TileType type)
    {
        Type = type;

        switch(type)
        {
            case TileType.Water:
                Walkable = false;
                break;

            default:
                Walkable = true;
                break;
        }
    }
}