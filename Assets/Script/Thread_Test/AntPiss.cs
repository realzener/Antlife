using System.Collections.Generic;
using UnityEngine;

public class AntPiss : MonoBehaviour
{
    public AntType antType;
    public int age;
    public int health;

    private SpriteRenderer spriteRenderer;
    public MagicState magic;

    public int[,] happiness;
    public Job currentJob;
    public int pathIndex;
    public List<PathNode> path;
    public Vector2 oldPos;



    public enum State
    {
        Idle,
        Moving,
        Working
    }
    public State state;





    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Initialize();
    }


    void Initialize()
    {
        health = antType.maxHealth;

        magic = new MagicState
        {
            magicType = antType.magic,
            potential = Random.Range(0,10),
            level = 1
        };

        spriteRenderer.sprite = antType.sprite; 
    }
}




