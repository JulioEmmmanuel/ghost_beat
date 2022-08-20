using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGhost : Ghost
{
    protected override void Awake()
    {
        base.Awake();
        lifePoints = 2.0f;
        strength = 1.0f;
        velocity = 1.0f;
        scorePoints = 50;
    }

}
