using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    public Vector2 direction;
    public float time;

    public Command(Vector2 dir, float t)
    {
        direction = dir;
        time = t;
    }
}
