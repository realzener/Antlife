using System.Collections.Generic;
using UnityEngine;

public class AntAI : MonoBehaviour
{
    private Ant ant;

    private Job currentJob;

    private List<PathNode> path;

    private int pathIndex;
    private float newZ = -2f;
    private Vector2 oldPos;

    public enum State
    {
        Idle,
        Moving,
        Working
    }

    public State state;

    private float thinkTimer;

    void Start()
    {
        ant = GetComponent<Ant>();
        MakeState(State.Idle);
        thinkTimer = Random.Range(0.1f, 0.3f); // stagger ants
        JobManager.Instance.unitGrid.Insert(transform.position, this);
        oldPos = new Vector2(transform.position.x, transform.position.y);
    }

    void MakeState(State newState)
    {
        if (JobManager.Instance != null)
        {
            JobManager.Instance.antsByState[state].Remove(this);
            state = newState;
            JobManager.Instance.antsByState[state].Add(this);
        }
        else
        {
            state = newState;
        }
    }


    void Update()
    {
       // ThinkUpdate();
        ActUpdate();
        
    }

    void Updatepos()
    {
        Vector2 newPos = new Vector2(transform.position.x, transform.position.y);
        JobManager.Instance.unitGrid.Update(this, oldPos, newPos);
        oldPos = newPos;
    }

    void LateUpdate()
    {
        Updatepos();
    }
    


    

    public void JobRequest(Job job)
    {

        if (state != State.Idle)
        return;

        if (currentJob != null)
        return;

    
        currentJob = job;
        Vector2Int start = Vector2Int.RoundToInt(transform.position);
        Vector3Int end = currentJob.tilePosition;

         Pathfinding pathfinding = MapGenerator.Instance.pathfinding;

        path = pathfinding.FindPath(
            start.x, start.y,
            end.x, end.y
        );

        if (path == null || path.Count == 0)
        {
            Debug.LogWarning("missing path");
           JobManager.Instance.RemoveJob(currentJob);
            currentJob = null;
            return;

        }

        pathIndex = 0;
        MakeState(State.Moving);
    }
    

    // ACT (fast logic)
    void ActUpdate()
    {
        switch (state)
        {
            case State.Moving:
                FollowPath();
                break;

            case State.Working:
                WorkJob();
                break;
        }
    }

    void FollowPath()
    {
        if (currentJob == null)
        {
            MakeState(State.Idle);
            return;
        }

        if (pathIndex >= path.Count)
        {
            MakeState(State.Working);
            return;
        }

        Vector3 target = new Vector3(path[pathIndex].x, path[pathIndex].y, newZ);

        Vector3 newPos = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 30f);

        transform.position = newPos;

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            pathIndex++;
        }
    }

    void WorkJob()
    {
        if (currentJob == null)
        {
            MakeState(State.Idle);
            return;
        }

        bool finished = currentJob.DoWork(Time.deltaTime);

        // job finished (your Job should trigger callback or null externally)
        if (finished == true)
        {
            JobManager.Instance.RemoveJob(currentJob);


            currentJob = null;
            MakeState(State.Idle);


            /// change latr
            
        }
        
    }

    public void CancelJob()
    {
        if (currentJob != null)
        {
            currentJob.isAssigned = false;
            currentJob.CancelJob();
        }

        currentJob = null;
        MakeState(State.Idle);
    }

    public void AssignJob(Job job, List<PathNode> newPath)
    {
        currentJob = job;

        path = newPath;

        pathIndex = 0;

        MakeState(State.Moving);
    }





//     public float CalculateBid(Job job)
// {
//     // busy ants cannot bid
//     if (state != State.Idle)
//         return float.MaxValue;

//     float dist = Vector2.Distance(
//         transform.position,
//         new Vector2(job.tilePosition.x, job.tilePosition.y)
//     );

//     return dist;
// }







// THINK (slow logic)
    #region think
    void ThinkUpdate()
    {
        thinkTimer -= Time.deltaTime;

        if (thinkTimer > 0f)
            return;

        thinkTimer = Random.Range(0.1f, 1.7f);

        switch (state)
        {
            case State.Idle:
                // TryGetJob();
                break;
        }
    }

    // void TryGetJob()
    // {
    //     if(JobManager.Instance ==null)
    //         return;
    //     currentJob = JobManager.Instance.RequestJob(
    //         Vector3Int.RoundToInt(transform.position)
    //     );

    //     if (currentJob == null)
    //         return;

    //     Vector3Int start = Vector3Int.RoundToInt(transform.position);
    //     Vector3Int end = currentJob.tilePosition;

    //      Pathfinding pathfinding = MapGenerator.Instance.pathfinding;

    //     path = pathfinding.FindPath(
    //         start.x, start.y,
    //         end.x, end.y
    //     );

    //     if (path == null || path.Count == 0)
    //     {
    //         currentJob.isAssigned = false;
    //         currentJob = null;
    //         return;
    //     }

    //     pathIndex = 0;
    //     MakeState(State.Moving);
    // }
    #endregion






}