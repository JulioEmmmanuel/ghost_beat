using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    protected int endurance;
    protected List<float> breakpoints;
    protected int currentState;
    protected bool destroyed;

    public int Endurance
    {
        get
        {
            return endurance;
        }
    }

    public int CurrentState
    {
        get
        {
            return currentState;
        }
    }

    public bool Destroyed
    {
        get
        {
            return destroyed;
        }
    }


    public float getBreakpoint()
    {
        return breakpoints[currentState];
    }



    public void advanceState()
    {
        currentState += 1;
        if (currentState == breakpoints.Count)
        {
            destroyed = true;
        }
    }

    // Start is called before the first frame update
    virtual protected void Start()
    {
        currentState = 0;
        destroyed = false;
    }

    virtual public void GetDamage(float forceAttack)
    {

    }

    virtual public void addAttacker(GameObject go)
    {

    }


}
