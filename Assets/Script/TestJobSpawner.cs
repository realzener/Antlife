using UnityEngine;
using UnityEngine.InputSystem;

public class TestJobSpawner : MonoBehaviour
{
    public Vector3Int jobPosition;

    void Start()
    {
        CreateTestJob();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            CreateTestJob();
        }
    }

    void CreateTestJob()
    {
        Job job = new Job(jobPosition, OnJobComplete, 3f);

        JobManager.Instance.AddJob(job);

        Debug.Log("Test Job Created at " + jobPosition);
    }

    void OnJobComplete(Job job)
    {
        Debug.Log("Job completed at " + job.tilePosition);
    }
}