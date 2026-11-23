using System;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class MouseController : MonoBehaviour
{
    [SerializeField]    GameController gameController;
    public Pathfinding pathfinding;

    [SerializeField] private MapGenerator generator;
    [SerializeField] private Tilemap tilemap;
    
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            gameController.Drawall();
            Vector3 mouseScreen = Mouse.current.position.ReadValue();
            mouseScreen.z = Mathf.Abs(mainCamera.transform.position.z);

            Vector3 worldpos = mainCamera.ScreenToWorldPoint(mouseScreen);



            Vector3Int cellPosition = tilemap.WorldToCell(worldpos);


            //pathfinding.GetGrid().Get(Mathf.FloorToInt(world.x), Mathf.FloorToInt(world.y) );
            TestJobSpawner.Instance.CreateTestBuildJob(cellPosition);
        }
    }
}