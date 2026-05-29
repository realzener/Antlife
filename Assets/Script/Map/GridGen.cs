using UnityEngine;

public class GridGen<TGridobject>
{

    private TGridobject[,] gridArray;

    private int width;
    private int height;

    private TGridobject tgridobject;

    
    public GridGen(int width, int height)
    {
        
        this.width = width;
        this.height = height;

        gridArray = new TGridobject[width, height];
    }

    public TGridobject Get(int x, int y) => gridArray[x,y];
    public void Set(int x, int y, TGridobject tGridobject) => gridArray[x, y] = tGridobject;
    public int GetLength(int dimension)
    {
        return dimension switch
        {
            0 => width,
            1 => height,
            _ => throw new System.IndexOutOfRangeException("Invaild dimension, This grid can only use 1 or 0")
        };
    }
}
