using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    private float _jobTimer;
    [SerializeField]
    private float jobTimer = 3f;
    public static JobManager Instance;

    [SerializeField]
    public Dictionary <AntAI.State, List<AntAI>> antsByState = new();

    public List<Job> availableJobs = new();
    public hashtest<AntAI> unitGrid = new hashtest<AntAI>(5f);
    Coroutine jobRoutine;
    public int jobsPerFrameBreak = 100;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        foreach (AntAI.State state in (AntAI.State[])Enum.GetValues(typeof(AntAI.State)))
        {
            antsByState.Add(state, new List<AntAI>());
        }   
        _jobTimer = jobTimer;


    }
    

    public void AddJob(Job job)
    {
        availableJobs.Add(job);
    }
    
    public void RemoveJob(Job job)
    {
        availableJobs.Remove(job);
    }

  


    public IEnumerator TryAssignJobs()
    {
        try{

        int idleAnts = antsByState[AntAI.State.Idle].Count;

        if (idleAnts == 0)
            yield break;

        int framebreak = 0;
        
        foreach (var job in availableJobs.ToArray())
        {
            if(job.isAssigned)
            continue;

            idleAnts = antsByState[AntAI.State.Idle].Count;
            if (idleAnts == 0)
            yield break;


            AntAI bestAnt = FindbestAnt(job);

            if(bestAnt == null )
            continue;
            else
            bestAnt.JobRequest(job);

            job.isAssigned = true;
            
            framebreak++;
            if (framebreak >= jobsPerFrameBreak){
                framebreak = 0;
                yield return null;
            }
        }
        
    }
        finally
        {
            jobRoutine = null;
        }
    }


    public AntAI FindbestAnt(Job job)
    {
        if(job.isAssigned)
        return null;



        int radius = 1;
        int maxRadius = 80;
        
    
        bool finished = false;
        
        while (!finished){
            if(radius >= maxRadius)
            radius = maxRadius;



            var nearby = unitGrid.QueryRadius(job.tilePosition2d, radius);

            AntAI bestAnt = null;
            float bestDist = float.MaxValue;

            foreach (AntAI ant in nearby)
            {

                if (ant == null)
                continue;

                if (ant.state != AntAI.State.Idle)
                continue;

                // Vector2 antPos = ant.transform.position;
                // float dist = ((Vector2)antPos- job.tilePosition2d).sqrMagnitude;


                // if (dist < bestDist)
                // {
                //     bestDist = dist;
                //     bestAnt = ant;
                // }



                return ant;

            }
            if (bestAnt != null)
            return bestAnt;


            
            if(radius >= maxRadius)
            finished = true;
            

            radius *= 2;
        }

        return null;
        
    }

    public void Update()
    {
        _jobTimer -= Time.deltaTime;

        if (_jobTimer <= 0f)
        {
            jobRoutine ??= StartCoroutine(TryAssignJobs());
            
            _jobTimer = jobTimer;
        }
            

    }

}