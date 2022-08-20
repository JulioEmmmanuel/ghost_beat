using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostShield : Shield
{

    void Awake()
    {
        breakpoints = new List<float>();
        endurance = 3;
        breakpoints.Add(1);
        breakpoints.Add(1);
        breakpoints.Add(1);
    }


}
