using System.Collections.Generic;
using UnityEngine;

public class JobManager1 : MonoBehaviour
{
    public static JobManager1 Instance;

    public bool isAssigned;

    private List<Job> availableJobs = new();


    public hashtest<GameObject> unitGrid = new hashtest<GameObject>(5f);

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

    private void Update()
    {
        TryAssignJobs();
    }

    private void TryAssignJobs()
    {
        foreach (var job in availableJobs){

            if (job.isAssigned)
            continue;

            AntAI bestAnt = FindBestAnt(job);

            if (bestAnt == null)
                return;

            job.isAssigned = true;
            bestAnt.JobRequest(job);
        }
    }

    public AntAI FindBestAnt(Job job)
    {

        AntAI bestAnt = null;
        float bestDist = float.MaxValue;
        const int maxRadius = 50;

        for (int radius = 1; radius <= maxRadius; radius++){

        List<GameObject> nearby = unitGrid.QueryRadius(job.tilePosition2d, radius);



            foreach (GameObject gameObject in nearby)
            {
                AntAI ant = gameObject.GetComponent<AntAI>();


                if (ant == null)
                continue;

                if (ant.state != AntAI.State.Idle)
                continue;


                float dist = ((Vector2)ant.transform.position - job.tilePosition2d).sqrMagnitude;

                if (dist < bestDist)
                {
                    bestDist = dist;
                    bestAnt = ant;
                }

            }

            if (bestAnt != null)
            return bestAnt;
        }



        return null;
        
    }
}