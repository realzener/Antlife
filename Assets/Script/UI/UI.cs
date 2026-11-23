using UnityEngine;
using TMPro;

public class UI : MonoBehaviour

{
    [SerializeField]
    public TMP_Text jobs;
    public TMP_Text idleAntCountT;
    public TMP_Text movingAntCountT;
    public TMP_Text workingAntCountT;

    int jobCount = 0;
    int idleAntCount = 0;
    int movingAntCount = 0;
    int workingAntCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int _jobCount = JobManagerpiss.Instance.availableJobs.Count;
        
        if(jobCount != _jobCount){
        jobs.text = _jobCount.ToString();
        jobCount = _jobCount;
        }


        int _idleAntCount = JobManagerpiss.Instance.antsByState[AntPiss.State.Idle].Count;

        if(idleAntCount != _idleAntCount){
        idleAntCountT.text = _idleAntCount.ToString();
        idleAntCount = _idleAntCount;
        }

        int _movingAntCount = JobManagerpiss.Instance.antsByState[AntPiss.State.Moving].Count;

        if(movingAntCount != _movingAntCount){
        movingAntCountT.text = _movingAntCount.ToString();
        movingAntCount = _movingAntCount;
        }

        int _workingAntCount = JobManagerpiss.Instance.antsByState[AntPiss.State.Working].Count;

        if(workingAntCount != JobManagerpiss.Instance.antsByState[AntPiss.State.Working].Count){
        workingAntCountT.text = _workingAntCount.ToString();
        workingAntCount = _workingAntCount;
        }



    }
}
