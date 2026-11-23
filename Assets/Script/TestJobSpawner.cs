using UnityEngine;
using UnityEngine.InputSystem;

public class TestJobSpawner : MonoBehaviour
{
    public Vector3Int jobPositionbase;
    public Vector3Int jobPosition;
    public int jobs = 400;

    public static TestJobSpawner Instance;

    void Start()
    {
        Instance = this;

        
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            
            CreateTestJob();
        }

        if(Keyboard.current.altKey.wasPressedThisFrame)
        {
            // JobManager.Instance.JobRequest();
            Debug.LogWarning("i pressed");
        }
    }

    void CreateTestJob()
    {
        for (int i = 0; i < jobs; i++){
        jobPosition = new Vector3Int(Random.Range(0,jobPositionbase.x), Random.Range(0,jobPositionbase.y), 0);
        Job job = new Job(jobPosition, OnJobComplete, 3f);

        JobManagerpiss.Instance.AddJob(job);

        //Debug.Log("Test Job Created at " + jobPosition);
        }
    }


    public void CreateTestBuildJob(Vector3Int jobPosition)
    {
        Job job = new JobBuild(TileType.Wall,jobPosition, OnJobComplete, 3f);

        JobManagerpiss.Instance.AddJob(job);
    }

    void OnJobComplete(Job job)
    {
        //Debug.Log("Job completed at " + job.tilePosition);
    }


}