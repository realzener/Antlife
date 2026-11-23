using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class hashtest<T>{
    private float cellSize = 0;
    private Dictionary<Vector2Int, List<T>> hashGrid = new();

    public hashtest(float cellSize)
    {
        this.cellSize = Mathf.Max(0.0001f, cellSize);
    }

    private Vector2Int Hash(Vector2 position)
    {
        int x = Mathf.FloorToInt(position.x / cellSize);
        int y = Mathf.FloorToInt(position.y / cellSize);
        return new Vector2Int(x, y);
    }

    public void Insert(Vector2 position, T Item)
    {
        Vector2Int key = Hash(position);

        if (!hashGrid.TryGetValue(key, out var list))
        {
            list = new List<T>();
            hashGrid[key] = list;
        }

        list.Add(Item);
    }

    public List<T> Findobject(Vector2 position)
    {
        Vector2Int key = Hash(position);

        if (hashGrid.TryGetValue(key, out var list))
        return list;

        return new List<T>();
    }

    public List<T> QueryRadius(Vector2 position, int radius = 1)
    {
        List<T> result = new List<T>();
        Vector2Int center = Hash(position);

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                Vector2Int key = new Vector2Int(center.x + x, center.y + y);

                if (hashGrid.TryGetValue(key, out var list))
                    result.AddRange(list);
            }
        }

        return result;
    }


    public void Remove(Vector2 position, T Item)
    {
        Vector2Int key = Hash(position);

        if (hashGrid.TryGetValue(key, out var list))
        {
            list.Remove(Item);

            if (list.Count == 0)
            hashGrid.Remove(key);
        }

        

    }

    public void Update(T Item, Vector2 positionOld, Vector2 positionNew)
    {
        Insert(positionNew, Item);
        Remove(positionOld, Item);
    }

}
