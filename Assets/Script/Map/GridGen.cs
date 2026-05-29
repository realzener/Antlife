using System;
using UnityEngine;

public class GridGen<TGridobject>
{

    private TGridobject[,] gridArray;

    public int Width {get; private set;}
    public int Height {get; private set;}

    private TGridobject tgridobject;

    
    public GridGen(int width, int height, Func<int, int, TGridobject> createGridObject)
    {
        
        Width = width;
        Height = height;

        gridArray = new TGridobject[Width, Height];

         for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                gridArray[x, y] = createGridObject(x, y);
            }
        }
    }

    public TGridobject Get(int x, int y) => gridArray[x,y];
    public void Set(int x, int y, TGridobject tGridobject) => gridArray[x, y] = tGridobject;
    public int GetLength(int dimension)
    {
        return dimension switch
        {
            0 => Width,
            1 => Height,
            _ => throw new System.IndexOutOfRangeException("Invaild dimension, This grid can only use 1 or 0")
        };
    }

}
