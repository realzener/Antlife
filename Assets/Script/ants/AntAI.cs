using System.Collections.Generic;
using UnityEngine;

public class AntAI : MonoBehaviour
{
    private Ant ant;

    private Job currentJob;

    private List<PathNode> path;
    private int pathIndex;
    private float newZ = -2f;

    public enum State
    {
        Idle,
        Moving,
        Working
    }

    public State state;

    private float thinkTimer;

    void Awake()
    {
        ant = GetComponent<Ant>();
        state = State.Idle;
        thinkTimer = Random.Range(0.1f, 0.3f); // stagger ants
        
    }

    void Update()
    {
        ThinkUpdate();
        ActUpdate();
    }


    // THINK (slow logic)
    #region think
    void ThinkUpdate()
    {
        thinkTimer -= Time.deltaTime;

        if (thinkTimer > 0f)
            return;

        thinkTimer = Random.Range(0.1f, 0.3f);

        switch (state)
        {
            case State.Idle:
                TryGetJob();
                break;
        }
    }

    void TryGetJob()
    {
        currentJob = JobManager.Instance.RequestJob(
            Vector3Int.RoundToInt(transform.position)
        );

        if (currentJob == null)
            return;

        Vector3Int start = Vector3Int.RoundToInt(transform.position);
        Vector3Int end = currentJob.tilePosition;

        Pathfinding pathfinding = MapGenerator.Instance.pathfinding;

        path = pathfinding.FindPath(
            start.x, start.y,
            end.x, end.y
        );

        if (path == null || path.Count == 0)
        {
            currentJob.isAssigned = false;
            currentJob = null;
            return;
        }

        pathIndex = 0;
        state = State.Moving;
    }
    #endregion

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
            state = State.Idle;
            return;
        }

        if (pathIndex >= path.Count)
        {
            state = State.Working;
            return;
        }

        Vector3 target = new Vector3(path[pathIndex].x, path[pathIndex].y, newZ);

        Vector3 newPos = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 3f);

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
            state = State.Idle;
            return;
        }

        bool finished = currentJob.DoWork(Time.deltaTime);

        // job finished (your Job should trigger callback or null externally)
        if (finished == true)
        {
            state = State.Idle;
        }
        
    }

    // -------------------------
    // OPTIONAL: external reset
    // -------------------------
    public void CancelJob()
    {
        if (currentJob != null)
        {
            currentJob.isAssigned = false;
            currentJob.CancelJob();
        }

        currentJob = null;
        state = State.Idle;
    }
}