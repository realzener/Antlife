using UnityEngine;

public class Ant : MonoBehaviour
{
    public AntType antType;
    public int age;
    public int health;

    private SpriteRenderer spriteRenderer;
    public MagicState magic;




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




