using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Old_Job
{
    public Vector3Int tilePosition { get; private set; }
    public Vector2Int tilePosition2d { get; private set; }
    
    Tile tile;
    float jobTime;
    public bool isAssigned;
    public AntAI assignedAnt;


    Action<Old_Job> cbJobComplete;
    Action<Old_Job> cbJobCancel;
    private bool isCompleted;
    private bool isCancelled;

    public Old_Job(Vector3Int tilePosition, Action<Old_Job> cbJobComplete, float jobTime= 1f)
    {
        this.tilePosition = tilePosition;
        this.tilePosition2d = new Vector2Int(tilePosition.x, tilePosition.y);
        this.cbJobComplete += cbJobComplete;
        this.jobTime = jobTime;
    }

    
    public void RegisterJobCompleteCallback(Action<Old_Job> cb){cbJobComplete += cb;}
    public void RegisterJobCancelCallback(Action<Old_Job> cb){cbJobCancel += cb;}


    public bool DoWork(float worktime)
    {
        if (isCompleted || isCancelled)
            return true;
            
        jobTime -= worktime;

        if(jobTime <= 0)
        {
            Complete();
            return true;
        }
        return false;
    }

    public void Assign()
    {
        isAssigned = true;
    }


    private void Complete()
    {
        if (isCompleted|| isCancelled) return;

        isCompleted = true;
        cbJobComplete?.Invoke(this);
    }

    public void CancelJob()
    {
        if (isCancelled || isCompleted) return;

        isCancelled = true;
        cbJobCancel?.Invoke(this);
    }

    public bool IsDone => isCompleted || isCancelled;








    
}
