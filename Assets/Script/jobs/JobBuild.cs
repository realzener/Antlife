using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class JobBuild : Job

{
    TileType tileType;

    public JobBuild(TileType tileType
        ,Vector3Int tilePosition, Action<Job> cbJobComplete, float jobTime = 1) : base(tilePosition, cbJobComplete, jobTime)
    {
        
        this.tileType = tileType;
    }




    public override void Complete()
    {
        if (isCompleted|| isCancelled) return;

        isCompleted = true;
        cbJobComplete?.Invoke(this);

        MapGenerator.Instance.BuildTile(tilePosition2d.x, tilePosition2d.y, TileType.Wall);
    }
}