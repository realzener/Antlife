using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Job
{
    public Vector3Int tilePosition { get; private set; }
    
    Tile tile;
    float jobTime;
    public bool isAssigned;

    Action<Job> cbJobComplete;
    Action<Job> cbJobCancel;
    private bool isCompleted;
    private bool isCancelled;

    public Job(Vector3Int tilePosition, Action<Job> cbJobComplete, float jobTime= 1f)
    {
        this.tilePosition = tilePosition;
        this.cbJobComplete += cbJobComplete;
        this.jobTime = jobTime;
    }

    
    public void RegisterJobCompleteCallback(Action<Job> cb){cbJobComplete += cb;}
    public void RegisterJobCancelCallback(Action<Job> cb){cbJobCancel += cb;}


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
