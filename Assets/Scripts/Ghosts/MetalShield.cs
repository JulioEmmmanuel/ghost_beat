using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalShield : Shield
{
    // Start is called before the first frame update
    void Awake()
    {
        breakpoints = new List<float>();
        endurance = 3;
        breakpoints.Add(3);
        breakpoints.Add(3);
        breakpoints.Add(3);
    }
}
