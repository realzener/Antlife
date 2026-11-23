using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AntManager : MonoBehaviour
{

    

    
    private float newZ = -2f;



    

    [SerializeField]GameObject antPrefab;

    [SerializeField] int amount;

    private List<AntPiss> spawnedAnts = new List<AntPiss>();

    public static AntManager Instance;

    public void Start()
    {


         if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        SpawnAnts(amount);


        
    }
    public void SpawnAnts(int amount = 1)
    {
        Vector3 pos = new Vector3(1,1,1);
        for(int i = 0; i < amount; i++){
        GameObject antobj = Instantiate(antPrefab, pos, quaternion.identity);
        var ant = antobj.GetComponent<AntPiss>();



        spawnedAnts.Add(ant);
        MakeState(AntPiss.State.Idle, ant);
        JobManagerpiss.Instance.unitGrid.Insert(transform.position, ant);
        ant.oldPos = new Vector2(transform.position.x, transform.position.y);
        }

        
        
    }



    void Update()
    {   
        foreach(AntPiss ant in spawnedAnts)
        ActUpdate(ant);
        
    }


    void Updatepos(AntPiss ant)
    {   
        Vector2 newPos = new Vector2(ant.transform.position.x, ant.transform.position.y);
        JobManagerpiss.Instance.unitGrid.Update(ant, ant.oldPos, newPos);
        ant.oldPos = newPos;
    }


    void LateUpdate()
    {
        foreach(AntPiss ant in spawnedAnts)
        Updatepos(ant);
        
    }






    void ActUpdate(AntPiss ant)
    {
        switch (ant.state)
        {
            case AntPiss.State.Moving:
                FollowPath(ant);
                break;

            case AntPiss.State.Working:
                WorkJob(ant);
                break;
        }
    }
    


    public void MakeState(AntPiss.State newState, AntPiss ant)
    {
        if (JobManagerpiss.Instance != null)
        {
            JobManagerpiss.Instance.antsByState[ant.state].Remove(ant);
            ant.state = newState;
            JobManagerpiss.Instance.antsByState[ant.state].Add(ant);
        }
        else
        {
            ant.state = newState;
        }
    }


    public void JobRequest(Job job, AntPiss ant)
    {
        var state = ant.state;
        if (state != AntPiss.State.Idle)
        return;

        if (ant.currentJob != null)
        return;

    
        ant.currentJob = job;
        Vector2Int start = Vector2Int.RoundToInt(ant.transform.position);
        Vector3Int end = ant.currentJob.tilePosition;

         Pathfinding pathfinding = MapGenerator.Instance.pathfinding;

        ant.path = pathfinding.FindPath(
            start.x, start.y,
            end.x, end.y
        );

        if (ant.path == null || ant.path.Count == 0)
        {
            Debug.LogWarning("missing path");
           JobManagerpiss.Instance.RemoveJob(ant.currentJob);
            ant.currentJob = null;
            return;

        }

        ant.pathIndex = 0;
        MakeState(AntPiss.State.Moving, ant);
    }



    void FollowPath(AntPiss ant)
    {
        if (ant.currentJob == null)
        {
            MakeState(AntPiss.State.Idle, ant);
            return;
        }

        if (ant.pathIndex >= ant.path.Count)
        {
            MakeState(AntPiss.State.Working, ant);
            return;
        }

        Vector3 target = new Vector3(ant.path[ant.pathIndex].x, ant.path[ant.pathIndex].y, newZ);

        Vector3 newPos = Vector3.MoveTowards(ant.transform.position, target, Time.deltaTime * 30f);

        ant.transform.position = newPos;

        if (Vector3.Distance(ant.transform.position, target) < 0.05f)
        {
            ant.pathIndex++;
        }
    }



    void WorkJob(AntPiss ant)
    {
        if (ant.currentJob == null)
        {
            MakeState(AntPiss.State.Idle, ant);
            return;
        }

        bool finished = ant.currentJob.DoWork(Time.deltaTime);

        // job finished (your Job should trigger callback or null externally)
        if (finished == true)
        {
            JobManagerpiss.Instance.RemoveJob(ant.currentJob);


            ant.currentJob = null;
            MakeState(AntPiss.State.Idle, ant);


            /// change latr
            
        }
        
    }

    
}
