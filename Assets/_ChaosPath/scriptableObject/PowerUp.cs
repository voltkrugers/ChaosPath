using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    None,
    Coin,
    AsteroidStatic,
    AsteroidCrossing,
    AsteroidAlternating,
    Projectile
}

[CreateAssetMenu(fileName = "PowerUp", menuName = "ScriptableObjects/PowerUp", order = 1)]
public class PowerUp : ScriptableObject
{
    public PowerUpType type;
    public Sprite sprite;
}
