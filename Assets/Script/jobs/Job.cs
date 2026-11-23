using System;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Job
{
    public Vector3Int tilePosition { get; private set; }
    public Vector2Int tilePosition2d { get; private set; }
    float jobTime;
    
    public bool isAssigned;

    public Action<Job> cbJobComplete;
    Action<Job> cbJobCancel;
    public bool isCompleted;
    public bool isCancelled;

    public Job(Vector3Int tilePosition, Action<Job> cbJobComplete, float jobTime= 1f)
    {
        this.tilePosition = tilePosition;
        this.tilePosition2d = new Vector2Int(tilePosition.x, tilePosition.y);
        this.cbJobComplete += cbJobComplete;
        this.jobTime = jobTime;
    }

    

    // public bool IsDone => isCompleted || isCancelled;
    // public void RegisterJobCompleteCallback(Action<Job> cb){cbJobComplete += cb;}
    // public void RegisterJobCancelCallback(Action<Job> cb){cbJobCancel += cb;}


    public virtual bool DoWork(float worktime)
    {
        if (isCompleted || isCancelled)
            return true;
            
        jobTime -= worktime;

        if(jobTime <= 0)
        {
            this.Complete();
            return true;
        }
        return false;
    }


    public virtual void Complete()
    {
        if (isCompleted|| isCancelled) return;

        isCompleted = true;
        cbJobComplete?.Invoke(this);
    }

    public virtual void CancelJob()
    {
        if (isCancelled || isCompleted) return;

        isCancelled = true;
        cbJobCancel?.Invoke(this);
    }

    








    
}
