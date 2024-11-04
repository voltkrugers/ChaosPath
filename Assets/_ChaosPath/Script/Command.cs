using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    public string direction;
    public float time;

    public Command(string dir, float t)
    {
        direction = dir;
        time = t;
    }
}
