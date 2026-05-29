using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Job
{
    public Vector3Int tilePosition { get; private set; }
    
    Tile tile;
    float jobTime;

    Action<Job> cbJobComplete;
    Action<Job> cbJobCancel;



    public Job(Vector3Int tilePosition, Action<Job> cbJobComplete, float jobTime= 1f)
    {
        this.tilePosition = tilePosition;
        this.cbJobComplete += cbJobComplete;
        this.jobTime = jobTime;
    }

    
    public void RegisterJobCompleteCallback(Action<Job> cb){cbJobComplete += cb;}
    public void RegisterJobCancelCallback(Action<Job> cb){cbJobCancel += cb;}


    public void DoWork(float worktime)
    {
        jobTime -= worktime;
        if(jobTime <= 0)
        {
            cbJobComplete?.Invoke(this);
        }
    }

    public void CancelJob()
    {
        cbJobCancel?.Invoke(this);
    }
}
