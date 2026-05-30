using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    public static JobManager Instance;
    public bool isAssigned;

    private List<Job> availableJobs = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddJob(Job job)
    {
        availableJobs.Add(job);
    }
    
    public void RemoveJob(Job job)
    {
        availableJobs.Remove(job);
    }

    public Job RequestJob(Vector3Int antPos)
    {
        Job bestJob = null;
        float bestDist = float.MaxValue;

        foreach (var job in availableJobs)
        {
            float dist = Vector3Int.Distance(antPos, job.tilePosition);

            if (dist < bestDist)
            {
                bestDist = dist;
                bestJob = job;
            }
        }

        if (bestJob != null)
            availableJobs.Remove(bestJob); // reserve it

        return bestJob;
    }
}