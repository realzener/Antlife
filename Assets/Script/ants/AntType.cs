using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Ant/Type")]
public class AntType : ScriptableObject
{
    public AntRole role;
    public Sprite sprite;

    public int maxHealth;
    public int lifespan;
    public MagicType magic;
}

// [Serializable]
// public class Magic
// {
//     public MagicType magicType;
// }


public enum MagicType
{
    None,
    Dark,
    Fire,
    Water
}

public enum AntRole
{
    Queen,
    Worker,
    Soldier,
    Mage
}

