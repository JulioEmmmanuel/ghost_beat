using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGhost : Ghost
{

    // Start is called before the first frame update
    override protected void Awake()
    {
        base.Awake();
        lifePoints = 5.0f;
        strength = 1.0f;
        velocity = 0.25f;
        scorePoints = 100;
    }


}
