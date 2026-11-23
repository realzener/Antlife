using System.IO;
using UnityEngine;

public class PathNode 
{
    public int x, y;

    public float GCost, HCost, FCost;

    public PathNode prevNode;

    public bool inOpen;
    public bool inClosed;   

    public bool iswalkable;
    public float Walkspeed;

    public PathNode(int x, int y, bool walkable, float Walkspeed)
    {
        this.x = x;
        this.y = y;
        this.iswalkable = walkable;
        this.Walkspeed = Walkspeed;
    }

    public void CalculateFCost()
    {
        FCost = GCost + HCost;
    }
}
