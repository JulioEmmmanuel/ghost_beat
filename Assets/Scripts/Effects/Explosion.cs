using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Timer timer;


    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 0.5f;
        timer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.Finished)
        {
            Destroy(gameObject);
        }
    }
}
