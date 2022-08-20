using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    protected float strength;
    protected float velocity;
    protected float rechargeTime;
    protected Direction dir;


    public float Strength
    {
        get
        {
            return strength;
        }
    }

    public float Velocity
    {
        get
        {
            return velocity;
        }
    }

    public float RechargeTime
    {
        get
        {
            return rechargeTime;
        }
    }

    public Direction Dir
    {
        get
        {
            return dir;
        }
        set
        {
            dir = value;
        }
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
