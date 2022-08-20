using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGhost : ProtectiveGhost
{
    
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        strength = 1.0f;
        velocity = 0.25f;
        scorePoints = 50;
        shield = gameObject.AddComponent<GhostShield>();
        lifePoints = shield.Endurance;
        pointsToNext = shield.getBreakpoint();
    }


}
