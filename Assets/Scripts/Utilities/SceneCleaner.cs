using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCleaner : MonoBehaviour
{
    private void Awake()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("collectable"))
        {
            Destroy(go);
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("effect"))
        {
            Destroy(go);
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("enemy"))
        {
            Destroy(go);
        }

    }
}
