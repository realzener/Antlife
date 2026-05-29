using System.Collections.Generic;
using UnityEngine;

public class JobQueue : MonoBehaviour
{
    Queue<Job> jobQueue;

    void OnEnable()
    {
      jobQueue = new Queue<Job>();  
    }
}
