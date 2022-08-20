using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    //timer duration
    private float duration = 0;

    //timer execution
    private float elapsedSeconds = 0;
    private bool running = false;

    //support for finished property
    private bool started = false;

    //setter for duration
    public float Duration
    {
        set
        {
            if (!running)
            {
                duration = value;
            }
        }
    }

    public bool Finished
    {
        get
        {
            return started && !running;
        }
    }


    public void Run()
    {
        if(duration > 0)
        {
            started = true;
            running = true;
            elapsedSeconds = 0;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            elapsedSeconds += Time.deltaTime;
            if(elapsedSeconds >= duration)
            {
                running = false;
            }
        }
    }

    public void Drop()
    {
        if (running)
        {
            running = false;
        }
    }

}
