using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalShieldGhost : ProtectiveGhost
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        strength = 1.0f;
        velocity = 0.10f;
        scorePoints = 100;
        shield = gameObject.AddComponent<MetalShield>();
        lifePoints = shield.Endurance;
        pointsToNext = shield.getBreakpoint();
    }
}

