using UnityEngine;
using UnityEngine.InputSystem;

public class TestJobSpawner : MonoBehaviour
{
    public Vector3Int jobPositionbase;
    public Vector3Int jobPosition;
    public int jobs = 100;

    void Start()
    {
        

        
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
        for (int i = 0; i < jobs; i++){
        jobPosition = new Vector3Int(Random.Range(0,jobPositionbase.x), Random.Range(0,jobPositionbase.y), 0);
        Job job = new Job(jobPosition, OnJobComplete, 3f);

        JobManager.Instance.AddJob(job);

        Debug.Log("Test Job Created at " + jobPosition);
        }
    }

    void OnJobComplete(Job job)
    {
        Debug.Log("Job completed at " + job.tilePosition);
    }


    
}