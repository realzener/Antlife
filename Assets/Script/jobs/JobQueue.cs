using System.Collections.Generic;
using UnityEngine;

public class JobQueue : MonoBehaviour
{
  public static JobQueue Instance;
  Queue<Job> jobQueue;
  public int Count => jobQueue.Count;

  void Awake()
  {
    Instance = this;
    jobQueue = new Queue<Job>();  
  }

  public void AddJob(Job job)
    {
        jobQueue.Enqueue(job);
    }


    public Job GetNextJob()
    {
        if (jobQueue.Count == 0)
            return null;

        return jobQueue.Dequeue();
    }

    public Job PeekJob()
    {
        if (jobQueue.Count == 0)
            return null;

        return jobQueue.Peek();


    }
}

