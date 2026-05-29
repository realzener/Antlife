using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Job
{

    
    Tile tile;
    float jobTime;

    Action<Job> cbJobComplete;
    Action<Job> cbJobCancel;



    public Job(Tile tile, Action<Job> cbJobComplete, float jobTime= 1f)
    {
        this.tile = tile;
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
            if(cbJobComplete !=null){
                cbJobComplete(this);
            }
        }
    }

    public void CancelJob()
    {
        if(cbJobCancel != null)
        {
            cbJobCancel(this);
        }
    }
}
