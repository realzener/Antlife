using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManagerpiss : MonoBehaviour
{
    private float _jobTimer;
    [SerializeField]
    private float jobTimer = 3f;
    public static JobManagerpiss Instance;

    [SerializeField]
    public Dictionary <AntPiss.State, List<AntPiss>> antsByState = new();

    public List<Job> availableJobs = new();
    public hashtest<AntPiss> unitGrid = new hashtest<AntPiss>(5f);
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

        foreach (AntPiss.State state in (AntPiss.State[])Enum.GetValues(typeof(AntPiss.State)))
        {
            antsByState.Add(state, new List<AntPiss>());
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
        
        try
        {

            if (antsByState[AntPiss.State.Idle].Count == 0)
                yield break;

            int framebreak = 0;
            
            
            foreach (var job in availableJobs.ToArray())
            {

                if(job.isAssigned)
                continue;

                if (antsByState[AntPiss.State.Idle].Count == 0)
                yield break;


                AntPiss bestAnt = FindbestAnt(job);

                if(bestAnt == null )
                continue;
                else{
                    AntManager.Instance.JobRequest(job, bestAnt);
                }


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


    public AntPiss FindbestAnt(Job job)
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


            AntPiss bestAnt = null;
            float bestDist = float.MaxValue;



            foreach (AntPiss ant in nearby)
            {


                if (ant == null)
                continue;

                if (ant.state != AntPiss.State.Idle)
                continue;

                // Vector2 antPos = ant.transform.position;
                // float dist = ((Vector2)antPos- job.tilePosition2d).sqrMagnitude;


                // if (dist < bestDist)
                // {
                //     bestDist = dist;
                //     bestAnt = ant;
                // }


                if(ant != null)
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