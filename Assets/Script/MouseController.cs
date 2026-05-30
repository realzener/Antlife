using System;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    [SerializeField]    GameController gameController;
    public Pathfinding pathfinding;

    [SerializeField] private MapGenerator generator;

    private void Start()
    {
        
    }
    void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            gameController.Drawall();
            Vector3 mouseScreen = Mouse.current.position.ReadValue();
            mouseScreen.z = Mathf.Abs(Camera.main.transform.position.z);

            Vector3 world = Camera.main.ScreenToWorldPoint(mouseScreen);


            //pathfinding.GetGrid().Get(Mathf.FloorToInt(world.x), Mathf.FloorToInt(world.y) );
            Debug.Log("clickly1");
            List<PathNode> path = pathfinding.FindPath(0,0, Mathf.FloorToInt(world.x), Mathf.FloorToInt(world.y));
            Debug.Log("clickly2");
            if (path != null) {
                Debug.Log("not null");
                    for(int i=0; i<path.Count -1; i++){
                        Debug.DrawLine(new Vector3(path[i].x,path[i].y) + Vector3.one , new Vector3(path[i+1].x,path[i+1].y)+ Vector3.one , Color.purple, 10f);
                        Debug.Log("linedraw");
                    }
                
            }
        }
    }
}