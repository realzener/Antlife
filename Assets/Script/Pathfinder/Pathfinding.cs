using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assemblies;

public class Pathfinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;
    private GridGen<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;


    public Pathfinding(int width, int height)
    {
        grid = new GridGen<PathNode>(width, height, (x, y) => new PathNode(x,y, MapGenerator.Instance.MapGround.Get(x, y).Walkable, MapGenerator.Instance.MapGround.Get(x, y).Walkspeed));
    }

    public GridGen<PathNode> GetGrid() {
        return grid;
    }


    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.Get(startX,startY);
        PathNode endNode = grid.Get(endX,endY);

        openList = new List<PathNode>{startNode};
        startNode.inOpen = true;
        closedList = new List<PathNode>();
        Debug.Log("clickly3");

        for (int x = 0; x < grid.GetLength(0); x++){
            for (int y = 0; y < grid.GetLength(1); y++)
                {
                    PathNode pathNode = grid.Get(x,y);
                    pathNode.GCost = int.MaxValue;
                    pathNode.CalculateFCost();
                    pathNode.prevNode = null;
                    pathNode.inOpen = false;
                    pathNode.inClosed = false;
                }
            }
            Debug.Log("clickly4");
        startNode.GCost = 0;
        startNode.HCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                //found end
                return CalculatePath(endNode);
            }
            
            openList.Remove(currentNode);
            currentNode.inOpen = false;
            closedList.Add(currentNode);
            currentNode.inClosed = true;

            foreach (PathNode neighbourNode in FindNeighbours(currentNode))
            {
                if( neighbourNode.inClosed) continue;
                if(!neighbourNode.iswalkable)
                {
                    neighbourNode.inClosed = true;
                    neighbourNode.inOpen = false;
                    continue;
                }

                float speed = neighbourNode.Walkspeed;
                
                int baseCost = CalculateDistanceCost(currentNode, neighbourNode);
                int moveCost = Mathf.RoundToInt(baseCost / speed);

                int tentativeGcost = currentNode.GCost + moveCost;
                if (tentativeGcost < neighbourNode.GCost)
                {
                    neighbourNode.prevNode = currentNode;
                    neighbourNode.GCost = tentativeGcost;
                    neighbourNode.HCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();


                    if (!neighbourNode.inOpen)
                    {
                        openList.Add(neighbourNode);
                        neighbourNode.inOpen = true;
                    }
                }
    
            }

        }
        // out of nodes on the open list
        Debug.Log("null sent");
        return null;
    }  

    private List<PathNode> FindNeighbours(PathNode currentNode)  
    {
        List<PathNode> neighbourList = new();


        if(currentNode.x - 1 >= 0)
        {
             //left
            neighbourList.Add(GetNode(currentNode.x -1, currentNode.y));

            //left down
            if (currentNode.y -1 >= 0)  neighbourList.Add(GetNode(currentNode.x -1, currentNode.y -1));

            //left up
            if (currentNode.y +1 < grid.Height)  neighbourList.Add(GetNode(currentNode.x -1, currentNode.y +1));
        }

        if(currentNode.x + 1 < grid.Width)
        {
             //right
            neighbourList.Add(GetNode(currentNode.x +1, currentNode.y));

            //right down
            if (currentNode.y -1 >= 0)  neighbourList.Add(GetNode(currentNode.x +1, currentNode.y -1));

            //right up
            if (currentNode.y +1 < grid.Height)  neighbourList.Add(GetNode(currentNode.x +1, currentNode.y +1));
        }

        if(currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        if(currentNode.y + 1 < grid.Height) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbourList;
    }

    private PathNode GetNode(int x, int y)
    {
        return grid.Get(x,y);
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        Debug.Log("path made");
        List<PathNode> path = new();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while(currentNode.prevNode != null)
        {
            path.Add(currentNode.prevNode);
            currentNode =currentNode.prevNode;
        }
        path.Reverse();

        return path;
    }


    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);


        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostnode = pathNodeList[0];


        for(int i =1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].FCost < lowestFCostnode.FCost)
            {
                lowestFCostnode = pathNodeList[i];
            }
        }
        return lowestFCostnode;
    }
}